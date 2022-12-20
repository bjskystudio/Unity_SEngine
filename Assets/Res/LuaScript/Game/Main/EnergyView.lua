local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local GameDataInst = require("GameData"):GetInstance()
local DeviceData = require("DeviceData")
local UIManager = require("UIManager")
local UIInst = require("UIManager"):GetInstance()
local TimeUtil = require("TimeUtil")
local TimerManager = require("TimerManager")

---@class EnergyView : PopBase 窗口
---@field private go_table EnergyView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local EnergyView = Class("EnergyView", PopBase)

---添加Events监听事件
function EnergyView:Awake()
    self.ClickSpaceClose = true
    self:AddEvent(GameEvent.EnergyChange)
end
--- 窗口显示[protected]
---@param ... any @窗口传参
function EnergyView:OnCreate()
    self:initData()
end

function EnergyView:initData()
    self.go_table.stmp_smallTitle.text = LanguageUtil:GetValue("Common_Energy_Recover_tips")
    self.go_table.stmp_message.text = LanguageUtil:GetValue("Common_Energy_Recover_01")

    self.go_table.stmp_unlockStr.text = LanguageUtil:GetValue("Common_Energy_Recover_button_02")
    self.go_table.stmp_buyStr.text = LanguageUtil:GetValue("Common_Energy_Recover_button_01")

    local recoverNum = Config.game_config.Gems_Energy_Recover.paramNum --钻石或广告提供的体力数量
    self.go_table.stmp_eneryNum.text = "x" .. recoverNum
    self.recoverNum = recoverNum
    self.go_table.stmp_message.text = LanguageUtil:GetValue("Common_Energy_Recover_01", recoverNum)

    local time = GameDataInst.EnergyRecoveryTime_next
    if (time <= 0) then
        self.startNatureTime = false
        self.go_table.stmp_energyMes.text = ""
    else
        self.startNatureTime = true
        self.Timer = TimerManager:GetInstance():GetTimerStartImme(1, self.OnTimer, self)
    end

    self:initAdText()
    self:InitDiamondButton()
end
function EnergyView:initAdText()
    local adCount = Config.game_config.AD_Count.paramNum --每日可观看广告数量
    local myAdCount = GameDataInst.EnergyAdCount --我看的广告数量
    local decAdCount = adCount - myAdCount
    if (decAdCount <= 0) then
        self.go_table.sbtn_confirm:SetGrayWithInteractable(true)
        self.startAdTime = true
        --开始倒计时
        local decTime = GameDataInst.UpdateTime - TimeUtil.GetSecTime()
        self.go_table.stmp_buyCs.text = LanguageUtil:GetValue("Common_Energy_Recover_05") .. TimeUtil.TimeToString(decTime, 3, false)

    else
        self.startAdTime = false
        self.go_table.stmp_buyCs.text = decAdCount .. "/" .. adCount
    end
    if (self.Timer == nil) then
        self.Timer = TimerManager:GetInstance():GetTimerStartImme(1, self.OnTimer, self)
    end

end
function EnergyView:OnTimer()
    if (self.startAdTime) then
        local decTime = GameDataInst.DecTime
        if (decTime <= 0) then
            self:Close()
            return
        end
        self.go_table.stmp_buyCs.text = LanguageUtil:GetValue("Common_Energy_Recover_05") .. TimeUtil.TimeToString(decTime, 3, false)
    end

    if (self.startNatureTime) then
        local time = GameDataInst.EnergyRecoveryTime_next
        if (time <= 0) then
            self.go_table.stmp_energyMes.text = ""
            self.startNatureTime = false
            TimerManager:GetInstance():StopAndClearTimer(self.Timer)
            self.Timer = nil
        else
            local str = TimeUtil.TimeToString(time, 3)
            local natureNum = Config.game_config.Energy_Recover_Amount.paramNum
            self.go_table.stmp_energyMes.text = LanguageUtil:GetValue("Common_Energy_Recover_02", str, natureNum)
        end

    end


end
--初始化钻石按钮相关
function EnergyView:InitDiamondButton()
    local adEnergyArray = Config.game_config.AD_Gems_Cost.paramNumArr --钻石购买消耗数组
    local lenAd = #adEnergyArray
    local isMax = GameDataInst.EnergyBuyCount >= lenAd
    local costNum
    if (isMax) then
        costNum = adEnergyArray[lenAd]
    else
        costNum = adEnergyArray[GameDataInst.EnergyBuyCount]
    end
    self.go_table.stmp_costDiamond.text = costNum

    self.costDiamond = costNum
end

---事件处理
---@private
---@param id EventID 事件ID
function EnergyView:EventHandle(id, ...)
    local tab = { ... }

    if (id == GameEvent.EnergyChange) then
        self:InitDiamondButton()
        self:initAdText()
    end
end

---可用
---@protected
function EnergyView:OnEnable()
end

---不可用
---@protected
function EnergyView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function EnergyView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end

    if btn == self.go_table.sbtn_buy then
        --钻石购买
        local myDiamond = GameDataInst.Diamond
        local lackNum = self.costDiamond - myDiamond
        local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Diamond)
        if (lackNum > 0) then
            --钻石不足

            DeviceData:GetInstance():OpenCommonLackView(lackId, lackNum)
            return
        end

        local maxEnergy = Config.game_config.Max_energy.paramNum --最大体力数量
        local decEnergy = maxEnergy - GameDataInst.Energy
        if (decEnergy <= 0) then
            print("体力已满")
            UIManager:GetInstance():OpenUIDefine(UIDefine.EnergyNoticeView)
            return
        end
        UIInst:OpenUIDefine(UIDefine.DeviceConfirmView, nil, function()
            GameDataInst:RecverEnergyByDiamond(self.costDiamond, self.recoverNum)
        end, lackId)


    elseif btn == self.go_table.sbtn_confirm then
        --广告购买
        local adCount = Config.game_config.AD_Count.paramNum --每日可观看广告数量
        local myAdCount = GameDataInst.EnergyAdCount --我看的广告数量
        local decAdCount = adCount - myAdCount
        if (decAdCount <= 0) then
            print("次数已用完")
            return
        end
        local maxEnergy = Config.game_config.Max_energy.paramNum --最大体力数量
        local decEnergy = maxEnergy - GameDataInst.Energy
        if (decEnergy <= 0) then
            print("体力已满")
            UIManager:GetInstance():OpenUIDefine(UIDefine.EnergyNoticeView)
            return
        end

        GameDataInst:RecverEnergyByAD(self.recoverNum)
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function EnergyView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function EnergyView:OnDestroy()
    --EnergyView.ParentCls.OnDestroy(self)
    TimerManager:GetInstance():StopAndClearTimer(self.Timer)
end

return EnergyView