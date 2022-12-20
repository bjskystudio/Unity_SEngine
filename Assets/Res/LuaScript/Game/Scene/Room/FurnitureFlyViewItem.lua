local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local UIComBase = require("UIComBase")
local TimerInst = require("TimerManager"):GetInstance()

---@class FurnitureFlyViewItem : UIComBase 窗口
---@field private go_table FurnitureFlyViewItem_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local FurnitureFlyViewItem = Class("FurnitureFlyViewItem", UIComBase)

---添加Events监听事件
function FurnitureFlyViewItem:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function FurnitureFlyViewItem:OnCreate()
end

function FurnitureFlyViewItem:InitData(str, iconName, time, func)
    self.go_table.stmp_coinStr.text = str
    self.go_table.img_icon:LoadSprite(GameDefine.eResPath.AtlasMain .. iconName, false, 1)
    self.start = false
    self.gameObject:SetActive(false)
    -- trans:SetLocalScaleXYZ(0.5)
    --trans.localScale.x = 0.5
    --trans.localScale.y = 0.5
    self.Timer = TimerInst:GetTimerStartImme(0.1, self.OnTimer, self)
    self.Time = time
    self.func = func

    self.startCount = true
end
function FurnitureFlyViewItem:OnTimer()
    if (self.startCount) then
        if (self.Time > 0) then
            self.Time = self.Time - 0.25
        end

        if (self.start) then

            self.gameObject:SetActive(true)
            ---@type UnityEngine.Transform
            local trans = self.transform
            trans:DOLocalMoveY(trans.position.y + 80, 1):OnComplete(function()
                self.startCount = false
                self.start = false
                TimerInst:StopAndClearTimer(self.Timer)
                self.transform.gameObject:SetActive(false)
                --self:DestoryComponentGameObj(self.transform.gameObject)
                self.func()
            end)
        else
            if (self.Time <= 0) then
                self.start = true
            end
        end
    end


end

---事件处理
---@private
---@param id EventID 事件ID
function FurnitureFlyViewItem:EventHandle(id, args)
end

---可用
---@protected
function FurnitureFlyViewItem:OnEnable()
end

---不可用
---@protected
function FurnitureFlyViewItem:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function FurnitureFlyViewItem:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function FurnitureFlyViewItem:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function FurnitureFlyViewItem:OnDestroy()
    --FurnitureFlyViewItem.ParentCls.OnDestroy(self)
    self.start = false
    TimerInst:StopAndClearTimer(self.Timer)
end

return FurnitureFlyViewItem