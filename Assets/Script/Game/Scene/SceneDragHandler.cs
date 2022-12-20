using SEngine;
using SEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XLua;

public class SceneDragHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler,IPointerClickHandler
{
    /// <summary>
    /// 穿透事件
    /// </summary>
    public bool Penetrate = false;

    /// <summary>
    /// goTable对象
    /// </summary>
    public UIGoTable goTable;

    [CSharpCallLua]
    public delegate void LuaBeginDragAction(LuaTable t, PointerEventData eventData);
    [CSharpCallLua]
    public delegate void LuaDragAction(LuaTable t, PointerEventData eventData);
    [CSharpCallLua]
    public delegate void LuaEndDragAction(LuaTable t, PointerEventData eventData);

    /// <summary>
    /// View Lua代码对应的按钮响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaBeginDragAction luaOnBeginDrag;
    /// <summary>
    /// View Lua代码对应的Toggle响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaDragAction luaOnDrag;
    /// <summary>
    /// View Lua代码对应的超链接响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaEndDragAction luaOnEndDrag;

    private void Awake()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (goTable)
        {
            luaOnBeginDrag?.Invoke(goTable.GetLuaGoTable(), eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (goTable)
        {
            luaOnDrag?.Invoke(goTable.GetLuaGoTable(), eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (goTable)
        {
            luaOnEndDrag?.Invoke(goTable.GetLuaGoTable(), eventData);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Log.Debug("click");
        OnClickHandle(eventData);
    }

    protected virtual void OnClickHandle(PointerEventData eventData)
    {
        if (Penetrate)
        {
            PenetrateEvent(eventData);
        }
    }

    /// <summary>
    /// 穿透点击
    /// </summary>
    /// <param name="eventData"></param>
    public void PenetrateEvent(PointerEventData eventData)
    {
        List<RaycastResult> result = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, result);
        GameObject cur = eventData.pointerCurrentRaycast.gameObject;
        for (int i = 0; i < result.Count; i++)
        {
            if (cur != result[i].gameObject)
            {
                if (result[i].gameObject.GetComponent<SButton>())
                {
                    result[i].gameObject.GetComponent<SButton>().OnPointerClick(eventData);
                }
                if (result[i].gameObject.GetComponent<Button>())
                {
                    result[i].gameObject.GetComponent<Button>().OnPointerClick(eventData);
                }
            }
        }
    }
}
