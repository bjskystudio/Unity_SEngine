local EventInst = require("EventManager"):GetInstance()
local GameEvent = require("GameEvent")
local PlayerPrefsUtil = require("PlayerPrefsUtil")
local ConfigManager = require("ConfigManager")
local GameDataInst = require("GameData"):GetInstance()
local GameDefine = GameDefine
local HotelData = require("HotelData")
local TimerInst = require("TimerManager"):GetInstance()
local TimeUtil = require("TimeUtil")
local UIManager = require("UIManager")
---游戏公共数据管理器（单例）
---@class DeviceData :Singleton
local DeviceData = Class("DeviceData", Singleton)

---@class DeviceData.eDevicePropType 设备属性枚举
---@field RoomIncome number 房间收益
---@field PassiveIncome number 被动收益
---@field UseIncome number 使用收益
---@field ExtIncome number 额外收益
---@field GivePopular number 提供人气
---@field ExtIncome number 库存数量
---@field TimeRest number 交互时间减少
DeviceData.eDevicePropType = {
    RoomIncome = 1,
    PassiveIncome = 2,
    UseIncome = 3,
    ExtIncome = 4,
    StoredCount = 6,
    GivePopular = 5,
    TimeRest = 8,
}
---@private
function DeviceData:__init()
    ---初始化本地数据

    self:InitLocalData()

    --开启倒计时
    self:StartTimer()
    --self:CheckNewFurniture()
    --场景点击家具解锁
    EventInst:AddListener(GameEvent.RoomFurnitureFinishTransport, self.DealClickFurniture, self)
end
---检查是否有家具为new
function DeviceData:CheckNewFurniture()
    for houseId, houseData in pairs(self.HouseData) do

        ---@type DeviceFieldData[]
        for i, fieldData in pairs(houseData) do

            ---@type furniture_level_Item[]
            local furnitureData = ConfigManager.GetConfigByField(ConfigManager.furniture_level, "furniture_id", { i })

            for i, data in pairs(furnitureData) do
                local furnitureId = data.id

                local houseId = self:GetHouseId(furnitureId)
                local fieldId = self:GetFieldId(furnitureId)
                local isUnLock = self:FurnitureIsUnlock(houseId, fieldId, furnitureId)--当前家具是否解锁
                if (isUnLock) then
                else
                    local could = self:FurnitureCouldUnlock(furnitureId)--是否满足解锁条件
                    if (could) then
                        local isBought = self:FurnitureUnlockButWait(furnitureId)
                        if (isBought) then
                        else
                            if (fieldData.NewUnlockFurnitureIds == nil) then
                                fieldData.NewUnlockFurnitureIds = {}
                            end
                            if (fieldData.NewUnlockFurnitureIds[furnitureId] == nil) then
                                fieldData.NewUnlockFurnitureIds[furnitureId] = 0 --NEW
                            end
                        end
                    else
                    end
                end
            end
        end
    end

    self:SaveLocalData()
end

--把new标记 去除
function DeviceData:SetOldFurniture(furnitureId)
    local houseId = self:GetHouseId(furnitureId)
    local fieldId = self:GetFieldId(furnitureId)
    local isUnLock = self:FurnitureIsUnlock(houseId, fieldId, furnitureId)--当前家具是否解锁
    if (isUnLock) then
    else
        local could = self:FurnitureCouldUnlock(furnitureId)--是否满足解锁条件
        if (could) then
            local isBought = self:FurnitureUnlockButWait(furnitureId)
            if (isBought) then
            else
                local fieldData = self.HouseData[houseId][fieldId]
                if (fieldData.NewUnlockFurnitureIds == nil) then
                    fieldData.NewUnlockFurnitureIds = {}
                end
                fieldData.NewUnlockFurnitureIds[furnitureId] = 1
            end
        else
        end
    end
end

function DeviceData:DealClickFurniture(furnitureId)
    local houseId = self:GetHouseId(furnitureId)
    local field = self:GetFieldId(furnitureId)

    --添加到已解锁家具列表中
    self:DeleteFurnitureOnTheWay(houseId, field, furnitureId)

    --添加为正在使用的家具
    self:SaveUsingFurniture(houseId, field, furnitureId)
    local myPop = GameDataInst.Popular

    --刷新人气值
    GameDataInst:RefreshPopular()

    --人气值是否发生了改变
    self.IsPopularChange = myPop ~= GameDataInst.Popular
    --判断开启New图片的家具
    if (self.IsPopularChange) then
        self:CheckNewFurniture()
    end

