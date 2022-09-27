using SEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.LuaCallCSharp]
public static class EventDef
{
    /// <summary>
    /// 获取设备刘海信息
    /// </summary>
    public static int SDKGetNotchScreenInfo = EventManager.NewEventId();

    /// <summary>
    /// 游戏startup完成
    /// </summary>
    public static int OnGameStartUp = EventManager.NewEventId();
}