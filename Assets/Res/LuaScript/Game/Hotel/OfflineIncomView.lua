local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local HotelDataInst = require("HotelData"):GetInstance()
local TimeUtil = require("TimeUtil")
local GameData = require("GameData")
local EventManager = require("EventManager")

---@class OfflineIncomView : PopBase 窗口
---@field private go_table OfflineIncomView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local OfflineIncomView = Class("OfflineIncomView", PopBase)

---添加Events监听事件
function OfflineIncomView:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function OfflineIncomView:OnCreate(coinNum, offlineTime)
    -- self.ClickSpaceClose = true

    self:InitData(coinNum, offlineTime)
end

function OfflineIncomView:InitData(coinNum, offlineTime)
    self.go_table.stmp_titleStr.text = LanguageUtil:GetValue("Function_Build_afktime_name")
    self.go_table.stmp_offlinetimestr.text = LanguageUtil:GetValue("Common_afktime_param") --.. TimeUtil.TimeToString2(offlineTime)
    local attrNum = HotelDataInst:GetMyAttribute(HotelDataInst.attributeType.offlineIncom)
    local nowStr = string.format("%.1f", attrNum / TimeUtil.SECOND_OF_HOUR)
    self.go_table.stmp_offlineMaxTime.text = LanguageUtil:GetValue("Common_afktime_limit") .. nowStr .. "h"

    self.go_table.stmp_LockStr.text = LanguageUtil:GetValue("Common_button_get")
    self.go_table.stmp_unlockStr.text = LanguageUtil:GetValue("Common_button_getmore")

    self.go_table.stmp_showTime.text = TimeUtil.TimeToString(offlineTime, 3)

    self.myNum = coinNum
    self.specialNum = string.format("%.0f", coinNum * 1.2) --TODO  其他加成
    self.go_table.stmp_iconNumstr.text = self.myNum
    self.go_table.stmp_iconNumstr2.text = self.specialNum

    local h = self.go_table.simg_barimg.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).sizeDelta.y

    local myPec = offlineTime * 1.0 / attrNum
    -- print("百分比:" .. myPec)
    local w = myPec * 528
    --保底策略 不使进度条过段导致变形
    if (w <= 20) then
        w = 60
    end

    self.go_table.simg_barimg.gameObject:GetComponent(typeof(CS.UnityEngine.RectTransform)).sizeDelta = Vector2.New(w, h)


end

---事件处理
---@private
---@param id EventID 事件ID
function OfflineIncomView:EventHandle(id, args)
end

---可用
---@protected
function OfflineIncomView:OnEnable()
end

---不可用
---@protected
function OfflineIncomView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function OfflineIncomView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end
    local trans = btn.transform
    if btn == self.go_table.sbtn_cancle then
        --正常获取金币
        GameData:GetInstance():ChangePlayerProp(GameDefine.ePlayerProp.Gold, self.myNum)
        HotelDataInst.HotelOffineCoin = 0
        HotelDataInst.HotelOfflineTime = 0
        HotelDataInst:SaveLocalData()
        GameData:GetInstance():StartFlyCoin(trans, self.myNum)
        -- self:StartFlyCoin(btn.transform.localPosition)
        self:Close()
    elseif btn == self.go_table.sbtn_confirm then
        --特殊获取金币
        GameData:GetInstance():ChangePlayerProp(GameDefine.ePlayerProp.Gold, tonumber(self.specialNum))

        HotelDataInst.HotelOffineCoin = 0
        HotelDataInst.HotelOfflineTime = 0
        HotelDataInst:SaveLocalData()

        GameData:GetInstance():StartFlyCoin(trans, self.myNum)
        --GameData:GetInstance():StartFlyCoin(btn.transform.localPosition)
        self:Close()
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function OfflineIncomView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function OfflineIncomView:OnDestroy()
    --OfflineIncomView.ParentCls.OnDestroy(self)
end

return OfflineIncomView