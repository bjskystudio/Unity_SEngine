// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:22:47
// ========================================================

//套接字频道 Kcp Tcp Udp

using System;
using Google.Protobuf;
using SEngine.Net;
using XLua;

namespace Game.Network
{
    [BlackList]
    public interface ISocketChannel
    {
        bool IsServerCloseKick { get; set; }
        
        bool AutoReconnect { get; set; }
        
        string IP { get; set; }
        
        int Port { get; set; }

        SocketListener Listener { get; }

        IDisposable HeartbeatDisposable { get; set; }
        
        ISocket Socket { get; set; }

        int RetryCount { get; set; }
        
        int MaxRetryCount { get; set; }
        
        IDisposable LoadingDisposable { get; set; }

        event Action<ISocket> SocketDisconnectEvent;
        
        event Action<ISocket> SocketReconnectEvent;
        
        bool IsConnect();
        
        void Connect();
        
        void Close(bool serverClose = false);
        
        void Disconnect(ISocket socket);
        
        void Reconnect(ISocket socket);
        
        void Send(ProtocolEnum cmd, IMessage message);
        
        void Send(int cmd, byte[] data);
    }
}