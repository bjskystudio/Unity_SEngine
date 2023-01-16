---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/10/13 18:17
---

local PopBase = require("PopBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local ConfigManager = require("ConfigManager")
local TimeUtil = require("TimeUtil")
local DeviceData = require("DeviceData")
local DeviceDataInst = require("DeviceData"):GetInstance()
local GameDefine = GameDefine
local GameDataInst = require("GameData"):GetInstance()
local UIInst = require("UIManager"):GetInstance()
local LoopListViewHelper = LoopListViewHelper
local TimerInst = require("TimerManager"):GetInstance()
local Config = Config
local EventInst = require("EventManager"):GetInstance()

---@class DeviceDescView : PopBase 窗口
---@field private go_table DeviceDescView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceDescView = Class("DeviceDescView", PopBase)

---添加Events监听事件
function DeviceDescView:Awake()
    self:AddEvent(GameEvent.FurnitureOnTheWayOverEvent)
    self:AddEvent(GameEvent.UpGradeFurnitureEvent)
    self:AddEvent(GameEvent.FurnitureUnlockByTimeEvent)
    self:AddEvent(GameEvent.CoinChangeFinsh)
end
--- 窗口显示[protected]
---@param furnitureData furniture_level_Item @窗口传参
function DeviceDescView:OnCreate(furnitureData, furnitureStatus)

    self:SetTitle(LanguageUtil:GetValue("Common_UItips_Details"))

    self.DeviceList = self.go_table.list_DeviceDesList

    --开启点击窗口外关闭弹窗
    self.ClickSpaceClose = true
    local spriteName = furnitureData.icon
    self.furnitureData = furnitureData;
    self.furnitureStatus = furnitureStatus

    self.go_table.simg_buildImg:LoadSprite(GameDefine.eResPath.AtlasFurniture .. spriteName, false, 1, function()
        -- print("详情界面动态图片加载完成")
    end)

    self.go_table.stmp_buildTitle.text = LanguageUtil:GetValue(furnitureData.name)
    self.go_table.stmp_buildMessage.text = LanguageUtil:GetValue(furnitureData.explain)

    if (DeviceDataInst:FurnitureIsOnTheWay(furnitureData.id)) then
        --我的时间
        local myTime = GameDataInst.SpeedTime
        self.go_table.stmp_time.text = TimeUtil.TimeStampToString(myTime < 0 and 0 or myTime, 3, false)
        self:StartTimer()
    end

    self:InitBtnShown(furnitureStatus)

    self:InitConditionView()
end

function DeviceDescView:InitBtnShown(furnitureStatus)
    self.furnitureStatus = furnitureStatus;

    --普通解锁
    local normalTab = self.furnitureData.cost_normal
    local normalCostType = normalTab[1]
    local normalCostNum = normalTab[2]

    --快速解锁
    --local fastTab = self.furnitureData.cost_fast
    local costType = DeviceData.UnlockType.Quick
    local costNum = DeviceDataInst:CalculationDiamondsByGold(normalCostNum) + DeviceDataInst:CalculationDiamondsByTime(self.furnitureData.waitstime) --钻石重新用金币计算向上取整

    local iconNameFast = GameDataInst:GetCostImageName(costType)
    self.go_table.img_IconImageFast:LoadSprite(GameDefine.eResPath.AtlasMain .. iconNameFast, false, 1)

    local iconNameNormal = GameDataInst:GetCostImageName(normalCostType)
    self.go_table.img_IconImageNormal:LoadSprite(GameDefine.eResPath.AtlasMain .. iconNameNormal, false, 1)

    local myDiamond = GameDataInst.Diamond
    if (myDiamond >= costNum) then
        self.go_table.stmp_costDiamond.text = costNum
    else
        self.go_table.stmp_costDiamond.text = "<color=#f66d93>" .. costNum .. "</color>"
    end

    local myGold = GameDataInst.Gold
    if (myGold >= normalCostNum) then
        self.go_table.stmp_costCoin.text = normalCostNum
        self.go_table.stmp_lockmeGold.text = normalCostNum
    else
        self.go_table.stmp_costCoin.text = "<color=#f66d93>" .. normalCostNum .. "</color>"
        self.go_table.stmp_lockmeGold.text = "<color=#f66d93>" .. normalCostNum .. "</color>"
    end

    self.go_table.sbtn_UseMe.gameObject:SetActive(false);
    self.go_table.sbtn_Using.gameObject:SetActive(false)
    self.go_table.sbtn_unlockme.gameObject:SetActive(false);
    self.go_table.sbtn_finishImme.gameObject:SetActive(false);
    self.go_table.sbtn_MyTime.gameObject:SetActive(false);
    self.go_table.sbtn_QuickTime.gameObject:SetActive(false);
    self.go_table.sbtn_Lockme.gameObject:SetActive(false)

    self.go_table.stmp_fastStr.text = LanguageUtil:GetValue("Common_button_buyfast")
    self.go_table.stmp_normalStr.text = LanguageUtil:GetValue("Common_button_buy")

    self.go_table.stmp_timefaststr.text = LanguageUtil:GetValue("Common_button_buyfast")
    self.go_table.stmp_timenormalstr.text = LanguageUtil:GetValue("Common_button_speedup")

    self.go_table.stmp_lockMeStr.text = LanguageUtil:GetValue("Common_unlock")

    if (self.furnitureStatus == DeviceData.furnitureStatus.Lock) then
        --未解锁
        --self.go_table.sbtn_unlockme.gameObject:SetActive(true);
        --self.go_table.sbtn_finishImme.gameObject:SetActive(true);
        self.go_table.sbtn_Lockme.gameObject:SetActive(true)

    elseif self.furnitureStatus == DeviceData.furnitureStatus.Notbought then
        --解锁 未购买
        self.go_table.sbtn_unlockme.gameObject:SetActive(true);
        self.go_table.sbtn_finishImme.gameObject:SetActive(true);

    elseif self.furnitureStatus == DeviceData.furnitureStatus.NotUseing then
        --解锁 未使用
        self.go_table.sbtn_UseMe.gameObject:SetActive(true);
        self.go_table.stmp_useText.text = LanguageUtil:GetValue("Common_button_use")
    elseif self.furnitureStatus == DeviceData.furnitureStatus.OnTheway then
        --运输中单独处理
        self.go_table.sbtn_MyTime.gameObject:SetActive(true);
        self.go_table.sbtn_QuickTime.gameObject:SetActive(true);
    elseif self.furnitureStatus == DeviceData.furnitureStatus.UnlockFinsh then
        self.go_table.sbtn_UseMe.gameObject:SetActive(true);
    else
        --已解锁 正在使用
        self.go_table.sbtn_Using.gameObject:SetActive(true)
        --self.go_table.sbtn_Using:SetGrayWithInteractable(true)

        self.go_table.stmp_usingText.text = LanguageUtil:GetValue("Common_button_useing")
    end

end

function DeviceDescView:InitConditionView()

    local ctTab = DeviceDataInst:FurnitureCondition(self.furnitureData.id)
    local tabStr = ctTab[1]
    local tabImg = ctTab[2]
    local count = 0
    --全部的词条
    local tab = {}
    for i, v in pairs(tabStr) do
        ---@type DeviceData.ConditionData
        local test = {
            ImageName = tabImg[i],
            ConditionStr = v,
            FurnitureId = self.furnitureData.id,
            IsTime = false,
        }

        table.insert(tab, test)
    end

    --克隆预制体
    for i, v in pairs(tab) do
        local itemNodeClone = CS.UnityEngine.Object.Instantiate(self.go_table.obj_MessageItem, self.go_table.obj_Message.transform)
        itemNodeClone.transform.gameObject:SetActive(true)
        local item = self:GetOrAddComponent(itemNodeClone, require("DeviceMesItem"), v)
        count = count + 1
    end

    if (self.furnitureStatus < DeviceData.furnitureStatus.Notbought) then
        --家具状态为 未购买和锁住状态  显示条件
        --前置条件
        ---@type furniture_Item
        local fData = ConfigManager.furniture_level[self.furnitureData.id]
        local paramFData = fData.term_num
        for i, v in pairs(fData.term_type) do
            local cStr
            local icon
            -- print(v)
            if (v == 8) then
                --人气条件
                ---@type furniture_properties_Item
                local cData = ConfigManager.furniture_properties[v]

                if (paramFData[i] <= GameDataInst.Popular) then
                    cStr = LanguageUtil:GetValue(cData.text) .. paramFData[i]
                    icon = cData.icon
                else
                    icon = cData.icon_lock
                    cStr = "<color=#f66d93>" .. LanguageUtil:GetValue(cData.text) .. paramFData[i] .. "</color>"
                end
                --TODO 其他条件

            end
            ---@type DeviceData.ConditionData
            local test = {
                ImageName = icon,
                ConditionStr = cStr,
                FurnitureId = self.furnitureData.id,
                IsTime = false,
            }
            local itemNodeClone = CS.UnityEngine.Object.Instantiate(self.go_table.obj_MessageItem, self.go_table.obj_Message.transform)
            itemNodeClone.transform.gameObject:SetActive(true)
            local item = self:GetOrAddComponent(itemNodeClone, require("DeviceMesItem"), test)
            -- table.insert(tab, test)
            count = count + 1
        end
    end

    if (self.furnitureStatus == DeviceData.furnitureStatus.OnTheway or self.furnitureStatus == DeviceData.furnitureStatus.Notbought) then
        --时间条件
        local str = LanguageUtil:GetValue("Commom_transittime") .. " "
        --时间条件
        --local timeStr = string.format(str .. "<color=#AD6692>%s</color>", TimeUtil.TimeToString(self.furnitureData.waitstime, 3, false))
        local timeStr = string.format(str .. TimeUtil.TimeToString(self.furnitureData.waitstime, 3, false))
        --初始图片
        local timeImg = "ui_icon_yunshu_60.60"

        ---@type DeviceData.ConditionData
        local test = {
            ImageName = timeImg,
            ConditionStr = timeStr,
            FurnitureId = self.furnitureData.id,
            IsTime = self.furnitureStatus == DeviceData.furnitureStatus.OnTheway,
        }
        local itemNodeClone = CS.UnityEngine.Object.Instantiate(self.go_table.obj_MessageItem, self.go_table.obj_Message.transform)
        itemNodeClone.transform.gameObject:SetActive(true)
        local item = self:GetOrAddComponent(itemNodeClone, require("DeviceMesItem"), test)
        -- table.insert(tab, test)
        count = count + 1
    end




    --LoopListViewHelper.InitCommonListView(self, self.DeviceList, tab, "DeviceMesItem")


    local h = self.go_table.obj_Message.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).sizeDelta.y
    local h3 = count * 58 - 100
    local h2 = h + count * 58 + 20
    local w = self.go_table.simg_messagebg.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).sizeDelta.x
    self.go_table.simg_messagebg.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).sizeDelta = Vector2.New(w, h2)
    -- self.go_table.simg_messagebg.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)):SetSizeWithCurrentAnchors(CS.UnityEngine.RectTransform.Axis.Vertical, w + 20)
    self:SetContentHeught(h3)

