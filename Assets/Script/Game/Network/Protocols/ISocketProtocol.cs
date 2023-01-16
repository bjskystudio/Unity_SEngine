// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:58:58
// ========================================================

//套接字+协议

using System;
using SEngine.Net;
using Google.Protobuf;
using XLua;

namespace Game.Network
{
    public enum ProtocolEnum
    {
        NONE,
    }

    [BlackList]
    public interface ISocketProtocol
    {
        //编码协议
        Frame EncodeMessage(ProtocolEnum cmd, IMessage message, bool kcp);
        //编码协议
        Frame EncodeMessage(short cmd, byte[] data, bool kcp);
        //解码协议
        object DecodeMessage(Type type, byte[] data, int len, ushort controlFlag = 0);
        //解压
        byte[] DecodeCompress(short cmd, byte[] compressData, int len, out int decodeLen);
    }
}


