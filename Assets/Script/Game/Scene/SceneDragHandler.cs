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
    /// ��͸�¼�
    /// </summary>
    public bool Penetrate = false;

    /// <summary>
    /// goTable����
    /// </summary>
    public UIGoTable goTable;

    [CSharpCallLua]
    public delegate void LuaBeginDragAction(LuaTable t, PointerEventData eventData);
    [CSharpCallLua]
    public delegate void LuaDragAction(LuaTable t, PointerEventData eventData);
    [CSharpCallLua]
    public delegate void LuaEndDragAction(LuaTable t, PointerEventData eventData);

    /// <summary>
    /// View Lua�����Ӧ�İ�ť��Ӧ�¼�
    /// </summary>
    [CSharpCallLua]
    public static LuaBeginDragAction luaOnBeginDrag;
    /// <summary>
    /// View Lua�����Ӧ��Toggle��Ӧ�¼�
    /// </summary>
    [CSharpCallLua]
    public static LuaDragAction luaOnDrag;
    /// <summary>
    /// View Lua�����Ӧ�ĳ�������Ӧ�¼�
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
    /// ��͸���
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
