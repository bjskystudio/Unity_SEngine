// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/11/10 15:51:23
// ========================================================

//串化接口

namespace Vavavoom.SIMB.Game.Interface
{
    public interface ISerialize
    {
        byte[] Serialize<T>(T data);
    }
}