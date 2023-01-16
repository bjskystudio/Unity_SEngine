---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/12/7 18:18
---
local MapManager = require("MapManager")
local AvatarState = require("AvatarState")
local AvatarManager = require("AvatarManager")
local AvatarStateMachine = require("AvatarStateMachine")
local ConfigManager = require("ConfigManager")
local SceneManager = require("SceneManager")

------@class LoungeStandState:AvatarState 休息室站立
---@field Machine AvatarStateMachine
local LoungeStandState = Class("LoungeStandState",AvatarState)

function LoungeStandState:__init()
    self.Speed = 30
end

---进入状态
---@override
function LoungeStandState:Enter()
    self.StartTime = self.Machine.Time

    self.FurnitureTypeId = self.Machine.Info.UseFurnitureId
    self.FurnitureConfig = ConfigManager.furniture[self.FurnitureTypeId]

    self.Machine:SetInfoAnim("idle",true)

end
---状态机更新
---@override
function LoungeStandState:Update()
    local time = self.Machine.Time
    ---1秒后move
    if time -self.StartTime > 0.2 then
        --验证目的地
        local gotoPos ,nextUseId = self:CheckNextFurniture()
        if gotoPos == MapManager.ePosition.None then
            ---更新startTime
            self.StartTime = self.Machine.Time
        else
            if gotoPos == MapManager.ePosition.LoungeDoor then
                --出去了，显示表情
                if self.Machine.Info.IsAngry then
                    ---@type expression_bubble_Item
                    self.BubbleConfig = Config.GetConfigByField(ConfigManager.expression_bubble,"bubble_type",{AvatarStateMachine.eHudType.Face})[1]
                    local angryBubbles = Config.GetConfigByField(ConfigManager.expression_bubble,"text_type",{1})
                    local angryBubbleId = angryBubbles[Mathf.Random(1,#angryBubbles)].id
                    self.Machine:SetInfoBubble(self.BubbleConfig.id,self.BubbleConfig.time,{angryBubbleId})
                end
            end
            self.Machine.Info.UseFurnitureId = nextUseId
            self.Machine.Info.GotoPos = gotoPos
            self.Machine:ChangeState(AvatarStateMachine.eStateName.LoungeMove)
        end
    end
end


---寻找下一个设备
---@return number,number
function LoungeStandState:CheckNextFurniture()
    ---找下一个设备去
    local canUseFurnitureIds = {}
    if self.FurnitureConfig.furniture_used == 1 then ---找必须使用的
    local needFurnitureConfigs = ConfigManager.GetConfigByField(ConfigManager.furniture,"furniture_used",{2})
        for i = 1, #needFurnitureConfigs do
            ---@type furniture_Item
            local config = needFurnitureConfigs[i]
            local canGoto = self.Machine:LoungeCanGotoFurniture(config.id)
            if canGoto then
                table.insert(canUseFurnitureIds,config.id)
            end
        end
        --随机一个
        if #canUseFurnitureIds >0 then
            return MapManager.ePosition.Furniture, canUseFurnitureIds[Mathf.Random(1,#canUseFurnitureIds)]
        else
            ---等待
            return MapManager.ePosition.None,0
        end
    elseif self.FurnitureConfig.furniture_used == 2 or self.FurnitureConfig.furniture_used == 3 then ---找可能使用的
        --不包括自己
        for i = 1, #self.Machine.Info.CanUseFurnitureIds do
            if self.Machine.Info.CanUseFurnitureIds[i] ~= self.FurnitureTypeId then
                local hasGoto = #AvatarManager:GetInstance():GetFurnitureStateMachines(AvatarStateMachine.eStateName.LoungeMove,self.Machine.Info.CanUseFurnitureIds[i]) > 0
                local hasNeed = #AvatarManager:GetInstance():GetFurnitureStateMachines(AvatarStateMachine.eStateName.NeedFurniture,self.Machine.Info.CanUseFurnitureIds[i]) > 0
                if not hasGoto and not hasNeed then
                    table.insert(canUseFurnitureIds,self.Machine.Info.CanUseFurnitureIds[i])
                end
            end
        end
        if #canUseFurnitureIds > 0 then
            return MapManager.ePosition.Furniture, canUseFurnitureIds[Mathf.Random(1,#canUseFurnitureIds)]
        else
            return MapManager.ePosition.LoungeDoor,0
        end
    end
end

---退出状态
---@override
function LoungeStandState:Exit()
    self.StartTime = 0
end
return LoungeStandState