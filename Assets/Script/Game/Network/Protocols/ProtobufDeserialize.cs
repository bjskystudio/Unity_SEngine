// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:52:42
// ========================================================

//协议反串化

using System;
using System.IO;
using Google.Protobuf;
using XLua;

namespace Game.Network
{
    [BlackList]
    public class ProtobufDeserialize : IProtobufDeserialize
    {
        public T Deserialize<T>(string data)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(Type type, MemoryStream source)
        {
            throw new NotImplementedException();
        }

        public object Deserialize(object type, MemoryStream source)
        {
            if (type is IMessage message)
            {
                message.MergeFrom(source);
                return message;
            }
            
            throw new InvalidCastException("the type don't derived from IMessage!");
        }

        public object Deserialize(object type, byte[] source, int offset, int length)
        {
            if (type is IMessage message)
            {
                message.MergeFrom(source, offset, length);
                return message;
            }
            
            throw new InvalidCastException("the type don't derived from IMessage!");
        }
    }
}