end

---事件处理
---@private
---@param id EventID 事件ID
function DeviceDescView:EventHandle(id, ...)
    if id == GameEvent.CoinChangeFinsh then
        --货币刷新了
        self:InitBtnShown(self.furnitureStatus)
        return
    end
    local tab = { ... }
    local fid = tab[1]
    if (fid ~= self.furnitureData.id) then
        return
    end
    if (id == GameEvent.FurnitureOnTheWayOverEvent) then
        --恢复成解锁未使用
        --self:InitBtnShown(DeviceData.furnitureStatus.NotUseing)
    elseif id == GameEvent.UpGradeFurnitureEvent then
        --开始倒计时
        --[[        if (tab[1] == self.furnitureData.id) then

                    if (tab[2] == DeviceData.UnlockType.Normal) then
                        self:InitBtnShown(DeviceData.furnitureStatus.OnTheway)
                        --我的时间
                        local myTime = GameDataInst.SpeedTime
                        self.go_table.stmp_time.text = TimeUtil.TimeStampToString(myTime, 3, false)
                        self:StartTimer()

                    end
                end]]
    elseif id == GameEvent.FurnitureUnlockByTimeEvent then
        --恢复成解锁未使用

        local isOver = tab[2]
        if (isOver) then
            --我的时间够用
            --self:InitBtnShown(DeviceData.furnitureStatus.NotUseing)
            self:Close()
        else
            --我的时间不够用
            self:InitBtnShown(DeviceData.furnitureStatus.OnTheway)
            --我的时间
            self.go_table.stmp_time.text = TimeUtil.TimeStampToString(0, 3, false)
        end
        -- GameDataInst:ChangePlayerProp(GameDefine.ePlayerProp.SpeedTime, -self.LeastTime)


    end
