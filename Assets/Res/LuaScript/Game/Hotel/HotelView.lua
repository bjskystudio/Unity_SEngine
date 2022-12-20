local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local UIManager = require("UIManager")
local UIDefine = require("UIDefine")
local HotelDataInt = require("HotelData"):GetInstance()
local Config = Config
local LoopListViewHelper = LoopListViewHelper

---@class HotelView : PopBase 窗口
---@field private go_table HotelView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local HotelView = Class("HotelView", PopBase)

---添加Events监听事件
function HotelView:Awake()
    self:AddEvent(GameEvent.UpgradeAttributeEvent)
    self:AddEvent(GameEvent.FinshHotelTaskEvent)
    self:AddEvent(GameEvent.MoveToHotelTask)
end
--- 窗口显示[protected]
---@param ... any @窗口传参
function HotelView:OnCreate()
    self.ClickSpaceClose = true
    self:SetTitle(LanguageUtil:GetValue("Common_UItips_bulid"))
    self.HotelList = self.go_table.list_HotelList
    self:InitData()

    self:InitListView()
end
function HotelView:InitData()
    self.go_table.stmp_cardName_Tip.text = LanguageUtil:GetValue(HotelDataInt.attributeName.tipName)
    self.go_table.stmp_cardName_OfflineIncom.text = LanguageUtil:GetValue(HotelDataInt.attributeName.offlineName)
    self.go_table.stmp_cardName_Propaganda.text = LanguageUtil:GetValue(HotelDataInt.attributeName.hotelPublicity)

    self.go_table.stmp_level_Tip.text = "Lv." .. HotelDataInt.TipLevel
    self.go_table.stmp_level_OfflineIncom.text = "Lv." .. HotelDataInt.OfflineIncomLevel

    self.go_table.stmp_level_Propaganda.text = "Lv." .. HotelDataInt.HotelPublicityLevel
end
function HotelView:InitListView()
    ---@type hotlebuild_Item[]
    self.DataList = table.toArray(Config.hotlebuild)

    HotelDataInt.SortDataList = {}
    for i, v in pairs(self.DataList) do
        HotelDataInt.SortDataList[i] = {}
        HotelDataInt.SortDataList[i][1] = v

        local status = HotelDataInt:TaskIsFinsh(v.id) and 1 or 0
        HotelDataInt.SortDataList[i][2] = { status, v.sort }
    end

    table.sort(HotelDataInt.SortDataList, function(a, b)
        local c = a[2]
        local d = b[2]

        for i = 1, #c do
            if (c[i] < d[i]) then
                return true
            elseif d[i] < c[i] then
                break
            end
        end
        return false
    end)

    -- LoopListViewHelper.InitCommonListView(self, self.HotelList, HotelDataInt.SortDataList, "HotelViewItem")

    self.listViewGetItem = function(_, _, index)
        index = index + 1
        if index < 0 or index > #HotelDataInt.SortDataList then
            return nil
        end
        local listViewItem = self.HotelList:NewListViewItem("HotelViewItem")
        ---@type ListItemBase
        local itemCom = self:GetOrAddComponent(listViewItem.gameObject, require("HotelViewItem"))
        itemCom:_InitItemData(HotelDataInt.SortDataList[index], index, HotelDataInt.SortDataList)
        return listViewItem
    end
    self.HotelList:InitListView(#HotelDataInt.SortDataList, handler(self, self.listViewGetItem))


end

--定位指定栏位
function HotelView:SetListViewLocation(taskId)
    --定位逻辑
    local targetIndex = 0
    for i, v in pairs(HotelDataInt.SortDataList) do
        if (v[1].id == taskId) then
            targetIndex = i
        end
    end

    self.HotelList:MovePanelToItemIndex(targetIndex, 200)
end

---事件处理
---@private
---@param id EventID 事件ID
function HotelView:EventHandle(id, ...)
    local tab = { ... }
    if (id == GameEvent.UpgradeAttributeEvent) then
        self:InitData()
    elseif id == GameEvent.FinshHotelTaskEvent then
        HotelDataInt.SortDataList = {}
        for i, v in pairs(self.DataList) do
            HotelDataInt.SortDataList[i] = {}
            HotelDataInt.SortDataList[i][1] = v

            local status = HotelDataInt:TaskIsFinsh(v.id) and 1 or 0
            HotelDataInt.SortDataList[i][2] = { status, v.sort }
        end

        table.sort(HotelDataInt.SortDataList, function(a, b)
            local c = a[2]
            local d = b[2]

            for i = 1, #c do
                if (c[i] < d[i]) then
                    return true
                elseif d[i] < c[i] then
                    break
                end
            end
            return false
        end)
        self.HotelList:RefreshAllShownItem()

    elseif id == GameEvent.MoveToHotelTask then
        self:SetListViewLocation(tab[1])
    end
end

---可用
---@protected
function HotelView:OnEnable()
end

---不可用
---@protected
function HotelView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function HotelView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end

    if btn == self.go_table.sbtn_tip then
        --小费上限
        UIManager:GetInstance():OpenUIDefine(UIDefine.HotelUpGradeView, nil, HotelDataInt.attributeType.tip)
    elseif btn == self.go_table.sbtn_offline then
        --离线收益
        UIManager:GetInstance():OpenUIDefine(UIDefine.HotelUpGradeView, nil, HotelDataInt.attributeType.offlineIncom)
    elseif btn == self.go_table.sbtn_propaganda then
        --旅店宣传
        UIManager:GetInstance():OpenUIDefine(UIDefine.HotelUpGradeView, nil, HotelDataInt.attributeType.hotelPublicity)
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function HotelView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function HotelView:OnDestroy()
    --HotelView.ParentCls.OnDestroy(self)
end

return HotelView