// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================

namespace Vavavoom.SIMB.Game.Library.Crypto.LZ4
{
    internal interface ILZ4Service
    {
        string CodecName { get; }
        int Encode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength);
        int EncodeHC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength);
        int Decode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength);
    }
}