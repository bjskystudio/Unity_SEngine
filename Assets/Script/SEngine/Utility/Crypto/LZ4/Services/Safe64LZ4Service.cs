// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================

namespace Vavavoom.SIMB.Game.Library.Crypto.LZ4.Services
{
    internal class Safe64LZ4Service: ILZ4Service
    {
        #region ILZ4Service Members

        public string CodecName
        {
            get { return "Safe 64"; }
        }

        public int Encode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
        {
            return LZ4ps.LZ4Codec.Encode64(input, inputOffset, inputLength, output, outputOffset, outputLength);
        }

        public int Decode(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength, bool knownOutputLength)
        {
            return LZ4ps.LZ4Codec.Decode64(input, inputOffset, inputLength, output, outputOffset, outputLength, knownOutputLength);
        }

        public int EncodeHC(byte[] input, int inputOffset, int inputLength, byte[] output, int outputOffset, int outputLength)
        {
            return LZ4ps.LZ4Codec.Encode64HC(input, inputOffset, inputLength, output, outputOffset, outputLength);
        }

        #endregion
    }
}