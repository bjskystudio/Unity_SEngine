---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/10/9 19:02
---

local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local UIManager = require("UIManager")
local UIDefine = require("UIDefine")
local GameDefine = GameDefine
local SceneManager = require("SceneManager")
local EventManager = require("EventManager")
local GameData = require("GameData")
local TimeUtil = require("TimeUtil")
local GameDataInst = require("GameData"):GetInstance()
local TimerInst = require("TimerManager"):GetInstance()
local EventInst = require("EventManager"):GetInstance()
local UILayerEnum = require("UILayerEnum")
---@class MainView : UIBase 窗口
---@field private go_table MainView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local MainView = Class("MainView", UIBase)

---添加Events监听事件
function MainView:Awake()
    self:AddEvent(GameEvent.SceneChangeEvent)
    self:AddEvent(GameEvent.GoinMoveEvent)
    self:AddEvent(GameEvent.EnergyMoveEvent)
    self:AddEvent(GameEvent.CoinFlyEvent)
    self:AddEvent(GameEvent.EggMachineProgress)
end
--- 窗口显示[protected]
---@param ... any @窗口传参
function MainView:OnCreate()

    EventManager:GetInstance():AddListener(GameEvent.GoldChange, self.ChangeGold, self)
    EventManager:GetInstance():AddListener(GameEvent.DiamondChange, self.ChangeDiamond, self)
    EventManager:GetInstance():AddListener(GameEvent.PopularChange, self.ChangePopular, self)
    EventManager:GetInstance():AddListener(GameEvent.GhostChange, self.ChangeGhost, self)
    EventManager:GetInstance():AddListener(GameEvent.EnergyChange, self.ChangeEnergy, self)
    self.go_table.obj_timemessage:SetActive(false)
    self.go_table.sbtn_anyClick.gameObject:SetActive(false)
    self.m_currSelTab = 0
    self.go_table.simg_task:LoadSprite(GameDefine.eResPath.AtlasMain .. "icon_task", false, 1, function(sprite)
        --Log.Debug("动态加载图片完成:icon_task")
    end)
    self:SetBottom(SceneManager.eSceneType.Hotel)
    self:InitHub()
    self.AddMaxTime = 1
    self.CountTime = 0
    self.RoundClickNum = nil
    self.go_table.sbtn_machineknob.NaClickIntervalmes = 0.5
end

---初始化头部数据
function MainView:InitHub()
    self.go_table.stmp_gold.text = GameData:GetInstance().Gold
    self.go_table.stmp_popular.text = GameData:GetInstance().Popular
    self.go_table.stmp_diamond.text = GameData:GetInstance().Diamond
    self.go_table.stmp_ghost.text = GameData:GetInstance().Ghost
    self.go_table.stmp_energy.text = GameDataInst.Energy .. "/" .. Config.game_config.Max_energy.paramNum

    self.FlyTime = TimerInst:GetTimerStartImme(0.1, self.FlyGold, self)

    self.TargetGoldNum = GameDataInst.Gold
    self.go_table.stmp_hotel.text = LanguageUtil:GetValue("HUD_Button_Hotel")
    self.go_table.stmp_shop.text = LanguageUtil:GetValue("HUD_Button_Shop")
    self.go_table.stmp_task.text = LanguageUtil:GetValue("HUD_Button_Task")
    self.go_table.stmp_staff.text = LanguageUtil:GetValue("HUD_Button_Staff")
    self.go_table.stmp_machine.text = LanguageUtil:GetValue("HUD_Button_Machine")
end
function MainView:ChangeGold(num, value, isAdd)
    self.TargetGoldNum = num

    -- self.go_table.obj_gold.transform:DOScale(2, 2)

    --[[    self.go_table.img_goldimg.transform:DOScale(1.3, 0.5):OnComplete(function()
            self.go_table.img_goldimg.transform:DOScale(1, 0.5)
        end)]]
    -- self.go_table.stmp_gold.text = num
end
function MainView:ChangeDiamond(num, value, isAdd)
    self.go_table.stmp_diamond.text = num
    EventManager:GetInstance():Broadcast(GameEvent.CoinChangeFinsh)

end
function MainView:ChangePopular(num, value, isAdd)
    self.go_table.stmp_popular.text = num
    EventManager:GetInstance():Broadcast(GameEvent.CoinChangeFinsh)
end
function MainView:ChangeGhost(num, value, isAdd)
    self.go_table.stmp_ghost.text = num
    EventManager:GetInstance():Broadcast(GameEvent.CoinChangeFinsh)
end
function MainView:ChangeEnergy(num, value, isAdd)
    self.go_table.stmp_energy.text = num .. "/" .. Config.game_config.Max_energy.paramNum
    EventManager:GetInstance():Broadcast(GameEvent.CoinChangeFinsh)
end

function MainView:SetSelTab(tabIndex)
    self.m_currSelTab = tabIndex
end