end
--是否是标识为新家具
function DeviceData:IsNewFurniture(houseId, field, furnitureId)

    if (furnitureId == 100501) then
        print("检测到指定id")
    end
    ---@type DeviceFieldData[]
    local fieldDataArray = self:GetunLockFurniture_fieldData(houseId)
    local fieldData = fieldDataArray[field]

    local isNew = false
    if (fieldData == nil) then
        return isNew
    end
    if (fieldData.NewUnlockFurnitureIds == nil) then
        return isNew
    end

    if (fieldData.NewUnlockFurnitureIds[furnitureId] ~= nil and fieldData.NewUnlockFurnitureIds[furnitureId] == 0) then
        isNew = true
        --print("是新的")
    end
    --fieldData.NewUnlockFurnitureIds[furnitureId] = 1
    --self:SaveLocalData()

    return isNew
end


--当前家具产生的收益 包含订单收益或使用收益
function DeviceData:FurnitureRoomIncom(roomId, fieldId, incomType)
    local inCom = 0
    local houseFieldData = self:GetHouseData(roomId)
    if (incomType == DeviceData.eDevicePropType.RoomIncome) then
        --订单收益

        for i, v in pairs(houseFieldData) do
            --所有栏位 来计算订单收益

            for k, id in pairs(v.UnlockFurnitureIds) do
                local fData = Config.furniture_level[id]
                local fTypeArray = fData.param_type

                local targetIndex = 0
                for index, num in pairs(fTypeArray) do
                    if (num == DeviceData.eDevicePropType.RoomIncome) then
                        targetIndex = index
                        break
                    end
                end

                if (targetIndex > 0) then
                    --订单收益家具
                    local fTypeNumArray = fData.param_num
                    local inNum = fTypeNumArray[targetIndex]
                    inCom = inCom + inNum
                end
            end

        end

    elseif incomType == DeviceData.eDevicePropType.UseIncome then
        --使用收益
        local fieldData = self.HouseData[roomId][fieldId]--当前栏位数据
        if (fieldData == nil) then
            return 0
        end

        for i, v in pairs(fieldData.UnlockFurnitureIds) do
            --当前栏位所有解锁家具的数据
            local fData = Config.furniture_level[v]
            local fTypeArray = fData.param_type

            local index = 0
            for j, num in pairs(fTypeArray) do
                if (num == DeviceData.eDevicePropType.UseIncome) then
                    index = j
                    break
                end
            end
            if (index > 0) then
                local fTypeNumArray = fData.param_num
                local inNum = fTypeNumArray[index]
                inCom = inCom + inNum
            end

        end
    else
        --30额外收益
        local fieldData = self.HouseData[roomId][fieldId]--当前栏位数据
        if (fieldData == nil) then
            return 0
        end
        local furnitureId = fieldData.CurUseFurnitureId --正在使用的家具Id
        local fData = Config.furniture_level[furnitureId]
        local fTypeNumArray = fData.param_type
        local fTypeArray = fData.param_num
        for i, v in pairs(fTypeNumArray) do
            if (v == DeviceData.eDevicePropType.ExtIncome) then
                local exIncom = fTypeArray[i]
                inCom = inCom + exIncom
                break
            end
        end

    end
    return inCom
end

--region ===================家具计时=============================

function DeviceData:StartTimer()
    if (self.Timer == nil) then
        self.timeTab = {}
        local nowTime = TimeUtil.GetSecTime()
        for i, fieldData in pairs(self.HouseData) do
            ---@type DeviceFieldData[]
            local fData = fieldData

            for i, fd in pairs(fData) do
                if (table.count(fd.OnThewayFiledIds) > 0) then
                    for i, v in pairs(fd.OnThewayFiledIds) do
                        local id = i
                        local endTime = v
                        --这里存的结束时间

                        if (endTime - nowTime <= 0) then
                            self.timeTab[id] = nil
                        else
                            self.timeTab[id] = endTime
                        end

                    end
                end

            end

        end
        self.Timer = TimerInst:GetTimerStart(1, self.OnTimer, self)
    end

