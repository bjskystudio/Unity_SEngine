// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/11 14:43:56
// ========================================================


namespace Vavavoom.SIMB.Game.Library.Crypto.LZ4
{
    /// <summary>
    /// Originally this type comes from System.IO.Compression, 
    /// but it is not present in portable assemblies.
    /// </summary>
    public enum LZ4StreamMode
    {
        /// <summary>Compress</summary>
        Compress,

        /// <summary>Decompress</summary>
        Decompress,
    }
}