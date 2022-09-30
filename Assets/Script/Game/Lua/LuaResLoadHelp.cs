using SEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lua侧资源加载静态方法提供
/// </summary>
[XLua.LuaCallCSharp]
public static class LuaResLoadHelp
{
    #region 资源加载

    /// <summary>
    /// 回调方式,加载prefab并且实例化
    /// 释放方式：GameObject.Destroy，会自动处理引用计数的减少
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="callback">回调</param>
    /// <param name="isSync">是否是同步</param>
    public static void LoadPrefabInstance(int callID, string path, int isSync = 0)
    {
        ResLoadManager.Instance.LoadRes(path,AssetType.ePrefab, (go,resRef) =>
        {
            CSCallLuaHelp.CallLuaGameObject?.Invoke(callID, go as GameObject);
        }, isSync == 1);
    }

    #endregion
}