end
function DeviceData:OnTimer()

    for i, v in pairs(self.timeTab) do
        local id = i
        local endTime = v

        --获取时间戳 单位s
        local nowTime = TimeUtil.GetSecTime()

        local time = endTime - nowTime
        if (time <= 0) then
            if (self.timeTab[id] == nil) then
                break
            end
            --向解锁的家具id数组新增id
            local houseId = self:GetHouseId(id)
            local field = self:GetFieldId(id)
            --移除运输中数据
            self.timeTab[id] = nil
            table.removebyvalue(self.timeTab, v)
            --print(self.timeTab[id])
            self:AddFurnitureFinsh(houseId, field, id)

            --通知已完成运输
            --self:FurnitureIsOnTheWayOver(id)

            --移除运势完成的家具 已完成运输等待点击
            --self:DeleteFurnitureOnTheWay(houseId, field, id)


        end
    end
end

--获得家具解锁倒计时
function DeviceData:GetFurnitureTiming(furnitureId)

    local time = self.timeTab[furnitureId]

    if (time == nil) then
        time = 0
    end
    return time - TimeUtil.GetSecTime()
end

--endregion


--region ==================数据结构定义=====================

---@class DeviceData.furnitureStatus 家具状态
DeviceData.furnitureStatus = {
    Lock = 0,
    Notbought = 1,
    OnTheway = 2,
    UnlockFinsh = 3,
    NotUseing = 4,
    isUseing = 5,
}
---@class DeviceData.fieldStatus 栏位状态
DeviceData.fieldStats = {
    lock = 0,
    NotBought = 1,
    unlock = 2,
}

---@class DeviceData.UnlockType 解锁方式
DeviceData.UnlockType = {
    Normal = 1,
    Quick = 2,
}
---@class DeviceData.ConditionData 词条数据结构
DeviceData.ConditionData = {
    ImageName = "",
    ConditionStr = "",
    FurnitureId = 0,
    IsTime = false,
}

---@class DeviceFieldData 栏位设备数据
---@field UnlockFurnitureIds table<number,number> 已解锁的家具数组
---@field CurUseFurnitureId number 正在使用的家具Id
---@field IsUnLockField boolean 当前栏位是否解锁
---@field OnThewayFiledIds table<number,number> 正在运输的家具id
---@field NewUnlockFurnitureIds table<number,number> 新增的家具

--endregion


--region ====================其他杂项=========================

--使用家具
---@param furnitureId number 更换的家具id
function DeviceData:ChangeUseFurniture(furnitureId)
    local houseId = self:GetHouseId(furnitureId)
    local field = self:GetFieldId(furnitureId)
    self:SaveUsingFurniture(houseId, field, furnitureId)
    GameDataInst:RefreshPopular()
    EventInst:Broadcast(GameEvent.ChangeFurnitureEvent, furnitureId)
    local field = DeviceData:GetInstance():GetFieldId(furnitureId)
    --记录定位某个栏位
    DeviceData:GetInstance().JumpFieldId = field
end

--运输完成
function DeviceData:FurnitureIsOnTheWayOver(furnitureId)
    EventInst:Broadcast(GameEvent.FurnitureOnTheWayOverEvent, furnitureId)
end

--房间栏位解锁
function DeviceData:FieldUnlock(field)
    EventInst:Broadcast(GameEvent.UpGradeFieldEvent, field)
end
--家具数据有变动
function DeviceData:FurnitureTrigger()
    EventInst:Broadcast(GameEvent.FurnitureTriggerEvent)
end


--打开货币缺少通用二级框
function DeviceData:OpenCommonLackView(lackId, num)
    UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, num)
end
--endregion

--region===============房间相关==============================

--获取当前房间已解锁栏位数据
---@param houseId number 当前房间id
function DeviceData:GetunLockFurniture_fieldData(houseId)
    local houseFieldData = self:GetHouseData(houseId)
    return houseFieldData
end

--获取当前房间已解锁家具数据
---@param houseId number 当前房间id
function DeviceData:GetunLockFurnitureData(houseId)
    local furniture_fieldData = self:GetunLockFurniture_fieldData(houseId)
    if (furniture_fieldData.UnlockFurnitureIds == nil) then
        furniture_fieldData.UnlockFurnitureIds = {}
    end
    return furniture_fieldData.UnlockFurnitureIds
end

--房间是否解锁
---@param houseId number 当前房间id
function DeviceData:IsHouseUnLock(houseId)
    local isUnlock = HotelData:GetInstance():HouseIsUnlock(houseId)
    return isUnlock
