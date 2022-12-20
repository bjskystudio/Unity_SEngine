---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/10/18 19:54
---@class
local UIComBase = require("UIComBase")

---@class ListItemBase : listItem组件
---@field private go_table any GoTable
---@field private ParentCls UIComBase 父窗口类
---@field private ItemInitComplete boolean 已经初始化
local ListItemBase = Class("ListItemBase", UIComBase)


function ListItemBase:OnBaseAwake()
    self.ItemInitComplete = false
    -- 其他base的扩展
    self:Awake()
end
---初始化列表项数据(内部）
---@private
---@param data any 项目数据
---@param index number 列表index
---@param listData any[] 数据列表
---@param ... any @列表项目的参数
---@return
function ListItemBase:_InitItemData(data,index,listData,...)
    if not self.ItemInitComplete then
        self:OnInitItemData(data,index,listData,...)
        self.ItemInitComplete = true
    end
    self:OnRefreshItemData(data,index,listData,...)
end

---初始化列表项数据(只有在创建的时候调用)
---@override
---@param data any 项目数据
---@param index number 列表index
---@param listData any[] 数据列表
---@param ... any @列表项目的参数
---@return
function ListItemBase:OnInitItemData(data,index,listData,...)

end
---刷新列表项数据
---@override
---@param data any 项目数据
---@param index number 列表index
---@param listData any[] 数据列表
---@param ... any @列表项目的参数
---@return
function ListItemBase:OnRefreshItemData(data,index,listData,...)

end

return ListItemBase