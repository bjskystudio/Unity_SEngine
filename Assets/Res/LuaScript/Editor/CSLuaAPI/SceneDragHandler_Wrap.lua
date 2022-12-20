---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SceneDragHandler : UnityEngine.MonoBehaviour
---@field public Penetrate System.Boolean
---@field public goTable UIGoTable
---@field static luaOnBeginDrag SceneDragHandler.LuaBeginDragAction
---@field static luaOnDrag SceneDragHandler.LuaDragAction
---@field static luaOnEndDrag SceneDragHandler.LuaEndDragAction
local SceneDragHandler = {}

---@param eventData UnityEngine.EventSystems.PointerEventData
function SceneDragHandler:OnBeginDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function SceneDragHandler:OnDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function SceneDragHandler:OnEndDrag(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function SceneDragHandler:OnPointerClick(eventData) end

---@param eventData UnityEngine.EventSystems.PointerEventData
function SceneDragHandler:PenetrateEvent(eventData) end

return SceneDragHandler