end

--获取当前房间数据
---@param houseId number 当前房间id
function DeviceData:GetHouseData(houseId)

    if (self.HouseData == nil) then
        self.HouseData = {}
    end
    if (self.HouseData[houseId] == nil) then
        self.HouseData[houseId] = {}
    end

    if (table.count(self.HouseData[houseId]) <= 0) then

        ---@type room_config_Item
        local houseData = ConfigManager.room_config[houseId]--当前家具数据
        if (table.count(houseData.initial_furniture) > 0) then
            --默认解锁得家具
            self:LoadInitFurnitureIds(houseId)
        end

        local houseFieldData = self.HouseData[houseId]
        return houseFieldData
    end
    return self.HouseData[houseId]
end
--endregion

--region===============栏位相关==============================

--获取当前栏位正在使用的家具
---@param houseId number 当前房间id
---@param houseId number 当前栏位id
function DeviceData:GetCurUseFurnitureData(houseId, fieldId)

    local houseFieldData = self:GetunLockFurniture_fieldData(houseId)
    if (houseFieldData == nil) then
        return nil
    end
    if (houseFieldData[fieldId] == nil) then
        return nil
    end
    local usingId = houseFieldData[fieldId].CurUseFurnitureId
    return usingId
end

--栏位是否解锁
---@param houseId number 房间id
---@param fieldId number 栏位id
function DeviceData:FieldIsUnlock(houseId, fieldId)
    local fieldData = ConfigManager.furniture[fieldId]

    if (fieldData.unlock == 1) then
        --默认解锁
        return true
    end

    ---@type DeviceFieldData[]
    local houseFieldData = self:GetunLockFurniture_fieldData(houseId)
    if (houseFieldData == nil) then

        return false
    end

    if (houseFieldData[fieldId] == nil) then
        return false
    end

    return houseFieldData[fieldId].IsUnLockField
end

--解锁栏位
---@param field number 栏位id
function DeviceData:UpGradeField(field)
    ---@type furniture_Item
    local fieldData = ConfigManager.furniture[field]

    local targetCondition = fieldData.cost
    local type = targetCondition[1]
    local costNum = targetCondition[2]

    local myNum = 0
    if (type == GameDefine.ePlayerProp.Gold) then
        myNum = GameDataInst.Gold
        -- elseif type == GameDefine.ePlayerProp.Diamond then
        -- myNum = GameDataInst.Diamond
    end

    if (myNum >= costNum) then
        --可解锁
        self:FieldUnlock(field)
    end

    local houseId = Config.furniture[field].house_type

    local dataArray = ConfigManager.room_config[houseId].initial_furniture --初始解锁数据
    for i, v in pairs(dataArray) do
        local fId = self:GetFieldId(v)

        if (fId == field) then
            self:ChangeUseFurniture(v)
            return
        end
    end
    --记录定位某个栏位
    DeviceData:GetInstance().JumpFieldId = field
end
--endregion

--region =======================家具相关================================

--获取家具词条和图片
function DeviceData:FurnitureCondition(furnitureId)
    ---@type furniture_level_Item
    local fData = ConfigManager.furniture_level[furnitureId]

    local tabStr = {}
    local tabImg = {}
    --房间属性 词条
    local paramsData = fData.param_num
    for i, v in pairs(fData.param_type) do

        ---@type furniture_properties_Item
        local itemData = ConfigManager.furniture_properties[v]

        local icon = itemData.icon
        if (itemData.text ~= nil and icon ~= nil) then
            local str
            if (v == 7) then
                str = LanguageUtil:GetValue(itemData.text, self:StringUtillPercentage(paramsData[i]))
            else
                str = LanguageUtil:GetValue(itemData.text, paramsData[i])
            end
            table.insert(tabStr, str)
            table.insert(tabImg, icon)
        end
    end
    return { tabStr, tabImg }

end

--处理数据百分比
function DeviceData:StringUtillPercentage(number)
    --TODO
    local str = number .. "%"
    return str

end

