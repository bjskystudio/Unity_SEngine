using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
namespace SEngine.Net
{
    public class USocket : ISocket
    {
        private Socket clientSocket;
        private SocketListener listener;
        private Protocol protocol;
        private string ip;
        private int port;
        private int status;
        private bool asyc;//异步收取
        private bool serverClose = true;//服务器主动关闭
        public const int STATUS_INIT = 0;
        public const int STATUS_CONNECTING = 1;
        public const int STATUS_CONNECTED = 2;
        public const int STATUS_CLOSED = 3;
        private ByteBuf buf;//接收缓存区
        //private BlockingQueue<ByteBuf> queue ;//阻塞队列
        private long sending;//发送的数量
        private long sended;//已经发送成功的数量
        private long bytesSended;//已经发送出去的字节数量
        private IDisposable timeoutDisposable;
        private bool timeout;
        private Queue<ByteBuf> _SendQueue = new Queue<ByteBuf>();
        private SocketAsyncEventArgs _SAEA;
        private bool _IsSendEmpty = true;
        private object _LockHandle = new object();
        /**
         * 构造（但不完善，需要设置监听器和协议解析器）
         */
        public USocket()
        {
            buf = new ByteBuf(4096);
            //queue = new BlockingQueue<ByteBuf>(5000);
        }
        /**
         * 构造
         */
        public USocket(SocketListener listener, Protocol protocol)
        {
            this.listener = listener;
            this.protocol = protocol;
            buf = new ByteBuf(4096);
            //queue = new BlockingQueue<ByteBuf>(5000);
        }

        static void TimerCallBack(object state)
        {

        }

        /**
         * 连接指定地址
        */
        public void Connect(string ip, int port)
        {
            timeout = false;
            this.status = STATUS_CONNECTING;
            this.ip = ip;
            this.port = port;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.NoDelay = true;
            LingerOption linger = new LingerOption(false, 0);
            clientSocket.LingerState = linger;
            //clientSocket.ReceiveBufferSize = (int)(1000000 * 8 * 0.1 * 2);
            //clientSocket.ExclusiveAddressUse = false;
            //clientSocket.SendTimeout = 3000;//send timeout
            //clientSocket.SendBufferSize = 1024 * 8;//16k的发送缓冲区
            //clientSocket.Blocking = false;
            //clientSocket.DontFragment = false;
            timeoutDisposable?.Dispose();
            timeoutDisposable = Observable.Timer(TimeSpan.FromSeconds(5)).Subscribe(null,_ => {
                if (clientSocket.Connected)
                    return;
                timeout = true;
                this.status = STATUS_CLOSED;
                this.serverClose = false;
                clientSocket.Close();
                this.listener.OnIdle(this);
                this.listener.OnClose(this, false);

            });
            clientSocket.BeginConnect(this.ip, this.port, connected, this);
            
         }
        /**
         * 连接成功
        */
        private void connected(IAsyncResult asyncConnect)
        {
            serverClose = true;
            timeoutDisposable?.Dispose();
            if (timeout)
                return;
            
            if (this.clientSocket.Connected)
            {
                this.clientSocket.EndConnect(asyncConnect);
                this.status = STATUS_CONNECTED;
                receive();//开始收取
                //Thread t = new Thread(new ThreadStart(send));//启动发送线程，同步发送
                //t.IsBackground = true;
                //t.Start();
                this.listener.OnOpen(this);
            }
            else
            {
                this.status = STATUS_CLOSED;
                this.serverClose = false;
                this.listener.OnIdle(this);
                this.listener.OnClose(this, false);
            }
            
        }
        /**
         * 装入一个监听器
         */
        public void setListener(SocketListener listener)
        {
            this.listener = listener;
        }
        /**
         * 装入一个协议解析器
         */
        public void setProtocol(Protocol p)
        {
            this.protocol = p;
        }
        /**
         * 协议
         */
        public Protocol GetProtocol()
        {
            return this.protocol;
        }
        public int getStatus()
        {
            return this.status;
        }
        public bool isAsyc()
        {
            return asyc;
        }
        public void setAsyc(bool a)
        {
            this.asyc = a;
        }
        public string GetIP()
        {
            return this.ip;
        }
        public int GetPort()
        {
            return this.port;
        }
        /*public long getSending()
        {
            return this.sending;
        }
        public long getSended()
        {
            return this.sended;
        }
        public long getBytesSended()
        {
            return this.bytesSended;
        }*/
        /**
         * 关闭连接
         */
        public void Close(bool serverClose = false)
        {
            this.serverClose = serverClose;
            if (serverClose)
            {
                Debug.LogWarning("服务器关闭网络链接！");
            }
           
            try
            {
                if (clientSocket != null 
                && clientSocket.Connected)
                {
                    //this.status = STATUS_CLOSED;
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                    //this.status = STATUS_CLOSED;
                    //this.serverClose = serverClose;
                    //this.listener.OnClose(this, this.serverClose);
                }
            }
            catch (Exception e)
            {
            }
            finally
            {
                if (status == STATUS_CONNECTED)
                {
                    this.status = STATUS_CLOSED;
                    this.listener.OnClose(this, this.serverClose);
                }
            }
        }