end
function DeviceDescView:StartTimer()

    if (self.Timer == nil) then

        self.Timer = TimerInst:GetTimerStart(1, self.OnTimer, self)
        self:OnTimer()
    end

end

function DeviceDescView:OnTimer()
    if (self.Timer == nil) then
        return
    end
    self.LeastTime = DeviceDataInst:GetFurnitureTiming(self.furnitureData.id)
    if (self.LeastTime <= 0) then
        TimerInst:StopAndClearTimer(self.Timer)
        self.Timer = nil
        --关闭某个指定二级框
        EventInst:Broadcast(GameEvent.ClosePopEvent, "DeviceView")

        --直接朝向场景中家具位置
        EventInst:Broadcast(GameEvent.RoomFurnitureLookAt, self.furnitureData.id)
        self:Close()
        return
    end
    --local timeStr = TimeUtil.TimeToString(time, 3, false)


    --计算钻石和时间的关系

    local costNum = DeviceDataInst:CalculationDiamondsByTime(self.LeastTime) --钻石重新用时间计算向上取整
    local myNum = GameDataInst.Diamond
    if (costNum <= myNum) then
        self.go_table.stmp_QuickCost.text = costNum
    else
        self.go_table.stmp_QuickCost.text = "<color=#f66d93>" .. costNum .. "</color>"
    end
    self.QuickCostDimond = costNum


