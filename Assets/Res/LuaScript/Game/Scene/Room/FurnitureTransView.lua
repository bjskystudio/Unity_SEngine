---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by simon.L.
--- DateTime: 2022/10/31 17:45
---

local UIBase = require("UIBase")
local PopBase = require("PopBase")
local UIComBase = require("UIComBase")
local GameEvent = require("GameEvent")
local Log = require("Log")
local ConfigManager = require("ConfigManager")
local GameDefine = GameDefine
local ResLoadInst = require("ResLoadManager"):GetInstance()
local EventInst = require("EventManager"):GetInstance()
local LoungeSceneView = require("LoungeSceneView")
local TimerInst = require("TimerManager"):GetInstance()
local DeviceDataInst = require("DeviceData"):GetInstance()

---@class eEffectType 特效类型
local eEffectType = {
    Normal = 1,
    Wall = 2,
    Floor = 3
}

---@class FurnitureTransView : UIComBase 窗口
---@field private go_table FurnitureTransView_GoTable GoTable
---@field private ParentCls UIBase 父窗口类
local FurnitureTransView = Class("FurnitureTransView", UIComBase)

---添加Events监听事件
function FurnitureTransView:Awake()
    self.FloorEffectPlaying = false
end
--- 窗口显示[protected]
---@param state LoungeSceneView.FurnitureNodeState
---@param furnitureId number 家具id
function FurnitureTransView:OnCreate(state, furnitureId)
    self.State = state
    self.FurnitureId = furnitureId
    local fieldId = DeviceDataInst:GetFieldId(furnitureId)
    self.EffectType = Config.furniture[fieldId].Effects_type
    self.EffectPoses = Config.furniture[fieldId].Effects_coord

    self:ShowStateView()
end

function FurnitureTransView:ShowStateView()
    ----显示使用中的家具id
    --if self.UseFurnitureId  ~= nil then
    --    local cfg = Config.furniture_level[self.UseFurnitureId]
    --    if cfg ~= nil then
    --        self.go_table.simg_unlock:LoadSprite(GameDefine.eResPath.AtlasRoomFurniture.."Lounge/"..cfg.art,true,1)
    --    end
    --end
    self.go_table.sbtn_unlock.gameObject:SetActive(self.State == LoungeSceneView.FurnitureNodeState.TransFinish)
    ---运输特效
    if self.State == LoungeSceneView.FurnitureNodeState.TransPort then
        if self.EffectType == eEffectType.Normal then
            local fxPath = GameDefine.eResPath.EffectPath .. "Fx_Prefab/Fx_Glow_Equipment"
            self:LoadParticle(fxPath, self.go_table.obj_effect, handler(self, self.OnLoadResCompleted))
        else
            ---墙和地板
            ---@type LoungeSceneView
            local loungeScene = self.OwnerUI
            loungeScene:StartNodeEffect(self.FurnitureId)
        end
    elseif self.State == LoungeSceneView.FurnitureNodeState.TransFinish then
        self:ShowTransFinish()
    end
end

---@param uiParticle Coffee.UIExtensions.UIParticle @ ui 粒子
function FurnitureTransView:OnLoadResCompleted(uiParticle)
    self.EffectTransGo = uiParticle.gameObject
    uiParticle:Play()
end

---显示运输完成
function FurnitureTransView:ShowTransFinish()
    if self.EffectTransGo ~= nil then
        self.EffectTransGo:DestroyGameObj()
    end
    if self.EffectType ~= eEffectType.Normal then
        ---墙和地板
        ---@type LoungeSceneView
        local loungeScene = self.OwnerUI
        loungeScene:StopNodeEffect(self.FurnitureId)
    end
    self.go_table.sbtn_unlock.gameObject:SetActive(true)
end

