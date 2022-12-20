

return _G.ConstClass("game_config",{
    Res_GoldAutoRaise = { Key = "Res_GoldAutoRaise", Desc = "挂机金币收益每秒", paramNum = 10, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Res_GoldStart = { Key = "Res_GoldStart", Desc = "开始金币", paramNum = 100, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Res_DiamondStart = { Key = "Res_DiamondStart", Desc = "开始钻石", paramNum = 10000, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Res_PopularStart = { Key = "Res_PopularStart", Desc = "开始人气", paramNum = 0, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Res_GhostStart = { Key = "Res_GhostStart", Desc = "开始幽灵", paramNum = 0, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Max_energy = { Key = "Max_energy", Desc = "体力最大值", paramNum = 50, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Energy_Recover_Time = { Key = "Energy_Recover_Time", Desc = "体力恢复需要的时间（秒）", paramNum = 600, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Energy_Recover_Amount = { Key = "Energy_Recover_Amount", Desc = "体力恢复量", paramNum = 5, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Click_Number = { Key = "Click_Number", Desc = "常规情况下扭蛋机的点击次数", paramNum = 3, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Diamond_Upgreade_Time = { Key = "Diamond_Upgreade_Time", Desc = "钻石升级，1钻石等于多少加速（s）", paramNum = 30, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Diamond_Upgreade_Gold = { Key = "Diamond_Upgreade_Gold", Desc = "钻石升级，1钻石等于多少金币", paramNum = 60, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    gashapon_machine_sp_interval = { Key = "gashapon_machine_sp_interval", Desc = "扭蛋机特殊状态出现时间间隔（秒）", paramNum = 300, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    Gems_Energy_Recover = { Key = "Gems_Energy_Recover", Desc = "观看广告和花费钻石恢复的体力量", paramNum = 50, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    AD_Count = { Key = "AD_Count", Desc = "每日可观看广告的次数", paramNum = 5, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    AD_Gems_Cost = { Key = "AD_Gems_Cost", Desc = "钻石恢复体力的花费", paramNum = 0, paramNumArr = { 10, 15, 20, 25, 30 }, paramNumArrs = {}, paramStr = "" },
    Initial_Acceleration_Time = { Key = "Initial_Acceleration_Time", Desc = "初始加速时间（s）", paramNum = 900, paramNumArr = {}, paramNumArrs = {}, paramStr = "" },
    waitqueue_room1001 = { Key = "waitqueue_room1001", Desc = "休息室室外排列", paramNum = 0, paramNumArr = {}, paramNumArrs = { { -10, -17 }, { -13, -18 }, { -16, -19 }, { -18, -22 }, { -20, -24 } }, paramStr = "" },
    client_wayout = { Key = "client_wayout", Desc = "顾客室外消失点", paramNum = 0, paramNumArr = {}, paramNumArrs = { { 10, -51, 1, 1 } }, paramStr = "" },
    lounge_door = { Key = "lounge_door", Desc = "休息室入口", paramNum = 0, paramNumArr = {}, paramNumArrs = { { -24, -31 } }, paramStr = "" },
    takingbubble_weight = { Key = "takingbubble_weight", Desc = "聊天气泡触发", paramNum = 50, paramNumArr = {}, paramNumArrs = {}, paramStr = "" }
})