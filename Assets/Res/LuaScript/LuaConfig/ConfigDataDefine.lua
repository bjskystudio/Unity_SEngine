---@field public room_config room_config_Item[]
---@field public language language
---@field public items items_Item[]
---@field public hotleconfig hotleconfig_Item[]
---@field public hotlebuild hotlebuild_Item[]
---@field public gashapon_machine_sp gashapon_machine_sp_Item[]
---@field public gashapon_machine_config gashapon_machine_config_Item[]
---@field public game_config game_config
---@field public furniture_properties furniture_properties_Item[]
---@field public furniture_level furniture_level_Item[]
---@field public furniture furniture_Item[]
---@field public expression_bubble expression_bubble_Item[]
---@field public customer_skill customer_skill
---@field public customer_config customer_config_Item[]
---@class ConfigManager
_G.ConfigList = {
    customer_config = {"customer_config"},
    customer_skill = {"customer_skill"},
    expression_bubble = {"expression_bubble"},
    furniture = {"furniture"},
    furniture_level = {"furniture_level"},
    furniture_properties = {"furniture_properties"},
    game_config = {"game_config"},
    gashapon_machine_config = {"gashapon_machine_config"},
    gashapon_machine_sp = {"gashapon_machine_sp"},
    hotlebuild = {"hotlebuild"},
    hotleconfig = {"hotleconfig"},
    items = {"items"},
    language = {"language"},
    room_config = {"room_config"},
}