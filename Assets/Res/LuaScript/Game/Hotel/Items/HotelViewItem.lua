local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local GameDataInst = require("GameData"):GetInstance()
local DeviceData = require("DeviceData")
local UIManager = require("UIManager")
local ListItemBase = require("ListItemBase")
local HotelDataInst = require("HotelData"):GetInstance()

---@class HotelViewItem : UIComBase 窗口
---@field private go_table HotelViewItem_GoTable GoTable
---@field private ParentCls UIComBase 父窗口类
local HotelViewItem = Class("HotelViewItem", ListItemBase)

---添加Events监听事件
function HotelViewItem:Awake()
    self:AddEvent(GameEvent.CoinChangeFinsh)
end
--- 窗口显示[protected]
---@param ... any @窗口传参
function HotelViewItem:OnCreate()

end

---@public
---刷新数据
---@param cList table
function HotelViewItem:OnRefreshItemData(cList)

    ---@type hotlebuild_Item
    local DataItem = cList[1]

    self.DataItem = DataItem
    self.go_table.stmp_taskName.text = LanguageUtil:GetValue(DataItem.text)

    --暂时没有图片 TODO
    self.go_table.simg_teskImg:LoadSprite(GameDefine.eResPath.AtlasHotel .. DataItem.icon, false, 1)
    local rewardNum = DataItem.award
    if (rewardNum > 0) then
        self.go_table.obj_extraReward:SetActive(true)
        self.go_table.stmp_rewardNum.text = "+" .. rewardNum
    else
        self.go_table.obj_extraReward:SetActive(false)
    end

    local needTerm = DataItem.term
    local myTerm = GameDataInst.Popular
    local termIconName
    if (myTerm >= needTerm) then
        self.go_table.stmp_conStr.text = needTerm
        termIconName = "ui_icon_renqi_60.60"
    else
        self.go_table.stmp_conStr.text = "<color=#f66d93>" .. needTerm .. "</color>"
        termIconName = "ui_icon_renqi_lock_60.60"
    end
    self.go_table.simg_conImg:LoadSprite(GameDefine.eResPath.AtlasDevice .. termIconName, false, 1)
    local costData = DataItem.cost
    self.costData = costData
    local costType = costData[1]
    local costNum = costData[2]

    local iconName = GameDataInst:GetCostImageName(costType)
    self.go_table.img_IconImage:LoadSprite(GameDefine.eResPath.AtlasMain .. iconName, false, 1)

    self:InitFinshCoin()

    local isOver = HotelDataInst:TaskIsFinsh(DataItem.id)
    if (isOver) then
        self.go_table.sbtn_finishImme.gameObject:SetActive(false)
        self.go_table.obj_condition:SetActive(false)
        self.go_table.simg_finshimg.gameObject:SetActive(true)

    else
        self.go_table.obj_condition:SetActive(true)
        self.go_table.sbtn_finishImme.gameObject:SetActive(true)
        self.go_table.simg_finshimg.gameObject:SetActive(false)
    end

    --关闭手指
    self.go_table.obj_finger:SetActive(false)

    self:DealFinger(isOver)
end
function HotelViewItem:DealFinger(isOver)

    if (isOver) then
        --关闭手指
        self.go_table.obj_finger:SetActive(false)
    else
        if (table.ContainsValue(HotelDataInst.FinishTask, self.DataItem.id)) then
            self.go_table.obj_finger:SetActive(true)
            --[[            local breath = self.go_table.transform:Find("@_obj_finger").gameObject:GetComponent(typeof(CS.BreathTween))
                        if breath ~= nil then
                            breath:Start()
                        end]]
            local seq = CS.DG.Tweening.DOTween.Sequence()
            seq:Append(self.go_table.obj_finger.transform:DOLocalMove(Vector3.New(self.go_table.obj_finger.transform.localPosition.x + 5, self.go_table.obj_finger.transform.localPosition.y, 0), 1))
            seq:Append(self.go_table.obj_finger.transform:DOLocalMove(Vector3.New(self.go_table.obj_finger.transform.localPosition.x, self.go_table.obj_finger.transform.localPosition.y, 0), 1))
            seq:SetLoops(-1)
        end
    end


end
function HotelViewItem:InitFinshCoin()
    local costData = self.costData
    local costType = costData[1]
    local costNum = costData[2]

    local needNum
    if (costType == DeviceData.UnlockType.Normal) then
        needNum = GameDataInst.Gold
    elseif costType == DeviceData.UnlockType.Quick then
        needNum = GameDataInst.Diamond
    end
    if (needNum >= costNum) then
        self.go_table.stmp_costNum.text = costNum
    else
        self.go_table.stmp_costNum.text = "<color=#f66d93>" .. costNum .. "</color>"
    end
end

---事件处理
---@private
---@param id EventID 事件ID
function HotelViewItem:EventHandle(id, ...)
    local tab = { ... }
    if id == GameEvent.CoinChangeFinsh then
        self:InitFinshCoin()
    end
end

---可用
---@protected
function HotelViewItem:OnEnable()
end

---不可用
---@protected
function HotelViewItem:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function HotelViewItem:OnClickBtn(btn)

    if btn.name == "@_btn_anyClose" then
        return
    end

    if (btn == self.go_table.sbtn_finishImme) then

        local condition = self.DataItem.front
        if (condition > 0) then

            if (HotelDataInst.HotelTaskMap[condition] == nil) then
                local cData = Config.hotlebuild[condition]
                local str = LanguageUtil:GetValue("Hotlebuild_tips") .. LanguageUtil:GetValue(cData.text)
                ---@type UISetting
                local setting = {
                    Layer = UILayerEnum.InfoLayer,
                    ShowBgMask = false,

                }
                UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceCommonTip, setting, str)
                return
            end


        end

        local needTerm = self.DataItem.term
        local myTerm = GameDataInst.Popular
        if (myTerm < needTerm) then
            local lackId = GameDataInst:GetPlayerPropItemId(GameDefine.ePlayerProp.Popularity)
            DeviceData:OpenCommonLackView(lackId, needTerm - myTerm)
            return
        end

        local costData = self.DataItem.cost
        local costType = costData[1]
        local costNum = costData[2]

        local myNum
        local id
        if (costType == DeviceData.UnlockType.Normal) then
            myNum = GameDataInst.Gold
            id = GameDefine.ePlayerProp.Gold
        elseif costType == DeviceData.UnlockType.Quick then
            myNum = GameDataInst.Diamond
            id = GameDefine.ePlayerProp.Diamond
        end
        if (myNum < costNum) then
            local lackId = GameDataInst:GetPlayerPropItemId(id)
            DeviceData:OpenCommonLackView(lackId, costNum - myNum)
            return
        end

        HotelDataInst:TaskFinsh(self.DataItem.id)
        self.OwnerUI:Close()
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function HotelViewItem:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function HotelViewItem:OnDestroy()
    --HotelViewItem.ParentCls.OnDestroy(self)
end

return HotelViewItem