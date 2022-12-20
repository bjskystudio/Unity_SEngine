local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local HotelDataInt = require("HotelData"):GetInstance()
local DeviceData = require("DeviceData")
local GameData = require("GameData")
local GameDefine = GameDefine
---@class HotelUpGradeView : PopBase 窗口
---@field private go_table HotelUpGradeView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local HotelUpGradeView = Class("HotelUpGradeView", PopBase)

---添加Events监听事件
function HotelUpGradeView:Awake()
    self:AddEvent(GameEvent.UpgradeAttributeEvent)
end
--- 窗口显示[protected]
---@param type number @窗口传参
function HotelUpGradeView:OnCreate(type)
    self.ClickSpaceClose = true
    self:SetTitle(LanguageUtil:GetValue("Common_UItips_Details"))
    self:InitData(type)

end
function HotelUpGradeView:InitData(type)

    self.type = type
    --我的当前等级
    self.level = HotelDataInt:GetAttributeLevel(type)
    self.go_table.stmp_buildTitle.text = LanguageUtil:GetValue(HotelDataInt:GetAttributeName(type))

    self.go_table.stmp_levelStr.text = "Lv.<color=#AD6692>" .. self.level .. "</color>"
    self.go_table.stmp_buildMessage.text = LanguageUtil:GetValue(HotelDataInt:GetAttributeMessage(type))

    self.go_table.simg_buildImg:LoadSprite(GameDefine.eResPath.AtlasHotel .. HotelDataInt:GetAttributeImg(type), false, 1)

    --内容对应的图片
    self.go_table.simg_showImg:LoadSprite(GameDefine.eResPath.AtlasHotel .. HotelDataInt:GetAttributeContentImg(type), false, 1)

    --内容文字

    local strData = HotelDataInt:GetAttributeStr_myLevel(type)
    local nowStr = strData[1]
    local nextStr = strData[2]
    self.go_table.stmp_showStr.text = nowStr
    --下一级数据文本
    if (nextStr ~= nil) then
        self.go_table.obj_nextObj:SetActive(true)
        self.go_table.stmp_nextNum.text = nextStr
    else
        self.go_table.obj_nextObj:SetActive(false)
    end

    local isMax = HotelDataInt:AttributeIsMaxLevel(self.type)
    if (isMax) then
        self.go_table.sbtn_upBtn:SetGrayWithInteractable(isMax)
    end

    self.costNum = HotelDataInt:GetAttributeUpCost(type)
    self.go_table.stmp_upNum.text = self.costNum
end

---事件处理
---@private
---@param id EventID 事件ID
function HotelUpGradeView:EventHandle(id, ...)
    local tab = { ... }
    if (id == GameEvent.UpgradeAttributeEvent) then
        local type = tab[1]
        self:InitData(type)
    end
end

---可用
---@protected
function HotelUpGradeView:OnEnable()
end

---不可用
---@protected
function HotelUpGradeView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function HotelUpGradeView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end

    if btn == self.go_table.sbtn_upBtn then
        if (HotelDataInt:AttributeIsMaxLevel(self.type)) then
            --UIManager:GetInstance():OpenUIDefine(UIDefine.DeviceCommonTip, nil, LanguageUtil:GetValue("Common_Title_transport"))
            return
        end

        --TODO 点击按钮升级属性
        local myDiamond = GameData:GetInstance().Diamond
        local lackNum = self.costNum - myDiamond
        if (lackNum > 0) then
            local lackId = GameData:GetInstance():GetPlayerPropItemId(GameDefine.ePlayerProp.Diamond)
            DeviceData:GetInstance():OpenCommonLackView(lackId, lackNum)
        end
        --升级属性
        HotelDataInt:UpgradeAttribute(self.type)
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function HotelUpGradeView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function HotelUpGradeView:OnDestroy()
    --HotelUpGradeView.ParentCls.OnDestroy(self)
end

return HotelUpGradeView