--升级家具
---@param type number 0普通升级 1快速升级
---@param id number 家具id
function DeviceData:UpGradeFurniture(type, id)
    local houseId = self:GetHouseId(id)
    local field = self:GetFieldId(id)

    --print("升级")
    --处理当前栏位 已完成运输 但未解锁的家具 处理成 已解锁状态

    ---@type DeviceFieldData
    local fieldData = self.HouseData[houseId][field]
    local nowTime = TimeUtil.GetSecTime()

    for oId, endTime in pairs(fieldData.OnThewayFiledIds) do
        if (id ~= oId) then
            if (endTime > nowTime) then
                --有人正在运输 不让他继续升级 弹出提示 TODO
                -- print("有东西正在运输")
                ---@type UISetting
                local setting = {
                    Layer = UILayerEnum.InfoLayer,
                    ShowBgMask = false

                }
                UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceCommonTip, setting, LanguageUtil:GetValue("Common_Title_transport"))
                return
            else
                --print("没东西，直接加入")
                --存在已运输完成的家具  加入到解锁列表中
                self:DeleteFurnitureOnTheWay(houseId, field, oId)
            end
        end

    end

    if (type == DeviceData.UnlockType.Normal) then
        --把家具加入到运输中
        self:AddFurnitureOnTheWay(houseId, field, id)
    else

        --钻石解锁直接完成
        --移除运输完成的家具 已完成运输等待点击

        -- self:AddFurnitureFinsh(houseId, field, id)


        local fieldData = self.HouseData[houseId][field]
        local furnitureId = id
        ---@type DeviceFieldData[]
        local data = self.HouseData[houseId]
        data[field].UnlockFurnitureIds[furnitureId] = furnitureId
        data[field].CurUseFurnitureId = furnitureId
        self.HouseData[houseId] = data
        self.HouseData[houseId][field].OnThewayFiledIds[furnitureId] = nil



        --删除在运输的家具 加入到已解锁列表中
        --self:DeleteFurnitureOnTheWay(houseId, field, id)
        --EventInst:Broadcast(GameEvent.UpGradeFurnitureEvent, id, type)
    end

    --关闭某个指定二级框
    EventInst:Broadcast(GameEvent.ClosePopEvent, "DeviceView")

    --计算扣除货币
    self:FurnitureCalculation(type, id)

    --直接朝向场景中家具位置
    EventInst:Broadcast(GameEvent.RoomFurnitureLookAt, id, function()
        if (type ~= DeviceData.UnlockType.Normal) then
            --立即完成事件
            EventInst:Broadcast(GameEvent.UnlockFurnitureImmediately, id)
        end
    end)

    --记录定位某个栏位
    DeviceData:GetInstance().JumpFieldId = field
end

--计算扣除解锁家具货币
function DeviceData:FurnitureCalculation(type, id)
    local costData = Config.furniture_level[id] .cost_normal
    local cost = 0
    if (type == DeviceData.UnlockType.Normal) then
        --普通解锁
        cost = costData[2]
    else
        --快速解锁
        cost = self:CalculationDiamondsByGold(costData[2])
    end
    GameDataInst:ChangePlayerProp(type, -cost)
end

--金币换算钻石
function DeviceData:CalculationDiamondsByGold(goldNum)
    return Mathf.Ceil(goldNum / Config.game_config.Diamond_Upgreade_Gold.paramNum)--钻石重新用金币计算向上取整
end

--时间换算钻石
function DeviceData:CalculationDiamondsByTime(timeNum)
    return Mathf.Ceil(timeNum / Config.game_config.Diamond_Upgreade_Time.paramNum)--钻石重新用时间计算向上取整
end

--家具是否满足解锁条件
function DeviceData:FurnitureCouldUnlock(furnitureId)
    local could = false

    ---@type furniture_level_Item
    local fData = Config.furniture_level[furnitureId]
    if (fData.unlock == 1) then
        return true
    end

    local paramFData = fData.term_num
    for i, v in pairs(fData.term_type) do

        if (v == 8) then
            --人气条件
            if (paramFData[i] <= GameDataInst.Popular) then
                could = true
            end

            --TODO 其他条件

        end
    end

    return could

end

--家具是否解锁
---@param houseId number 房间id
---@param fieldId number 栏位id
---@param furnitureId number 家具id
function DeviceData:FurnitureIsUnlock(houseId, fieldId, furnitureId)

    local houseFieldData = self:GetunLockFurniture_fieldData(houseId)
    if (houseFieldData == nil) then
        return false
    end
    if (houseFieldData[fieldId] == nil) then
        return false
    end

    --当前已解锁的家具id数组
    ---@type furniture_level_Item[]
    local furTab = houseFieldData[fieldId].UnlockFurnitureIds
    for i, v in pairs(furTab) do
        if (v == furnitureId) then
            return true
        end
    end
    return false
