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
    public class AesCrypto : ICrypto
    {
        public string Key { get; set; }
        public string InitialVector { get; set; }

        public AesCrypto(string key, string iv)
        {
            Key = key;
            InitialVector = iv;
        }

        public byte[] Encryption(byte[] data)
        {
            return AesUtility.Encrption(data, Key, InitialVector);
        }

        public byte[] Decryption(byte[] data)
        {
            return AesUtility.Decrption(data, Key, InitialVector);
        }
    }
}
