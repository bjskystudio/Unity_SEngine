local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local DeviceDataInst = require("DeviceData"):GetInstance()
local GameDefine = require("GameDefine")
local ListItemBase = require("ListItemBase")
local TimerInst = require("TimerManager"):GetInstance()
local TimeUtil = require("TimeUtil")
local DeviceData = require("DeviceData")
local EventInst = require("EventManager"):GetInstance()

---@class DeviceMesItem : UIBase 窗口
---@field private go_table DeviceMesItem_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceMesItem = Class("DeviceMesItem", ListItemBase)

---添加Events监听事件
function DeviceMesItem:Awake()
    self:AddEvent(GameEvent.UpGradeFurnitureEvent)
    self:AddEvent(GameEvent.FurnitureUnlockByTimeEvent)
end
--- 窗口显示[protected]
---@param conditionData DeviceData.ConditionData
function DeviceMesItem:OnCreate(conditionData)
    self.conditionData = conditionData
    print(self.conditionData.ImageName)
    self.go_table.img_szMesImg:LoadSprite(GameDefine.eResPath.AtlasDevice .. self.conditionData.ImageName, false, 1)

    self.go_table.stmp_szMesContent.text = self.conditionData.ConditionStr

    self.furnitureId = conditionData.FurnitureId
    self.isTime = conditionData.IsTime

    self.isUpdate = DeviceDataInst:FurnitureIsOnTheWay(self.furnitureId)
    local str = LanguageUtil:GetValue("Commom_timeremaining") .. " "

    --初始图片
    self.timeImg = "ui_icon_yunshu_lock_60.60"

    self.str = str
    if (self.isTime and self.isUpdate) then
        self.Timer = TimerInst:GetTimerStart(1, self.OnUpdate, self)
        self:OnUpdate()
    end
end
function DeviceMesItem:RefreshItemData()
    if (self.isTime) then
        if (self.Timer ~= nil) then
            self.Timer = nil

            --TimerInst:StopAndClearTimer(self.Timer)
        end

        self.Timer = TimerInst:GetTimerStart(1, self.OnUpdate, self)
    end
end

function DeviceMesItem:OnUpdate()
    if (self.Timer == nil) then
        return
    end
    if (self.isUpdate) then
        local timeStr
        local time = DeviceDataInst:GetFurnitureTiming(self.furnitureId)
        if (time <= 0) then
            TimerInst:StopAndClearTimer(self.Timer)
            self.Timer = nil
            self.isUpdate = false
            self.go_table.img_szMesImg:LoadSprite(GameDefine.eResPath.AtlasDevice .. self.conditionData.ImageName, false, 1)
            self.go_table.stmp_szMesContent.text = self.conditionData.ConditionStr
            return
        end
        --时间条件
        --local timeStr = string.format(str .. "<color=#AD6692>%s</color>", TimeUtil.TimeToString(self.furnitureData.waitstime, 3, false))
        timeStr = string.format("<color=#F66D93>" .. self.str .. "%s</color>", TimeUtil.TimeToString(time, 3, false))
        self.go_table.stmp_szMesContent.text = timeStr
        self.go_table.img_szMesImg:LoadSprite(GameDefine.eResPath.AtlasDevice .. self.timeImg, false, 1)

    end
end
---事件处理
---@private
---@param id EventID 事件ID
function DeviceMesItem:EventHandle(id, ...)
    local tab = { ... }
    local fid = tab[1]
    if (id == GameEvent.UpGradeFurnitureEvent) then
        local type = tab[2]
        --当前家具 普通解锁
        if (type == DeviceData.UnlockType.Normal and fid == self.furnitureId and self.isTime) then
            local isOnTheWay = DeviceDataInst:FurnitureIsOnTheWay(fid)

            self.isUpdate = isOnTheWay
            if (isOnTheWay) then
                if (self.Timer ~= nil) then
                    self.Timer = nil
                end
                self:OnUpdate()
                self.Timer = TimerInst:GetTimerStart(1, self.OnUpdate, self)
            end
        end

    elseif id == GameEvent.FurnitureUnlockByTimeEvent then
--[[        local isOver = tab[2]
        if (isOver) then
            self:OnInitItemData(self.conditionData)
        end]]

    end
end

---可用
---@protected
function DeviceMesItem:OnEnable()
end

---不可用
---@protected
function DeviceMesItem:OnDisable()

end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceMesItem:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceMesItem:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function DeviceMesItem:OnDestroy()
    --DeviceMesItem.ParentCls.OnDestroy(self)
    if (self.Timer ~= nil) then
        TimerInst:StopAndClearTimer(self.Timer)
        self.Timer = nil
        self.isUpdate = false
        --
    end

end

return DeviceMesItem