---事件处理
---@private
---@param id EventID 事件ID
function MainView:EventHandle(id, ...)
    local args = table.pack(...)
    if id == GameEvent.SceneChangeEvent then
        local sceneType = args[1]
        local roomId = args[2]
        self:SetBottom(sceneType, roomId)

    elseif id == GameEvent.GoinMoveEvent then
        local num = args[1]

        local itemNodeClone = CS.UnityEngine.Object.Instantiate(self.go_table.obj_coinMove, self.go_table.obj_moveNode.transform)
        itemNodeClone.transform.gameObject:SetActive(true)
        local item = self:GetOrAddComponent(itemNodeClone, require("CoinMoveItem"))
        item:InitData(num, function()
            self:RemoveComponentInstance(item)
        end)
    elseif id == GameEvent.EnergyMoveEvent then
        local num = args[1]
        local itemNodeClone = CS.UnityEngine.Object.Instantiate(self.go_table.obj_coinMove, self.go_table.obj_moveEnergyNode.transform)
        itemNodeClone.transform.gameObject:SetActive(true)
        local item = self:GetOrAddComponent(itemNodeClone, require("CoinMoveItem"))
        item:InitData(num, function()
            self:RemoveComponentInstance(item)
        end)

    elseif id == GameEvent.CoinFlyEvent then

        if (table.count(self.FlyMap) <= 0) then
            self.FlyMap = {}
        end

        --金币飞行
        local trans = args[1]
        local startPos = trans:ConvertLocalPositionToParent(CSUIModel.UICamera, self.go_table.obj_gold.transform)
        local num = args[2]
        if (num > 10) then
            num = 10
        end
        local key = Random.Range(1, 100) .. "abv" .. startPos.x .. "b" .. startPos.y
        GameDataInst.StartPosArray[key] = startPos
        table.insert(self.FlyMap, { num, 0, key })

        --self:FlyGold()
    elseif id == GameEvent.EggMachineProgress then
        local bar = args[1]
        print("xxxx" .. bar)
        if bar ~= self.RoundClickNum then
            self.RoundClickNum = bar
            if bar == 0 then
                self.go_table.img_machinebar:DOFillAmount(1 , 0.2):OnComplete(function()
                    self.go_table.img_machinebar.fillAmount = 0
                end
                )
            else
                self.go_table.img_machinebar:DOFillAmount(bar , 0.2)
            end

            self.go_table.sbtn_machineknob.transform:DOLocalRotate(Vector3.New(0,180,360*bar)  , 0.5):OnComplete(function()
                
            end
            )
        end
    end
end

---物品飞行
function MainView:FlyGold()
    ---金币飞行相关
    if (table.count(self.FlyMap) > 0) then

        for j, v in pairs(self.FlyMap) do
            if (v ~= nil) then
                local startPos = GameDataInst.StartPosArray[v[3]]
                if (startPos ~= nil) then
                    local num = v[1]
                    v[2] = v[2] + 1  --计时
                    for i = 1, num do
                        if (i >= v[2]) then
                            local itemNodeClone = CS.UnityEngine.Object.Instantiate(self.go_table.obj_goldFlyitem, self.go_table.obj_fly.transform)
                            itemNodeClone.transform.gameObject:SetActive(true)
                            local item = self:GetOrAddComponent(itemNodeClone, require("GoldFly"))

                            itemNodeClone.transform.localPosition = startPos

                            local iconName = "Icon_coin"
                            item:InitData(iconName, function()
                            end)
                            --self.transform.localScale = 0.3
                            --itemNodeClone.transform.localScale = 0.3
                            itemNodeClone.transform:DOScale(2, 0.4)
                            local endPos = self.go_table.img_goldimg.transform.localPosition
                            local controlPos = Vector3.New((startPos.x + endPos.x) / 2, startPos.y + 20, 0)
                            itemNodeClone.transform:DoBezier2(startPos, controlPos, endPos, 0.4):OnComplete(function()
                                --itemNodeClone:DestroyGameObj()
                                self:RemoveComponentInstance(item)
                                if (i == num) then
                                    table.removebyvalue(self.FlyMap, v)
                                    GameDataInst.StartPosArray[v[3]] = nil

                                end

                            end)         :SetEase(CS.DG.Tweening.Ease.OutSine)
                        end

                    end
                end

            end

        end

        ---金币text增长相关
        local num = tonumber(self.go_table.stmp_gold.text)
        --print(num .. "  " .. tonumber(self.go_table.stmp_gold.text))
        if (self.CountTime < self.AddMaxTime and num < self.TargetGoldNum) then

            self.CountTime = self.CountTime + 0.1
            local value = self.TargetGoldNum - num
            local speed = value / (self.AddMaxTime * 10)
            if (speed < 1) then
                speed = 1
            end
            local showNum = string.format("%.0f", num + speed)
            self.go_table.stmp_gold.text = showNum
            self.go_table.img_goldimg.transform:DOScale(1.3, 0.1):OnComplete(function()
                self.go_table.img_goldimg.transform:DOScale(1, 0.1)
            end)
        end
        if (num >= self.TargetGoldNum or self.CountTime >= self.AddMaxTime) then
            self.CountTime = 0
            EventManager:GetInstance():Broadcast(GameEvent.CoinChangeFinsh)
            num = self.TargetGoldNum
            self.go_table.stmp_gold.text = string.format("%.0f", num)
        end
    else
        local num = tonumber(self.go_table.stmp_gold.text)
        if (num ~= self.TargetGoldNum) then
            EventManager:GetInstance():Broadcast(GameEvent.CoinChangeFinsh)
            self.go_table.stmp_gold.text = string.format("%.0f", self.TargetGoldNum)
        end

    end