end

--是否是当前栏位正在使用的家具
---@param houseId number 房间id
---@param fieldId number 栏位id
function DeviceData:IsUsingFurniture(houseId, fieldId, furnitureId)
    local houseFieldData = self:GetunLockFurniture_fieldData(houseId)
    if (houseFieldData == nil) then
        return false
    end
    if (houseFieldData[fieldId] == nil) then
        return false
    end
    local usingId = houseFieldData[fieldId].CurUseFurnitureId
    return usingId == furnitureId
end


--通过家具获取当前房间id
---@param furnitureId number
function DeviceData:GetHouseId(furnitureId)

    ---@type furniture_level_Item
    local furnituerData = ConfigManager.furniture_level[furnitureId] --当前家具数据

    local fieldId = furnituerData.furniture_id

    ---@type furniture_Item
    local fieldData = ConfigManager.furniture[fieldId] --当前栏位数据

    local houseId = fieldData.house_type

    return houseId

end


--通过家具获取当前栏位id
---@param furnitureId number
function DeviceData:GetFieldId(furnitureId)

    ---@type furniture_level_Item
    local furnituerData = ConfigManager.furniture_level[furnitureId] --当前家具数据

    local fieldId = furnituerData.furniture_id

    return fieldId

end

--家具是否已购买 但是未解锁
function DeviceData:FurnitureUnlockButWait(furnitureId)
    for i, fieldData in pairs(self.HouseData) do
        ---@type DeviceFieldData[]
        local fData = fieldData

        for i, fd in pairs(fData) do
            if (table.count(fd.OnThewayFiledIds) > 0) then
                local endTime = fd.OnThewayFiledIds[furnitureId]
                if (endTime ~= nil) then
                    return true
                end
            end

        end

    end

    return false
end

--家具是否在运输中
function DeviceData:FurnitureIsOnTheWay(furnitureId)

    local nowTime = TimeUtil.GetSecTime()

    for i, fieldData in pairs(self.HouseData) do
        ---@type DeviceFieldData[]
        local fData = fieldData

        for i, fd in pairs(fData) do
            if (table.count(fd.OnThewayFiledIds) > 0) then
                for i, v in pairs(fd.OnThewayFiledIds) do
                    if (i == furnitureId and v ~= nil and v > 0) then

                        --nowTime小于 结束时间 表明正在路上  大于结束时间表明 已完成运输 等待点击解锁
                        return nowTime < v
                    end
                end
            end

        end

    end

    return false
end
--endregion

--region=====================本地存储=============================

--使用钻石加速
function DeviceData:UpGradeFurnitureByAddDiamond(id, costNum)
    local houseId = self:GetHouseId(id)
    local field = self:GetFieldId(id)

    --删除在运输的家具 加入到已解锁列表中
    --self:DeleteFurnitureOnTheWay(houseId, field, id)
    --EventInst:Broadcast(GameEvent.UpGradeFurnitureEvent, id, type)

    --关闭某个指定二级框
    EventInst:Broadcast(GameEvent.ClosePopEvent, "DeviceView")

    --计算扣除货币
    GameDataInst:ChangePlayerProp(self.UnlockType.Quick, -costNum)


    --[[    --直接朝向场景中家具位置
        EventInst:Broadcast(GameEvent.RoomFurnitureLookAt, id)
        --立即完成事件
        EventInst:Broadcast(GameEvent.UnlockFurnitureImmediately, id)]]

    EventInst:Broadcast(GameEvent.RoomFurnitureLookAt, id, function()
        --if (type ~= DeviceData.UnlockType.Normal) then
        --立即完成事件
        self:AddFurnitureFinsh(houseId, field, id)
        EventInst:Broadcast(GameEvent.UnlockFurnitureImmediately, id)
        --end
    end)

    --记录定位某个栏位
    DeviceData:GetInstance().JumpFieldId = field
end

