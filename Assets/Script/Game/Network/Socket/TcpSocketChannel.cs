// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:36:15
// ========================================================

//Tcp套接字频道

using SEngine.Net;
using Google.Protobuf;
using UnityEngine;
using XLua;

namespace Game.Network
{
    [BlackList]
    public class TcpSocketChannel : SocketChannel
    { 
        private USocket _Socket;
        private SocketListener _Listener;
        private SEngine.Net.Protocol _Protocol;

        public override ISocket Socket
        {
            get => _Socket;
            set => Debug.Assert(false, "TcpSocketChannel can not set!!!!");
        }

        public override SocketListener Listener => _Listener;

        public TcpSocketChannel(string ip, int port, SocketListener listener, SEngine.Net.Protocol protocol)
        {
            IP = ip;
            Port = port;
            _Listener = listener;
            _Protocol = protocol;
            _Socket = new USocket(listener, protocol);
        }

        public override bool IsConnect()
        {
            if (null == _Socket)
            {
                return false;
            }
            
            return USocket.STATUS_CONNECTED == _Socket.getStatus();
        }

        public override void Connect()
        {
            if (false == IsConnect())
            {
                _Socket?.Connect(IP, Port);
            }
        }

        public override void Close(bool serverClose = false)
        {
            HeartbeatDisposable?.Dispose();
            HeartbeatDisposable = null;

            if (null != _Socket)
            {
                if (USocket.STATUS_CLOSED != _Socket.getStatus())
                {
                    _Socket.Close(serverClose);
                }
            }
        }

        public override void Send(ProtocolEnum cmd, IMessage message)
        {
            if (null == _Socket)
            {
                Debug.Assert(false, "Socket hasn't been initzed yet!");
                return;
            }
            
            if (USocket.STATUS_CONNECTED == _Socket.getStatus())
            {
                Frame f = SocketProtocol.I.EncodeMessage(cmd, message, false);
                _Socket.Send(f.GetData());
            }
            else
            {
                
            }
        }

        public override void Send(int cmd, byte[] data)
        {
            if (null == _Socket)
            {
                Debug.Assert(false, "Socket hasn't been initzed yet!");
                return;
            }
            
            if (USocket.STATUS_CONNECTED == _Socket.getStatus())
            {
                Frame f = SocketProtocol.I.EncodeMessage((short)cmd, data, false);
                _Socket.Send(f.GetData());
            }
            else
            {
                
            }
        }
    }
}