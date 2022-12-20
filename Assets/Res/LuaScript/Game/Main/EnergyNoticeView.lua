local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")

---@class EnergyNoticeView : PopBase 窗口
---@field private go_table EnergyNoticeView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local EnergyNoticeView = Class("EnergyNoticeView", PopBase)

---添加Events监听事件
function EnergyNoticeView:Awake()
    -- self.ClickSpaceClose = true
end
--- 窗口显示[protected]
---@param ... any @窗口传参
function EnergyNoticeView:OnCreate()
    self.go_table.stmp_confirmStr.text = LanguageUtil:GetValue("Common_Sure")
    self.go_table.stmp_message.text = LanguageUtil:GetValue("Common_Energy_Recover_03")
    self.go_table.stmp_smallTitle.text = LanguageUtil:GetValue("Common_Energy_Recover_tips")
end

---事件处理
---@private
---@param id EventID 事件ID
function EnergyNoticeView:EventHandle(id, args)
end

---可用
---@protected
function EnergyNoticeView:OnEnable()
end

---不可用
---@protected
function EnergyNoticeView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function EnergyNoticeView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end
    if (btn == self.go_table.sbtn_confirm) then
        self:Close()
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function EnergyNoticeView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function EnergyNoticeView:OnDestroy()
    --EnergyNoticeView.ParentCls.OnDestroy(self)
end

return EnergyNoticeView