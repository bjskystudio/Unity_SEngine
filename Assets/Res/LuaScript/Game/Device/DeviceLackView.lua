local UIBase = require("UIBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local PopBase = require("PopBase")
local ConfigManager = require("ConfigManager")
local GameDataInst = require("GameData"):GetInstance()

---@class DeviceLackView : PopBase 窗口
---@field private go_table DeviceLackView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local DeviceLackView = Class("DeviceLackView", PopBase)

---添加Events监听事件
function DeviceLackView:Awake()

end
--- 窗口显示[protected]
---@param ... any @窗口传参
function DeviceLackView:OnCreate(lackId, lackNum)

    --开启点击窗口外关闭弹窗
    self.ClickSpaceClose = true

    self.lackId = lackId--缺的物品id

    ---@type Items_Item[]
    self.lackWpData = ConfigManager.Items[self.lackId]
    if (self.lackWpData == nil) then
        return
    end
    local imgName = self.lackWpData.icon

    --物品图片
    self.go_table.img_wupinImg:LoadSprite(GameDefine.eResPath.AtlasMain .. imgName, false, 1)
    self.go_table.img_ggWupinImg:LoadSprite(GameDefine.eResPath.AtlasMain .. imgName, false, 1)
    --物品名称
    self.go_table.stmp_wpName.text = LanguageUtil:GetValue(self.lackWpData.name)
    --还需要多少
    self.go_table.stmp_needNum.text = "还需要" .. "：" .. string.format("<color=#AD6692>%s</color>", lackNum)
    --物品描述
    self.go_table.stmp_miaosu.text = LanguageUtil:GetValue(self.lackWpData.text)

    self.go_table.stmp_huoqu.text = "获取方式"

    --广告获取次数 TODO
    self.go_table.stmp_ggcishu.text = "0/2"



    --广告获取数量 TODO
    self.AddNum = 1000000
    self.go_table.stmp_guanggaohuoqu.text = "+" .. self.AddNum

    if (lackId == 1000003 or lackId == 1000004) then
        self.go_table.sbtn_lookgg.gameObject:SetActive(false)
        self.go_table.stmp_ggcishu.gameObject:SetActive(false)
    end

    if lackId == 1000003 then
        self.go_table.stmp_huoqu1.text = LanguageUtil:GetValue("Common_getrenqi")
    elseif lackId == 1000004 then
        self.go_table.stmp_huoqu1.text = LanguageUtil:GetValue("Common_button_gainspeed")
    else
        self.go_table.stmp_huoqu1.text = LanguageUtil:GetValue("Common_button_speed_Sure")
    end


end

---事件处理
---@private
---@param id EventID 事件ID
function DeviceLackView:EventHandle(id, args)
end

---可用
---@protected
function DeviceLackView:OnEnable()
end

---不可用
---@protected
function DeviceLackView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function DeviceLackView:OnClickBtn(btn)
    if btn.name == "@_btn_anyClose" then
        return
    end

    if (btn == self.go_table.sbtn_lookgg) then
        if (self.lackId == 1000001) then
            GameDataInst:ChangePlayerProp(GameDefine.ePlayerProp.Gold, self.AddNum)
        elseif self.lackId == 1000002 then
            GameDataInst:ChangePlayerProp(GameDefine.ePlayerProp.Diamond, self.AddNum)
        end
        self:Close()
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function DeviceLackView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function DeviceLackView:OnDestroy()
    --DeviceLackView.ParentCls.OnDestroy(self)
end

return DeviceLackView