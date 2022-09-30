using SEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lua����Դ���ؾ�̬�����ṩ
/// </summary>
[XLua.LuaCallCSharp]
public static class LuaResLoadHelp
{
    #region ��Դ����

    /// <summary>
    /// �ص���ʽ,����prefab����ʵ����
    /// �ͷŷ�ʽ��GameObject.Destroy�����Զ��������ü����ļ���
    /// </summary>
    /// <param name="path">��Դ·��</param>
    /// <param name="callback">�ص�</param>
    /// <param name="isSync">�Ƿ���ͬ��</param>
    public static void LoadPrefabInstance(int callID, string path, int isSync = 0)
    {
        ResLoadManager.Instance.LoadRes(path,AssetType.ePrefab, (go,resRef) =>
        {
            CSCallLuaHelp.CallLuaGameObject?.Invoke(callID, go as GameObject);
        }, isSync == 1);
    }

    #endregion
}
