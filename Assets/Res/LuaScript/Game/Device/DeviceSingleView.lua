local PopBase = require("PopBase")
local GameEvent = require("GameEvent")
local Log = require("Log")

---@class DeviceSingleView : PopBase 窗口
---@field private go_table DeviceSingleView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceSingleView = Class("DeviceSingleView", PopBase)

---添加Events监听事件
function DeviceSingleView:Awake()

end
--- 窗口显示[protected]
---@param furnitureData furniture_Item @窗口传参
function DeviceSingleView:OnCreate(furnitureData)
    --开启点击窗口外关闭弹窗
    self.ClickSpaceClose = true
    local spriteName = furnitureData.icon
    self.furnitureData = furnitureData;
    self.go_table.img_buildImg:LoadSprite(GameDefine.eResPath.AtlasFurniture .. spriteName, false, 1, function()
        -- print("详情界面动态图片加载完成")
    end)
end

---事件处理
---@private
---@param id EventID 事件ID
function DeviceSingleView:EventHandle(id, args)
end

---可用
---@protected
function DeviceSingleView:OnEnable()
end

---不可用
---@protected
function DeviceSingleView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceSingleView:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceSingleView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function DeviceSingleView:OnDestroy()
    --DeviceSingleView.ParentCls.OnDestroy(self)
end

return DeviceSingleView