--花费时间直接解锁
function DeviceData:UpGradeFurnitureByTime(furnitureId, leastTime)
    local houseId = self:GetHouseId(furnitureId)
    local field = self:GetFieldId(furnitureId)
    local isOver = GameDataInst.SpeedTime - leastTime >= 0
    if (isOver) then
        self:AddFurnitureFinsh(houseId, field, furnitureId)
        --关闭某个指定二级框
        EventInst:Broadcast(GameEvent.ClosePopEvent, "DeviceView")
        --直接朝向场景中家具位置
        EventInst:Broadcast(GameEvent.RoomFurnitureLookAt, furnitureId)
        --self:DeleteFurnitureOnTheWay(houseId, field, furnitureId)
    else
        local costTime = GameDataInst.SpeedTime
        ---@type DeviceFieldData
        local fieldData = self.HouseData[houseId][field]

        if (self:FurnitureIsOnTheWay(furnitureId)) then

            ---@type furniture_level_Item
            local furnituerData = ConfigManager.furniture_level[furnitureId] --当前家具数据

            local endTime = fieldData.OnThewayFiledIds[furnitureId] - costTime
            --local endTime = TimeUtil.GetSecTime()
            fieldData.OnThewayFiledIds[furnitureId] = endTime
            self.HouseData[houseId][field] = fieldData

            self.timeTab[furnitureId] = endTime
            --保存本地
            self:SaveLocalData()

        end
    end

    GameDataInst:ChangePlayerProp(GameDefine.ePlayerProp.SpeedTime, -leastTime)
    EventInst:Broadcast(GameEvent.FurnitureUnlockByTimeEvent, furnitureId, isOver)


    --记录定位某个栏位
    DeviceData:GetInstance().JumpFieldId = field
end

--移除正在运输的家具
function DeviceData:DeleteFurnitureOnTheWay(houseId, field, furnitureId)
    ---@type DeviceFieldData
    local fieldData = self.HouseData[houseId][field]

    -- if (self:FurnitureIsOnTheWay(furnitureId)) then


    ---@type DeviceFieldData[]
    local data = self.HouseData[houseId]
    data[field].UnlockFurnitureIds[furnitureId] = furnitureId
    self.HouseData[houseId] = data

    self.HouseData[houseId][field].OnThewayFiledIds[furnitureId] = nil

    --end
    --保存本地
    self:SaveLocalData()
    --家具解锁事件已完成
    self:FurnitureIsOnTheWayOver(furnitureId)
end

--把家具加入到运输数据中
function DeviceData:AddFurnitureOnTheWay(houseId, field, furnitureId)

    ---@type DeviceFieldData
    local fieldData = self.HouseData[houseId][field]

    if (not self:FurnitureIsOnTheWay(furnitureId)) then

        ---@type furniture_level_Item
        local furnituerData = ConfigManager.furniture_level[furnitureId] --当前家具数据

        local endTime = TimeUtil.GetSecTime() + furnituerData.waitstime
        fieldData.OnThewayFiledIds[furnitureId] = endTime
        self.HouseData[houseId][field] = fieldData

        self.timeTab[furnitureId] = endTime
        --保存本地
        self:SaveLocalData()

        --发送普通 家具升级
        EventInst:Broadcast(GameEvent.UpGradeFurnitureEvent, furnitureId, DeviceData.UnlockType.Normal)
    end

end


--添加新家具到已完成的数据中
function DeviceData:AddFurnitureFinsh(houseId, field, furnitureId)
    ---@type DeviceFieldData
    local fieldData = self.HouseData[houseId][field]

    -- ---@type furniture_Item
    -- local furnituerData = ConfigManager.furniture_level[furnitureId] --当前家具数据

    local endTime = TimeUtil.GetSecTime() - 1
    fieldData.OnThewayFiledIds[furnitureId] = endTime
    self.HouseData[houseId][field] = fieldData

    self.timeTab[furnitureId] = nil
    --保存本地
    self:SaveLocalData()

    self:FurnitureIsOnTheWayOver(furnitureId)
end



--存储解锁栏位数据
---@param houseId number 房间id
---@param field number 栏位id
function DeviceData:SaveFieldData(houseId, field)
    if (self.HouseData[houseId] == nil) then
        self.HouseData[houseId] = {}
    end
    ---@type DeviceFieldData
    local data
    --是否已有初始数据

    if (self.HouseData[houseId][field] ~= nil and self.HouseData[houseId][field].IsUnLockField == false) then
        data = self.HouseData[houseId][field]
        data.IsUnLockField = true
    else
        ---@type DeviceFieldData
        data = {
            UnlockFurnitureIds = {},
            CurUseFurnitureId = nil,
            IsUnLockField = true,
            OnThewayFiledIds = {}
        }
    end
    self.HouseData[houseId][field] = data

    --保存本地
    self:SaveLocalData()

