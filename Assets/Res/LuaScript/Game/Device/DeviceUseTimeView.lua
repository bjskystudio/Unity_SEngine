local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local GameDataInst = require("GameData"):GetInstance()
local UIManager = require("UIManager")
local UIInst = require("UIManager"):GetInstance()
local DeviceDataInst = require("DeviceData"):GetInstance()
local TimeUtil = require("TimeUtil")
---@class DeviceUseTimeView : PopBase 窗口
---@field private go_table DeviceUseTimeView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceUseTimeView = Class("DeviceUseTimeView", PopBase)

---添加Events监听事件
function DeviceUseTimeView:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function DeviceUseTimeView:OnCreate(id, func)
    self.func = func
    self.ClickSpaceClose = true
    self.go_table.stmp_title = LanguageUtil:GetValue("Common_Title")
    self.go_table.stmp_unlockStr.text = LanguageUtil:GetValue("Common_button_speed_Sure")
    self.go_table.stmp_LockStr = LanguageUtil:GetValue("Common_button_speed_cancel")
    self.fid = id
    -- self.go_table.stmp_Message.text = string.format(LanguageUtil:GetValue("Common_hint_usejewel") .. "?")
end

---事件处理
---@private
---@param id EventID 事件ID
function DeviceUseTimeView:EventHandle(id, args)
end

---可用
---@protected
function DeviceUseTimeView:OnEnable()
end

---不可用
---@protected
function DeviceUseTimeView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceUseTimeView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end

    if (btn == self.go_table.sbtn_confirm) then
        --self.func();
        local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.SpeedTime)
        local time = DeviceDataInst:GetFurnitureTiming(self.fid)
        local needStr = TimeUtil.TimeToString(time, 3, false)
        UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needStr)
        return
    elseif btn == self.go_table.sbtn_cancle then
        if (GameDataInst.SpeedTime <= 0) then

            --提示时间不足 TODO
            --[[            local setting = {
                            Layer = UILayerEnum.InfoLayer,
                            ShowBgMask = false

                        }
                        UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceCommonTip, setting, LanguageUtil:GetValue("Common_Title_nospeedup"))]]

            local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.SpeedTime)
            local time = DeviceDataInst:GetFurnitureTiming(self.fid)
            local needStr = TimeUtil.TimeToString(time, 3, false)
            UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needStr)
            return
        end
        self.func();
    end
    self:Close()
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceUseTimeView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function DeviceUseTimeView:OnDestroy()
    --DeviceUseTimeView.ParentCls.OnDestroy(self)
end

return DeviceUseTimeView