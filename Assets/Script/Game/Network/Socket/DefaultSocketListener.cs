// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 11:25:22
// ========================================================

//

using UnityEngine;
using SEngine.Net;
using XLua;

namespace Game.Network
{ 
    [BlackList]
    public struct MessageData
    {
        public byte[] Data;
        public int StartIndex;
        public int Length;
        public ushort ControlFlag;
    }
    
    [BlackList]
    public class DefaultSocketListener : SocketListener
    {
        
        public override void OnMessage(ISocket us, ByteBuf bb)
        {
            bb.ReaderIndex(us.GetProtocol().HeaderLen());

            ushort controlFlag = bb.ReadByte();           
            short cmd = bb.ReadShort();
            var msgData = new MessageData {Data = bb.GetRaw(), StartIndex = bb.ReaderIndex(), Length = bb.ReadableBytes(), ControlFlag = controlFlag};

            MessageQueueHandler.Instance.PushQueue(cmd, msgData);
        }

        public override void OnClose(ISocket us, bool fromRemote)
        {
            Debug.LogWarning(fromRemote ? "与服务器连接已断开" : "关闭连接");
            OnSocketClosed(us, fromRemote);
            //MessageQueueHandler.I.PushError(-1, new MessageQueueHandler.SocketCloseItem { SocketListener = this, Socket = us, FromRemote = fromRemote});
        }

        public override void OnIdle(ISocket us)
        {
            Debug.LogWarning("连接超时");
            OnSockeConnectTimeout(us);
        }

        public override void OnOpen(ISocket us)
        {
            OnSocketOpened(us);
        }

        public override void OnError(ISocket us, string err)
        {
            Debug.LogWarning("异常:" + err);
            OnSocketError(us, err);
        }
    }
}