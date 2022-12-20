local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local TimerInst = require("TimerManager"):GetInstance()

---@class DeviceCommonTip : PopBase 窗口
---@field private go_table DeviceCommonTip_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceCommonTip = Class("DeviceCommonTip", PopBase)

---添加Events监听事件
function DeviceCommonTip:Awake()

end
--- 窗口显示[protected]
---@param str string @窗口传参
function DeviceCommonTip:OnCreate(str)
    --开启点击窗口外关闭弹窗
    --self.ClickSpaceClose = true

    self.tipStr = str
    self.go_table.stmp_Tipstr.text = self.tipStr

    local fitter = self.go_table.stmp_Tipstr.gameObject:GetComponent(typeof(CS.UnityEngine.UI.ContentSizeFitter))
    fitter:SetLayoutHorizontal()
    local w = self.go_table.stmp_Tipstr.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).sizeDelta.x

    self.go_table.img_bg.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)):SetSizeWithCurrentAnchors(CS.UnityEngine.RectTransform.Axis.Horizontal, w + 50)

    self.count = 0
    self.MaxCount = 2
    self.Timer = TimerInst:GetTimerStartImme(1, self.OnTimer, self)
end
function DeviceCommonTip:OnTimer()

    self.count = self.count + 1
    if (self.count >= self.MaxCount) then
        TimerInst:StopAndClearTimer(self.Timer)
        self:Close()
    end
end

---事件处理
---@private
---@param id EventID 事件ID
function DeviceCommonTip:EventHandle(id, args)
end

---可用
---@protected
function DeviceCommonTip:OnEnable()
end

---不可用
---@protected
function DeviceCommonTip:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceCommonTip:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceCommonTip:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function DeviceCommonTip:OnDestroy()
    --DeviceCommonTip.ParentCls.OnDestroy(self)
end

return DeviceCommonTip