function FurnitureTransView:PlayFinishEffect()


    EventInst:Broadcast(GameEvent.RoomFurnitureFinishTransport, self.FurnitureId)
    if self.EffectType == eEffectType.Normal then
        ---播放属性预制
        self.OwnerUI:ShowShuXingUi(self.FurnitureId)
        EventInst:Broadcast(GameEvent.RoomFurnitureEffectFinish, self.FurnitureId)
        self:LoadFinishParticle()
    elseif self.EffectType == eEffectType.Wall then
        self.go_table.obj_spine:SetActive(true)
        --位置
        if #self.EffectPoses > 0 then
            self.go_table.obj_spine.transform.localPosition = Vector3.New(self.EffectPoses[1][1], self.EffectPoses[1][2], 0)
        end
        ---@type SEngine.UI.UISpine
        local spine = self.go_table.obj_spine:GetComponent(typeof(CS.SEngine.UI.UISpine))
        spine:PlayAnim("idle", false, function()
            EventInst:Broadcast(GameEvent.RoomFurnitureEffectFinish, self.FurnitureId)
            self.OwnerUI:RemoveTransView(self.FurnitureId)
        end)
    elseif self.EffectType == eEffectType.Floor then
        ---播放属性预制
        self.OwnerUI:ShowShuXingUi(self.FurnitureId)
        self.FloorEffectPoses = {}
        for i = 1, #self.EffectPoses do
            table.insert(self.FloorEffectPoses, Vector3.New(self.EffectPoses[i][1], self.EffectPoses[i][2], 0))
        end
        if #self.FloorEffectPoses > 0 then
            self.FloorEffectPlaying = true
            self.FloorEffectTimer = TimerInst:GetTimerStartImme(0.5, self.OnFloorEffectTimer, self, false)
        end
    end
    self.go_table.sbtn_unlock.gameObject:SetActive(false)
end

---连续播放地板特效
function FurnitureTransView:OnFloorEffectTimer()
    if #self.FloorEffectPoses > 0 then
        self.CurFloorEffectPos = table.remove(self.FloorEffectPoses, 1)
        self:LoadFinishParticle()
    else
        TimerInst:StopAndClearTimer(self.FloorEffectTimer)
        self.ParticleDestroyTimer = TimerInst:GetTimerStart(5, function()
            self.FloorEffectPlaying = false
            --EventInst:Broadcast(GameEvent.RoomFurnitureEffectFinish, self.FurnitureId)
            self.OwnerUI:ShowFurnitureImage(self.FurnitureId, false)
            self.OwnerUI:RemoveTransView(self.FurnitureId)
        end, self, true)
    end
end

function FurnitureTransView:LoadFinishParticle()
    local fxPath = GameDefine.eResPath.EffectPath .. "Fx_Prefab/Fx_Equipment_Ribbon"
    self:LoadParticle(fxPath, self.go_table.obj_effect, handler(self, self.OnLoadRibbonCompleted))
end

---@param uiParticle Coffee.UIExtensions.UIParticle @ ui 粒子
function FurnitureTransView:OnLoadRibbonCompleted(uiParticle)
    self.EffectTransGo = uiParticle.gameObject
    if self.FloorEffectPlaying then
        self.EffectTransGo.transform.localPosition = self.CurFloorEffectPos
        self.EffectTransGo.transform:SetLocalScaleXYZ(0.5)
    end
    uiParticle:Play()
    if not self.FloorEffectPlaying then
        self.ParticleDestroyTimer = TimerInst:GetTimerStart(5, function()
            self.OwnerUI:RemoveTransView(self.FurnitureId)
        end, self, true)
    end
end

---事件处理
---@private
---@param id EventID 事件ID
function FurnitureTransView:EventHandle(id, args)
end

---可用
---@protected
function FurnitureTransView:OnEnable()
end

---不可用
---@protected
function FurnitureTransView:OnDisable()
end

---点击Button回调
---@param btn UnityEngine.UI.Button 按钮
function FurnitureTransView:OnClickBtn(btn)
    if btn == self.go_table.sbtn_unlock then
        --点击运输完成
        self:PlayFinishEffect()
    end
end

---点击Toggle回调
---@protected
---@param toggle UnityEngine.UI.Toggle Toggle
---@param isOn boolean 是否选中
function FurnitureTransView:OnClickToggle(toggle, isOn)
end

---数据清理
---@protected
function FurnitureTransView:OnDestroy()
    --FurnitureTransView.ParentCls.OnDestroy(self)
    if self.EffectTransGo then
        self.EffectTransGo:DestroyGameObj()
    end
    if self.ParticleDestroyTimer ~= nil then
        TimerInst:StopAndClearTimer(self.ParticleDestroyTimer)
    end

    TimerInst:StopAndClearTimer(self.FloorEffectTimer)
    self.gameObject:DestroyGameObj()
end

return FurnitureTransView