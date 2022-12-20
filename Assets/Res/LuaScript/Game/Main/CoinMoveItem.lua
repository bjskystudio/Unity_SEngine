local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local UIComBase = require("UIComBase")
local UIManager = require("UIManager")

---@class CoinMoveItem : UIComBase 窗口
---@field private go_table CoinMoveItem_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local CoinMoveItem = Class("CoinMoveItem", UIComBase)

---添加Events监听事件
function CoinMoveItem:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function CoinMoveItem:OnCreate()

end

function CoinMoveItem:InitData(num, func)
    self.go_table.stmp_coinStr.text = "+" .. num

    ---@type UnityEngine.Transform
    local trans = self.transform
    -- trans:SetLocalScaleXYZ(0.5)
    --trans.localScale.x = 0.5
    --trans.localScale.y = 0.5
    trans:DOLocalMoveY(trans.position.y + 30, 1):OnComplete(function()
        func()
    end)
end
---事件处理
---@private
---@param id EventID 事件ID
function CoinMoveItem:EventHandle(id, args)
end

---可用
---@protected
function CoinMoveItem:OnEnable()
end

---不可用
---@protected
function CoinMoveItem:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function CoinMoveItem:OnClickBtn(btn)
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function CoinMoveItem:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function CoinMoveItem:OnDestroy()
    --CoinMoveItem.ParentCls.OnDestroy(self)
end

return CoinMoveItem