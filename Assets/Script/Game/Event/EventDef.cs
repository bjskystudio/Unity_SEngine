using SEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[XLua.LuaCallCSharp]
public static class EventDef
{
    /// <summary>
    /// ��ȡ�豸������Ϣ
    /// </summary>
    public static int SDKGetNotchScreenInfo = EventManager.NewEventId();

    /// <summary>
    /// ��Ϸstartup���
    /// </summary>
    public static int OnGameStartUp = EventManager.NewEventId();
}