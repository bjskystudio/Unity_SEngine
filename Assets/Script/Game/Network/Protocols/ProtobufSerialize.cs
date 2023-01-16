// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:26:32
// ========================================================

//协议串化

using System;
using Google.Protobuf;
using XLua;

namespace Game.Network
{
    [BlackList]
    public class ProtobufSerialize : IProtobufSerialize
    {
        public byte[] Serialize<T>(T data) 
        {
            if (data is IMessage message)
            {
                return message.ToByteArray();
            }

            throw new InvalidCastException("the type don't derived from IMessage!");
        }
    }
}