end

--存储解锁家具数据
---@param houseId number 房间id
---@param field number 栏位id
------@param furnitureId number 家具id
function DeviceData:SaveFurniture(houseId, field, furnitureId)
    if (self.HouseData[houseId] == nil) then
        self.HouseData[houseId] = {}
    end
    ---@type DeviceFieldData
    local data = self.HouseData[houseId][field]
    data.UnlockFurnitureIds[furnitureId] = furnitureId
    self.HouseData[houseId][field] = data
    --保存本地
    self:SaveLocalData()
end

--存储使用中的家具
function DeviceData:SaveUsingFurniture(houseId, field, furnitureId)
    if (self.HouseData[houseId] == nil) then
        self.HouseData[houseId] = {}
    end
    ---@type DeviceFieldData
    local data = self.HouseData[houseId][field]
    data.CurUseFurnitureId = furnitureId
    self.HouseData[houseId][field] = data
    --保存本地
    self:SaveLocalData()
    self:FurnitureTrigger()


end
---@class GameDataDevice 本地房间数据
---@field HouseData table<number,table<number,DeviceFieldData>> 房间本地数据


function DeviceData:SaveLocalData()
    ---@type GameDataDevice
    local currency = {
        HouseData = self.HouseData
    }
    PlayerPrefsUtil.SetTable("DeviceData_key", currency)
end
function DeviceData:InitLocalData()
    ---货币
    ---@type GameDataDevice
    local currency = PlayerPrefsUtil.GetTable("DeviceData_key")
    if not IsNil(currency) then
        self.HouseData = currency.HouseData
    else
        self.HouseData = {}

    end

end

--region ################# 获取设备上的属性 ##################

---获取设备上的属性
---@param furnitureId number 设备id
---@param propType DeviceData.eDevicePropType 设备属性key
---@return number 属性值
function DeviceData:GetFurniturePropValue(furnitureId, propType)

    if not IsNil(Config.furniture_level[furnitureId]) then
        local params = Config.furniture_level[furnitureId].param_type
        local paramMap = {}
        for i = 1, #params do
            local values = Config.furniture_level[furnitureId].param_num
            if i <= #values then
                paramMap[params[i]] = values[i]
            end
        end
        if (paramMap[propType] == nil) then
            return 0
        end
        return paramMap[propType]
    end
    return 0
end

--endregion ################# 获取设备上的属性 ##################


--加载房间初始化数据
function DeviceData:LoadInitFurnitureIds(houseId)

    --加载房间默认家具
    local dataArray = ConfigManager.room_config[houseId].initial_furniture

    local furnitureIds = {}
    for i, v in pairs(dataArray) do
        table.insert(furnitureIds, v)
    end

    ---@type furniture_Item[]
    local fieldData = ConfigManager.GetConfigByField(ConfigManager.furniture, "house_type", { houseId })


    --默认解锁的栏位
    ---@type furniture_Item[]
    local fieldArray = {}
    for i, v in pairs(fieldData) do
        if (v.unlock == 1) then
            table.insert(fieldArray, v.id)
        end
    end

    --栏位对应解锁的家具
    local unlockIds = {}
    for i, v in pairs(fieldArray) do
        ---@type furniture_level_Item[]
        local fData = ConfigManager.GetConfigByField(ConfigManager.furniture_level, "furniture_id", { v })

        for i, fv in pairs(fData) do

            if (fv.unlock == 1) then
                -- print(fv.id)
                table.insert(furnitureIds, fv.id)
                table.insert(unlockIds, fv.id)
            end
        end
    end

    for i, v in pairs(furnitureIds) do
        -- print(v)
        --栏位id
        --初始数据处理
        local initField = self:GetFieldId(v)
        -- print(initField)
        ---@type DeviceFieldData
        local test = {
            UnlockFurnitureIds = { v },
            CurUseFurnitureId = v,
            --此处设定栏位是否解锁
            IsUnLockField = table.ContainsValue(unlockIds, v),
            OnThewayFiledIds = {}
        }

        self.HouseData[houseId][initField] = test
    end
    self:SaveLocalData()
    self:FurnitureTrigger()
end
--endregion
return DeviceData