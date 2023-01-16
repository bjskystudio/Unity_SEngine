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
    [CSharpCallLua]
    public delegate void LuaZoomAction(LuaTable t, float zoomDelta);

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
    /// <summary>
    /// View Lua代码对应的超链接响应事件
    /// </summary>
    [CSharpCallLua]
    public static LuaZoomAction luaOnZoom;

    private bool IsZoom = false;

    private void Awake()
    {
        
    }

    private void LateUpdate()
    {
        if (IsZoom && Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);

            Vector2 touch0PrevPos = touch0.position - touch0.deltaPosition;
            Vector2 touch1PrevPos = touch1.position - touch1.deltaPosition;

            float prevTouchDelta = (touch0PrevPos - touch1PrevPos).magnitude;
            float touchDelta = (touch0.position - touch1.position).magnitude;

            float zoomDelta = prevTouchDelta - touchDelta;

            if (zoomDelta > 0.01f || zoomDelta < -0.01f)
            {
                //Log.Debug("Unity Zoom:" + zoomDelta);
                luaOnZoom?.Invoke(goTable.GetLuaGoTable(), zoomDelta);
            }

        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (Input.touchCount >= 2)
        {
            //Log.Debug("EndDrag,touch count:" + Input.touchCount);
            IsZoom = Input.touchCount == 2;
            return;
        }
        if (goTable)
        {
            luaOnBeginDrag?.Invoke(goTable.GetLuaGoTable(), eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (Input.touchCount >= 2)
            return;
        if (goTable)
        {
            luaOnDrag?.Invoke(goTable.GetLuaGoTable(), eventData);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Input.touchCount >= 2)
        {
            IsZoom = Input.touchCount == 2;
            return;
        }
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
