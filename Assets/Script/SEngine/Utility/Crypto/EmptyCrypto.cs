// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================

using System.Text;

namespace Vavavoom.SIMB.Game.Library.Crypto
{
    public class EmptyCrypto : ICrypto
    {
        public byte[] Encryption(byte[] data)
        {
            return data;
        }

        public string Decryption(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        byte[] ICrypto.Decryption(byte[] data)
        {
            return data;
        }
    }

}

