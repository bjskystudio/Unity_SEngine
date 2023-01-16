local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local ConfigManager = require("ConfigManager")
---@class DeviceConfirmView : PopBase 窗口
---@field private go_table DeviceConfirmView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceConfirmView = Class("DeviceConfirmView", PopBase)

---添加Events监听事件
function DeviceConfirmView:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function DeviceConfirmView:OnCreate(func, costId)
    self.func = func
    self.ClickSpaceClose = true
    self.go_table.stmp_title = LanguageUtil:GetValue("Common_Title")
    self.go_table.stmp_unlockStr.text = LanguageUtil:GetValue("Common_Sure")
    self.go_table.stmp_LockStr = LanguageUtil:GetValue("Common_Cancel")
    if (costId ~= nil) then
        self.go_table.stmp_Message.text = string.format(LanguageUtil:GetValue("Common_hint_usejewel") .. LanguageUtil:GetValue(ConfigManager.items[costId].name) .. "?")
    end
end

---事件处理
---@private
---@param id EventID 事件ID
function DeviceConfirmView:EventHandle(id, args)
end

---可用
---@protected
function DeviceConfirmView:OnEnable()
end

---不可用
---@protected
function DeviceConfirmView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceConfirmView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end
    if (btn == self.go_table.sbtn_confirm) then
        self.func();
    end
    self:Close()
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceConfirmView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function DeviceConfirmView:OnDestroy()
    --DeviceConfirmView.ParentCls.OnDestroy(self)
end

return DeviceConfirmView