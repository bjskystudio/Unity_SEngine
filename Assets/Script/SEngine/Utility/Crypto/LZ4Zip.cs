// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vavavoom.SIMB.Game.Library.Crypto
{
    public class LZ4Zip : IZip
    {

        public LZ4Zip()
        {
            
        }

        public byte[] Encode(short cmd, byte[] data)
        {
            return ZLib.Zip(data);
        }

        public byte[] Decode(short cmd, byte[] data, int len, out int decodeLen)
        {
            return ZLib.UnZip(data, len, out decodeLen);
        }
    }
}
