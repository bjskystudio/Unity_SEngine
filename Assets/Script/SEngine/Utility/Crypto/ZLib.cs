// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================

using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.IO.Compression;

namespace Vavavoom.SIMB.Game.Library.Crypto
{
    public class ZLib
    {
        public static byte[] Zip(byte[] unzip,int offset = 0,int len = -1)
        {
            len = len < 0 ? unzip.Length : len;
            var ziped = LZ4.LZ4Codec.Encode(unzip, offset, len);
            var buffer = new byte[ziped.Length + 4];
            Buffer.BlockCopy(ziped, 0, buffer, 4, ziped.Length);
            buffer[0] = (byte)((len >> 24) & 0xFF);
            buffer[1] = (byte)((len >> 16) & 0xFF);
            buffer[2] = (byte)((len >> 8) & 0xFF);
            buffer[3] = (byte)((len >> 0) & 0xFF);
            return buffer;
        }
        public static byte[] UnZip(byte[] zip, int zipLen, out int decodeLen, int offset = 0, int len = -1)
        {
            len = len < 0 ? zipLen : len;
            int length = (int)(((zip[offset + 0] << 24) | (zip[offset + 1] << 16) | (zip[offset + 2] << 8) | (zip[offset + 3] << 0)) & 0xFFFFFFFF);
            return LZ4.LZ4Codec.Decode(zip, offset + 4, len - 4, length, out decodeLen);
        }
    }
}