        public void Update()
        {
            if (_SendQueue.Count <= 0)
            {
                return;
            }

            lock (_LockHandle)
            {
                if (false == _IsSendEmpty)
                {
                    return;
                }

                var bResult = SendInner(_SendQueue.Dequeue());
                if (true == bResult)
                {
                    _IsSendEmpty = false;
                }
            }
        }

        public void Send(ByteBuf frame)
        {
            foreach (var item in _SendQueue)
            {
                if (frame.GetRaw() == item.GetRaw())
                {
                    int a = 0;
                }
            }
            
            _SendQueue.Enqueue(frame);
        }

        /**
         *发送
         */
        private bool SendInner(ByteBuf frame)
        {
            try
            {
                if (this.status == STATUS_CONNECTED)
                {
                    this.sending++;
                    
                    if (null == _SAEA)
                    {
                        _SAEA = new SocketAsyncEventArgs();
                        _SAEA.Completed += OnSend;
                    }
                    
                    _SAEA.SetBuffer(frame.GetRaw(), frame.ReaderIndex(), frame.ReadableBytes());
                    _SAEA.UserToken = frame.GetRaw();

                    this.clientSocket.SendAsync(_SAEA);

                    return true;
                }
            }
            catch (Exception ex)
            {
                this.Close(true);
            }

            return false;
        }
        /**
         * 发送回调
         */
        private void OnSend(object sender, SocketAsyncEventArgs e)
        {
            lock (_LockHandle)
            {
                var bytes = e.UserToken as byte[];
            
                TSArrayPool<byte>.Release(bytes);

                if (e.SocketError == SocketError.Success)
                {
                    //ByteBuf bb = e.UserToken as ByteBuf;
                    //Interlocked.Increment(ref this.sended);
                    //Interlocked.Add(ref this.bytesSended, bb.ReadableBytes());
                }
                else//发送失败
                {
                    this.Close(true);
                }

                _IsSendEmpty = true;
            }
        }

        private bool CheckSocketIsConnected(Socket so)
        {
            if (null == so
            || false == so.Connected    
            )
            {
                return false;
            }

            return true;
        }

        /**
         * 接收数据
         */
        private void receive()
        {
            if (this.status == STATUS_CONNECTED)
            {
                try
                {
                    if (CheckSocketIsConnected(clientSocket) == true)
                    {
                        clientSocket.BeginReceive(buf.GetRaw(), 0, buf.GetRaw().Length, SocketFlags.None, new AsyncCallback(onRecieved), clientSocket);
                    }
                }
                catch (Exception e)
                {
                    this.status = STATUS_CLOSED;
                    this.listener.OnError(this, e.StackTrace + e.Message);
                    this.Close(true);
                }
            }
        }



        /**
         * 异步收取信息
         */
        private void onRecieved(IAsyncResult ar)
        {
            try
            {
                Socket so = (Socket)ar.AsyncState;
                if (CheckSocketIsConnected(so) == false)
                {
                    return;
                }

                int len = so.EndReceive(ar, out var socketError);
                if (len > 0)
                {
                    buf.ReaderIndex(0);
                    buf.WriterIndex(len);
                    while (true)
                    {
                        ByteBuf frame = this.protocol.TranslateFrame(ref buf);
                        if (null != frame.GetRaw())
                        {
                            this.listener.OnMessage(this, frame);
                        }
                        else
                        {
                            break;
                        }
                    }
                    
                    this.receive();
                }
                else
                {
                    this.Close(true);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                this.Close(serverClose);
            }
        }
    }
}

