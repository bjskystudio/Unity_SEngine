// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:31:12
// ========================================================

//中间基类

using System;
using SEngine.Net;
using Google.Protobuf;
using XLua;

namespace Game.Network
{
    [BlackList]
    public abstract class SocketChannel : ISocketChannel
    {
        public string IP { get; set; }
        public int Port { get; set; }
        public abstract SocketListener Listener { get; }
        public IDisposable HeartbeatDisposable{ get; set; }
        public abstract ISocket Socket { get;  set; }
        public bool Login { get; set; }
        public int RetryCount { get; set; }
        public int MaxRetryCount { get; set; }

        public IDisposable LoadingDisposable { get; set; }
        public bool IsServerCloseKick { get; set; }
        public bool AutoReconnect { get; set; }

        public event Action<ISocket> SocketDisconnectEvent;
        public event Action<ISocket> SocketReconnectEvent;

        public SocketChannel()
        {
            MaxRetryCount = 1;
            RetryCount = MaxRetryCount;
        }

        public abstract void Close(bool serverClose = false);

        public abstract void Connect();

        public abstract bool IsConnect();

        public abstract void Send(ProtocolEnum cmd, IMessage message);
        public abstract void Send(int cmd, byte[] data);

        public void Disconnect(ISocket socket)
        {
            SocketDisconnectEvent?.Invoke(socket);
        }

        public void Reconnect(ISocket socket)
        {
            SocketReconnectEvent?.Invoke(socket);
        }
    }
}