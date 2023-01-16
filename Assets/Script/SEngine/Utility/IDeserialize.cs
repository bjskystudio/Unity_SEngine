// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:54:02
// ========================================================

//反串化接口

using System;
using System.IO;

namespace Vavavoom.SIMB.Game.Interface
{
    public interface IDeserialize
    {
        T Deserialize<T>(string data);
        object Deserialize(Type type, MemoryStream source);

        object Deserialize(object type, MemoryStream source);

        object Deserialize(object type, byte[] source, int offset, int length);
    }
}