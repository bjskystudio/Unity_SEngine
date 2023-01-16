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
    public interface ICrypto
    {
        byte[] Encryption(byte[] data);
        byte[] Decryption(byte[] data);
    }
}
