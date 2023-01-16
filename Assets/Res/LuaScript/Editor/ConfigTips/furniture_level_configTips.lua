---@class furniture_level_Item
---@field public id number @唯一di
---@field public furniture_id number @对应家具表
---@field public desc string @说明（不读取）
---@field public level number @等级
---@field public name string @物品名称
---@field public explain string @物品说明
---@field public icon string @界面中的图标
---@field public art string @场景中的美术资源
---@field public unlock number @是否默认解锁
---@field public term_type number[] @解锁条件（8：需求人气 9：需求房间  10：需求前置道具）
---@field public term_num number[] @解锁参数
---@field public cost_normal number[] @消耗1.金币#数量”
---@field public waitstime number @运输时间（s)
---@field public param_type number[] @家具收益类型（1.房间订单收益/2.挂机收益/3.使用收益/4.额外收益/5.人气值/6.库存数量/7.交互时间减少）
---@field public param_num number[] @收益数值
---@field public talk_tage number[] @角色在使用设备时说的话


