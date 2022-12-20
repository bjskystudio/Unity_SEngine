local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local UIComBase = require("UIComBase")

---@class GoldFly : UIComBase 窗口
---@field private go_table GoldFly_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local GoldFly = Class("GoldFly", UIComBase)

---添加Events监听事件
function GoldFly:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function GoldFly:OnCreate()

end

function GoldFly:InitData(iconName)
    self.go_table.img_image:LoadSprite(GameDefine.eResPath.AtlasMain .. iconName, false, 1)
end

---事件处理
---@private
---@param id EventID 事件ID
function GoldFly:EventHandle(id, args)
end

---可用
---@protected
function GoldFly:OnEnable()
end

---不可用
---@protected
function GoldFly:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function GoldFly:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function GoldFly:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function GoldFly:OnDestroy()
    --GoldFly.ParentCls.OnDestroy(self)
end

return GoldFly