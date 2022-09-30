using SEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Lua��<CSCallLua.lua>�󶨽���
/// </summary>
public static class CSCallLuaHelp
{
    /// <summary>
    /// ָ�����ܻص�ӳ���
    /// </summary>
    public readonly static Dictionary<uint, int> FunActionMap = new Dictionary<uint, int>();

    /// <summary>
    /// Lua��ע��ָ�����ܵĻص�ί��ID
    /// </summary>
    /// <param name="funID">��������ID</param>
    /// <param name="luaCallId">lua��ص�ID</param>
    public static void RegisterFunAction(uint funID, int luaCallId)
    {
        if (!FunActionMap.ContainsKey(funID))
        {
            FunActionMap.Add(funID, luaCallId);
        }
    }
    /// <summary>
    /// Lua��ȡ��ע��ָ�����ܵĻص�ί��ID
    /// </summary>
    /// <param name="funID">��������ID</param>
    public static void UnregisterFunAction(uint funID)
    {
        if (FunActionMap.ContainsKey(funID))
        {
            FunActionMap.Remove(funID);
        }
    }
    /// <summary>
    /// ����
    /// </summary>
    public static void ClearFunAction()
    {
        FunActionMap.Clear();
    }

    #region Luaί��IDע�ᵽC#

    public static Action<int> CallLua;
    public static void FunCallLua(FunEnum fun)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLua?.Invoke(id);
        }
    }

    public static Action<int, int> CallLuaInt;
    public static void FunCallLuaInt(FunEnum fun, int parm)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaInt?.Invoke(id, parm);
        }
    }

    public static Action<int, string> CallLuaStr;
    public static void FunCallLuaStr(FunEnum fun, string parm)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaStr?.Invoke(id, parm);
        }
    }

    public static Action<int, float> CallLuaFloat;
    public static void FunCallLuaFloat(FunEnum fun, float parm)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaFloat?.Invoke(id, parm);
        }
    }

    public static Action<int, GameObject> CallLuaGameObject;
    public static void FunCallLuaGameObject(FunEnum fun, GameObject go)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaGameObject?.Invoke(id, go);
        }
    }

    public static Action<int, UnityEngine.Object, SResRef> CallLuaAssetResRef;
    public static void FunCallLuaAsset(FunEnum fun, UnityEngine.Object obj, SResRef resRef)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaAssetResRef?.Invoke(id, obj, resRef);
        }
    }

    public static Action<int, Transform, int> CallLuaTransInt;
    public static void FunCallLuaTransInt(FunEnum fun, Transform trans, int index)
    {
        if (FunActionMap.TryGetValue((uint)fun, out int id))
        {
            CallLuaTransInt?.Invoke(id, trans, index);
        }
    }


    #endregion

    /* ˵����ֵ��������ͬ, Lua��ʹ�ö�Ӧ��intֵ�󶨹��� */

    /// <summary>
    /// ����ö�ٶ���
    /// </summary>
    public enum FunEnum : uint
    {
        Test = 0,
        SubRecallBind = 101,
        SubRecalldestructor = 102,
    }
}