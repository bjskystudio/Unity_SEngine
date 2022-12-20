---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Simon.L.
--- DateTime: 2022/11/3 18:05
---
local EventID = require("EventID")
local Log = require("Log")
local EventInst = require("EventManager"):GetInstance()
local GameDataInst = require("GameData"):GetInstance()
local UIManager = require("UIManager")
local UIDefine = require("UIDefine")

---@class EggMachine.EggInfo
---@field type EggMachine.eEggType


---@class EggMachine : Singleton @扭蛋机
---@field PlayState EggMachine.ePlayState 当前轮次玩法
---@field RoundEggs table<number,EggMachine.eEggType> 当前轮次产生的扭蛋
local EggMachine = Class("EggMachine", Singleton)
--region  ---------------------- 扭蛋事件 ----------------------
---扭蛋机点击一次
EggMachine.ClickOnce = EventID:CreateEventID()
---扭蛋机点击一轮
EggMachine.RoundComplete = EventID:CreateEventID()
--endregion  ---------------------- 扭蛋事件 ----------------------

function EggMachine:__init()
    self.ClickNum = 0
    self.PlayState = EggMachine.ePlayState.Normal
end


---扭蛋机玩法状态
---@class EggMachine.ePlayState
EggMachine.ePlayState = {
    ---普通，点三次出一个
    Normal = 1,
    ---电力充足，点1次出一个
    Enough = 2,
    ---双子，点6次，出2个
    Double = 3,
    ---捣蛋小精灵，点6次，出4个
    Spirit = 4,
    --- 骇客入侵,点6次，出8个扭蛋
    Intrusion = 5
}


---@class EggMachine.eEggType
EggMachine.eEggType = {
    --- 普通，一个客人
    Normal = 1,
    ---体力蛋，体力+3
    Power = 2,
    ---金蛋，jia金币
    GoldEgg = 3,
    --- 赫尔墨斯，2倍小费
    Monster = 4,
    --- 矮人，持续时间内，点击1次获得扭蛋
    Dwarves = 5
}

function EggMachine:ClickMachine()
    if GameDataInst.Energy == 0 then
        Log.Debug("没有体力")
        UIManager:GetInstance():OpenUIDefine(UIDefine.EnergyView)
        return
    end
    --Log.Debug("点击扭蛋机")
    if self.ClickNum == 0  then
        self.PlayState = self:GetNextPlayState(self.PlayState)
    end

    if self.PlayState == EggMachine.ePlayState.Normal then
        ---普通
        self.RoundClickNum = 3
    end
    self.ClickNum = self.ClickNum + 1
    EventInst:Broadcast(EggMachine.ClickOnce,self.ClickNum)

    if self.ClickNum >= self.RoundClickNum then
        self:CompleteRound(self.PlayState)
        EventInst:Broadcast(EggMachine.RoundComplete,self.PlayState,self.RoundEggs)
        self.ClickNum = 0
    end
end

---获取当前进度
function EggMachine:GetJDValue()
    if self.RoundClickNum ~= nil then
        return self.ClickNum / self.RoundClickNum
    else
        return 0
    end
end
function EggMachine:ResetClick()
    self.ClickNum = 0
end

---@param prePlayState EggMachine.ePlayState 上一个状态
function EggMachine:GetNextPlayState(prePlayState)
    return EggMachine.ePlayState.Normal
end

---完成轮次产生扭蛋
function EggMachine:CompleteRound(playState)
    local eggCount = 0
    local eggs = {}
    if playState == EggMachine.ePlayState.Normal then
        eggCount = 1
        for i = 1, eggCount do
            ---@type EggMachine.EggInfo
            local eggInfo = {
                type = EggMachine.eEggType.Normal
            }
            eggs[i] = eggInfo
        end
    end
    self.RoundEggs = eggs
end

return EggMachine