end

---可用
---@protected
function MainView:OnEnable()
end

---不可用
---@protected
function MainView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function MainView:OnClickBtn(btn)
    if btn == self.go_table.sbtn_back then
        SceneManager:GetInstance():ChangeToHotel()
    elseif btn == self.go_table.sbtn_device then
        UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceView, nil, SceneManager:GetInstance().RoomId)
    elseif btn == self.go_table.sbtn_hotel then

        --[[        ---@type UISetting
                local setting = {
                    Layer = UILayerEnum.InfoLayer,
                    ShowBgMask = false
                }
                UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceCommonTip, setting, LanguageUtil:GetValue("Common_Title_transport"))]]
        UIManager:GetInstance():OpenUIDefine(UIDefine.HotelView)
        self.m_currSelTab = 1
        self:RefreshTab()
    elseif btn == self.go_table.sbtn_showenergyMessage then
        --打开精力恢复倒计时UI
        self.go_table.obj_timemessage:SetActive(true)
        self.go_table.sbtn_anyClick.gameObject:SetActive(true)
        self:OpenTimer()
    elseif btn == self.go_table.sbtn_anyClick then
        --关闭精力恢复倒计时UI
        self.go_table.obj_timemessage:SetActive(false)
        self.go_table.sbtn_anyClick.gameObject:SetActive(false)
        self:StopTimer()
    elseif btn == self.go_table.sbtn_addenergy then
        UIManager:GetInstance():OpenUIDefine(UIDefine.EnergyView)
    elseif btn == self.go_table.sbtn_setting then
        --GameDataInst:ChangePlayerProp(GameDefine.ePlayerProp.Gold, 50)
    elseif btn == self.go_table.sbtn_atlas then
        --GameDataInst:ChangePlayerProp(GameDefine.ePlayerProp.Gold, 50)
    elseif btn == self.go_table.sbtn_machineknob then
        EventInst:Broadcast(GameEvent.BtnMachine)
    end
end
function MainView:OpenTimer()

    if (self.Timer == nil) then

        self.Timer = TimerInst:GetTimerStartImme(1, self.OnTimer, self)
    end

end
function MainView:OnTimer()
    local nowTime = TimeUtil.GetSecTime()
    if (self.go_table.obj_timemessage.activeSelf) then
        local time = GameDataInst.EnergyRecoveryTime_next
        if (time <= 0) then
            time = 0
        end
        self.go_table.stmp_nextenergy.text = "下次体力恢复:" .. TimeUtil.TimeToString(time, 3)

        local time2 = GameDataInst.EnoughTime
        if (time2 <= 0) then
            time2 = 0
        end
        self.go_table.stmp_allEnergy.text = "全部恢复:" .. TimeUtil.TimeToString(time2, 3)
    end
end
function MainView:StopTimer()
    TimerInst:StopAndClearTimer(self.Timer)
    self.Timer = nil
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function MainView:OnClickToggle(toggle, isOn)
end

---@param sceneType SceneManager.eSceneType
---@param roomId number
function MainView:SetBottom(sceneType, roomId)
    self.go_table.obj_bottom.gameObject:SetActive(sceneType == SceneManager.eSceneType.Hotel)
    self:RefreshTab()
    ---房间bottom
    if roomId and roomId > 0 then
        self.go_table.obj_PublicBot.gameObject:SetActive(SceneManager:GetInstance():IsPublicRoom(roomId))
    else
        self.go_table.obj_PublicBot.gameObject:SetActive(false)
    end
end

function MainView:RefreshTab()
    self.go_table.simg_tabhotel.gameObject:SetActive(false)
    self.go_table.simg_tabItask.gameObject:SetActive(false)
    self.go_table.simg_tabshop.gameObject:SetActive(false)
    self.go_table.simg_tabstaff.gameObject:SetActive(false)
    
    if self.m_currSelTab == 1 then
        self.go_table.simg_tabhotel.gameObject:SetActive(true)
    elseif self.m_currSelTab == 2 then
        self.go_table.simg_tabshop.gameObject:SetActive(true)
    elseif self.m_currSelTab == 3 then
        self.go_table.simg_tabItask.gameObject:SetActive(true)
    elseif self.m_currSelTab == 4 then
        self.go_table.simg_tabstaff.gameObject:SetActive(true)
    end
    
end

---数据清理
---@protected
function MainView:OnDestroy()
    --MainView.ParentCls.OnDestroy(self)
    self:StopTimer()
end

return MainView