end
---可用
---@protected
function DeviceDescView:OnEnable()
end

---不可用
---@protected
function DeviceDescView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceDescView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end
    if btn == self.go_table.sbtn_finishImme then
        --使用钻石直接解锁
        if (self.furnitureStatus == DeviceData.furnitureStatus.Lock) then
            --条件判定 钻石解锁
            local needTerm = self.furnitureData.term_num[1]
            if (needTerm > GameDataInst.Popular) then
                local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Popularity)
                UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needTerm - GameDataInst.Popular)
                return
            end
        end

        if (self.furnitureStatus == DeviceData.furnitureStatus.Notbought) then
            --钻石
            local needNum = DeviceDataInst:CalculationDiamondsByGold(self.furnitureData.cost_normal[2]) + DeviceDataInst:CalculationDiamondsByTime(self.furnitureData.waitstime) --消耗数量

            if (GameDataInst.Diamond < needNum) then
                local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Diamond)
                UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needNum - GameDataInst.Diamond)
                return
            end
            local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Diamond)
            UIInst:OpenUIDefine(UIDefine.DeviceConfirmView, nil, function()
                DeviceDataInst:UpGradeFurniture(DeviceData.UnlockType.Quick, self.furnitureData.id)
                self:Close()
            end, lackId)

        end

    elseif btn == self.go_table.sbtn_unlockme or btn == self.go_table.sbtn_Lockme then

        if (self.furnitureStatus == DeviceData.furnitureStatus.Lock) then
            --条件判定 金币解锁
            local needTerm = self.furnitureData.term_num[1]
            if (needTerm > GameDataInst.Popular) then
                local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Popularity)
                UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needTerm - GameDataInst.Popular)
                return
            end
        end
        if (self.furnitureStatus == DeviceData.furnitureStatus.Notbought) then
            --金币
            local needGold = self.furnitureData.cost_normal[2]--消耗数量
            local needType = self.furnitureData.cost_normal[1]--消耗类型
            if (GameDataInst.Gold < needGold) then
                local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Gold)
                UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needGold - GameDataInst.Gold)
                return
            end

            DeviceDataInst:UpGradeFurniture(DeviceData.UnlockType.Normal, self.furnitureData.id)
            self:Close()
        end

    elseif btn == self.go_table.sbtn_UseMe then
        if (self.furnitureStatus == DeviceData.furnitureStatus.NotUseing) then
            DeviceDataInst:ChangeUseFurniture(self.furnitureData.id)
            self:Close()
        end

    elseif btn == self.go_table.sbtn_checkBuild then
        UIInst:OpenUIDefine(UIDefine.DeviceSingleView, nil, self.furnitureData)

    elseif btn == self.go_table.sbtn_QuickTime then
        --使用钻石消耗运输时间
        local needNum = self.QuickCostDimond--消耗数量

        if (GameDataInst.Diamond < needNum) then
            local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Diamond)
            UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needNum - GameDataInst.Diamond)
            return
        end
        local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Diamond)
        UIInst:OpenUIDefine(UIDefine.DeviceConfirmView, nil, function()

            DeviceDataInst:UpGradeFurnitureByAddDiamond(self.furnitureData.id, needNum)
            self:Close()
        end, lackId)

    elseif btn == self.go_table.sbtn_MyTime then

        --使用我得加速时间来解锁家具

        if (self.LeastTime ~= nil) then
            if (self.LeastTime <= GameDataInst.SpeedTime) then
                --时间足够花费时间解锁家具
                DeviceDataInst:UpGradeFurnitureByTime(self.furnitureData.id, self.LeastTime)

            else

                if (GameDataInst.SpeedTime <= 0) then
                    local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.SpeedTime)
                    local time = DeviceDataInst:GetFurnitureTiming(self.furnitureData.id)
                    local needStr = TimeUtil.TimeToString(time, 3, false)
                    UIInst:OpenUIDefine(UIDefine.DeviceLackView, nil, lackId, needStr)
                else
                    --时间不足
                    local time = self.LeastTime

                    UIInst:OpenUIDefine(UIDefine.DeviceUseTimeView, nil, self.furnitureData.id, function()
                        --任性使用
                        if (GameDataInst.SpeedTime > 0) then
                            DeviceDataInst:UpGradeFurnitureByTime(self.furnitureData.id, self.LeastTime)
                        end
                        --self:Close()
                    end)
                end

            end

        end


    end

end
---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceDescView:OnClickToggle(toggle, isOn)
end

---点击超链接回调
function DeviceDescView:OnClickTmp(tmp, link)

end

---数据清理
---@protected
function DeviceDescView:OnDestroy()
    TimerInst:StopAndClearTimer(self.Timer)
    --DeviceDescView.ParentCls.OnDestroy(self)
end

return DeviceDescView