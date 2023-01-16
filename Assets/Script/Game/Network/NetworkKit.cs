// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:41:52
// ========================================================

//网络工具类

using System;
using System.Collections.Generic;
using SEngine.Net;
using Google.Protobuf;
using Vavavoom.SIMB.Game.Library.Crypto;
using XLua;
using Object = System.Object;
using UnityEngine;

namespace Game.Network
{
    public class ConnectStatus
    {
        public int Status = 0;
        public string Msg = "";
        public ConnectStatus(int status,string msg)
        {
            Status = status;
            Msg = msg;
        }
    }
    public static class NetworkKit
    {
        #region ###Property###

        //c#消息注册
        private static Dictionary<int, Dictionary<Object, Action<IMessage>>> _CSharpMsgHandlers;
        
        //是否是马甲包(可能以后要用 先留着)
        private static bool _IOSMJ;
        
        //我们只考虑单一socket频道 后面有多频道再改设计
        private static ISocketChannel _DefaultSocketChannel;

        private static bool _Inited;

        #endregion

        public static void Init()
        {
            _CSharpMsgHandlers = new Dictionary<int, Dictionary<object, Action<IMessage>>>();
            _DefaultSocketChannel = new TcpSocketChannel("", 0, new DefaultSocketListener(), new LV4Protocol());
            
            SocketProtocol.I.Init(new ProtobufSerialize(), new ProtobufDeserialize(), new EmptyCrypto(),  new LZ4Zip());

            Debug.Log("NetworkKit:网络连接初始化");
            MessageQueueHandler mq = MessageQueueHandler.Instance;
            _Inited = true;
            
            //RegisterCSharpMsg<CommonAckInfo>(ProtocolEnum.COMMON_ACK, ACKAction);
        }

        public static void UnInit()
        {
            _CSharpMsgHandlers = null;
            _DefaultSocketChannel = null;
            
            SocketProtocol.I.UnInit();

            _Inited = false;
            
            //UnregisterCSharpMsg<CommonAckInfo>(ProtocolEnum.COMMON_ACK, ACKAction);             
        }

        #region ###API###

        public static ISocketChannel DefaultSocketChannel => _DefaultSocketChannel;

        public static bool IsIOSMJ
        {
            get => _IOSMJ;
            set => _IOSMJ = value;
        }

        //连接网络
        //eventCollect:1=连接成功 2=连接关闭 3=正在等待接连(超时) 4= 连接出错(返回错误信息)
        public static void Connect(string ip, int port, Action<int,string> eventCollect)
        {
            if (false == _Inited)
            {
                return;
            }
            
            _DefaultSocketChannel.IP = ip;
            _DefaultSocketChannel.Port = port;
            _DefaultSocketChannel.Listener.SocketConnectCollectEvent += (int status,ISocket us,string msg)=>
            {
                Loom.QueueOnMainThread((param) =>
                {
                    eventCollect?.Invoke(((ConnectStatus)param).Status, ((ConnectStatus)param).Msg);
                },new ConnectStatus(status,msg));
            };
            _DefaultSocketChannel.Connect();
        }


        public static void SendServerHttpRequest(string url, string postData, Action<int, string> callback)
        {
            //HTTPRequest req = new HTTPRequest(url, "POST", 5, (HTTPResponse resp) =>
            // {
            //     string text = resp.GetResponseText();
            //     callback?.Invoke(resp.StatusCode, text);
            // });
            //req.AddPostData("username", userName);
            //req.AddPostData("password", pwd);
            //req.Start();
            HTTPRequestMT req = new HTTPRequestMT(url, 1, (HTTPResponseMT resp) =>
             {
                 callback?.Invoke(resp.StatusCode, resp.ReceiveContent);
             });
            req.SetPostData(postData);
            req.Send();
        }

        //断开连接
        public static void DisConnect()
        {
            _DefaultSocketChannel?.Close();
        }

        public static bool IsConnected()
        {
            if (null == _DefaultSocketChannel)
            {
                return false;
            }

            return _DefaultSocketChannel.IsConnect();
        }

        //注册c#消息
        public static void RegisterCSharpMsg<T>(ProtocolEnum cmd, Action<T> callback) where T:IMessage
        {
            if (false == _Inited)
            {
                return;
            }
            
            // 如果为空无效
            if (callback == null)
            {
                return;
            }

            int iCmd = (int)cmd;
            Action<IMessage> tempCallback = msg => callback((T)msg);

            if (false == _CSharpMsgHandlers.TryGetValue(iCmd, out var listData))
            {
                listData = new Dictionary<object, Action<IMessage>>();
                _CSharpMsgHandlers.Add(iCmd, listData);
            }

            // 判断是否存在
            if (true == listData.ContainsKey(callback))
            {
                listData.Remove(callback);
            }
            
            listData.Add(callback, tempCallback);
        }
        
        //注销c#消息
        public static void UnregisterCSharpMsg<T>(ProtocolEnum cmd, Action<T> callback) where T : IMessage
        {
            if (false == _Inited)
            {
                return;
            }
            
            int iCmd = (int)cmd;

            if (true == _CSharpMsgHandlers.TryGetValue(iCmd, out var listData))
            {
                // 判断是否存在
                if (true == listData.ContainsKey(callback))
                {
                    listData.Remove(callback);
                }
            }
        }

        //发送c#消息
        [BlackList]
        public static void Send(ProtocolEnum cmd, IMessage msg)
        {
            if (false == _Inited)
            {
                return;
            }
            
            _DefaultSocketChannel.Send(cmd, msg);
        }
        
        //发送lua消息
        public static void SendLua(short cmd, byte[] bytes)
        {
            if (false == _Inited)
            {
                return;
            }
            
            _DefaultSocketChannel.Send(cmd, bytes);
        }
        
        //分发C#消息
        [BlackList]
        public static void HandleCSharpMessage(int cmd, IMessage msg)
        {
            if (false == _Inited)
            {
                return;
            }
            
            if (null == msg)
            {
                return;
            }
            
            if (true == _CSharpMsgHandlers.TryGetValue(cmd, out var actions))
            {
                foreach(var kp in actions)
                {
                    kp.Value?.Invoke(msg);
                }
            }
        }

        #endregion

        public static void TestMessage()
        {
            //测试
            MessageQueueHandler.Instance.DispatchNetMessage(1, null, 1);
        }

        #region ###Private###

        //private static void ACKAction(CommonAckInfo commonAck)
        //{
        //    UIModule.Instance.CloseWindow("UIConnecting");
        //    Debug.Log($"commonAck is code = {commonAck.Code} msg ={commonAck.Msg}");
        //}

        #endregion
    }
}