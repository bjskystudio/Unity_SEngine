local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local UIComBase = require("UIComBase")
local MapManager = require("MapManager")
local LoungeSceneView = require("LoungeSceneView")

---@class FurnitureView : UIComBase 窗口
---@field private go_table FurnitureView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
---@field parts UnityEngine.GameObject[]
---@field OwnerUI LoungeSceneView
local FurnitureView = Class("FurnitureView", UIComBase)

---添加Events监听事件
function FurnitureView:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function FurnitureView:OnCreate(parts, field)
    self.parts = parts
    self.field = field

    --主节点先隐藏
    self.gameObject:SetActive(false)
    local tilePos = MapManager:GetInstance().FurnitureTilePosDic[field]
    if tilePos ~= nil and #tilePos > 0 then
        --添加到排序中
        ---@type LoungeSortInfo
        local sortInfo = {
            Type = LoungeSceneView.eSortType.FurnitureSingle,
            Node = self.gameObject,
            PosX = tilePos[1].x,
            PosY = tilePos[1].y,
            OffsetY = Mathf.Random(1, 100) / 1000
        }
        table.insert(self.OwnerUI.SortInfoList, sortInfo)
    end
    --拆分的资源节点
    for i = 1, #self.parts do
        --先隐藏
        self.parts[i].gameObject:SetActive(false)

        if tilePos ~= nil and #tilePos > 0 then
            --添加到排序中
            ---@type LoungeSortInfo
            local sortInfo = {
                Type = LoungeSceneView.eSortType.FurnitureSingle,
                Node = self.parts[i],
                PosX = tilePos[i].x,
                PosY = tilePos[i].y,
                OffsetY = Mathf.Random(1, 100) / 1000
            }
            table.insert(self.OwnerUI.SortInfoList, sortInfo)
        end
    end
end
---@return SEngine.UI.SImage[]
function FurnitureView:GetImageNode()
    --return { self.transform:GetChild(0):GetComponent(typeof(CS.UnityEngine.UI.Image)) }

    local imageNodes = {}

    for i, v in pairs(self.parts) do
        table.insert(imageNodes, self.parts[i].transform:GetChild(0):GetComponent(typeof(CS.UnityEngine.UI.Image)))
    end
    return imageNodes
end

function FurnitureView:SetNodeActive(alive)
    self.gameObject:SetActive(alive)
    for i, v in pairs(self.parts) do
        self.parts[i]:SetActive(alive)
    end
end

---事件处理
---@private
---@param id EventID 事件ID
function FurnitureView:EventHandle(id, args)
end

---可用
---@protected
function FurnitureView:OnEnable()
end

---不可用
---@protected
function FurnitureView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function FurnitureView:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function FurnitureView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function FurnitureView:OnDestroy()
    --FurnitureView.ParentCls.OnDestroy(self)
end

return FurnitureView