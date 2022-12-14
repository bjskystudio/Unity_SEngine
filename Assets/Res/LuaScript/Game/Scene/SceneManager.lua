---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Simon.L.
--- 房间场景管理
--- DateTime: 2022/10/20 15:56
---

local UIInst = require("UIManager"):GetInstance()
local EventInst = require("EventManager"):GetInstance()
local GameEvent = require("GameEvent")
local ConfigManager = require("ConfigManager")
local Log = require("Log")
local MapManager = require("MapManager")
local UILayerEnum = UILayerEnum
local UIDefine = UIDefine
local IsNil = IsNil

---@class SceneManager : Singleton @场景管理器
---@field SceneType number 当前场景类型
---@field RoomId number 当前房间id
---@field CurSceneView SceneViewBase 当前场景view
local SceneManager = Class("SceneManager", Singleton)
SceneManager.SceneConstSortOrder = 1000

---@class SceneManager.eSceneType 场景类型
---@field Hotel number 旅店（外景）
---@field Room number 房间
---
SceneManager.eSceneType = {
    Hotel = 1,
    Room = 2
}

---@class SceneManager.eRoomType 房间类型
---@field Public number 公共房间
---@field Personal number 个人房间
---
SceneManager.eRoomType = {
    Public = 1,
    Personal = 2
}

---@class SceneManager.eRoomId 房间编号
---@field Lounge number 休息室
---@field Restaurant number 餐厅
---@field SpringRoom number 温泉
SceneManager.eRoomId = {
    Hotel = 1000,
    Lounge = 1001,
    Restaurant = 1002,
    SpringRoom = 1003
}

---@private
function SceneManager:__init()
    self.SceneType = SceneManager.eSceneType.Hotel
    self.RoomId = SceneManager.eRoomId.Hotel
end

----是不是公共房
---@param roomId number 房间id，没给就是当前房间
function SceneManager:IsPublicRoom(roomId)
    roomId = roomId or self.RoomId
    if roomId > 0 then
        local cfg = Config.room_config[roomId]
        return cfg.type == 1
    else
        return false
    end
end

---初始化进入Hotel
---@param first boolean 首次进入
function SceneManager:EnterHotel(first)

    first = first or false
    if first then --首次
        ---地图初始化
        MapManager:GetInstance():InitMapConfig()
        ---打开地图界面
        UIInst:OpenUIDefine(UIDefine.MapView, {
            Layer = UILayerEnum.SceneLayer,
            ConstOrder = SceneManager.SceneConstSortOrder - 1
        })
        ---打开主界面
        UIInst:OpenUIDefine(UIDefine.MainView, {
            Layer =  UILayerEnum.SceneLayer,
            ConstOrder = SceneManager.SceneConstSortOrder + 1
        })
    end
    self.CurSceneView = UIInst:OpenUIDefine(UIDefine.HotelSceneView, {
        Layer = UILayerEnum.SceneLayer,
        ConstOrder = SceneManager.SceneConstSortOrder
    })
end

---切换场景到旅店
function SceneManager:ChangeToHotel()
    --if self.SceneType == SceneManager.eSceneType.Hotel then
    --    return
    --end
    self.SceneType = SceneManager.eSceneType.Hotel
    self.RoomId = SceneManager.eRoomId.Hotel
    ---关闭当前
    if self.CurSceneView then
        self.CurSceneView:Close()
    end
    --进入酒店
    self:EnterHotel()
    ---场景改变事件
    EventInst:Broadcast(GameEvent.SceneChangeEvent, self.SceneType, self.RoomId)
end

---切换场景到房间
function SceneManager:ChangeToRoom(roomId)
    if roomId == self.RoomId then
        return
    end
    self.RoomId = roomId
    self.SceneType = SceneManager.eSceneType.Room

    local roomCfg = ConfigManager.room_config[roomId]
    if (roomCfg.type == 0) then
        return
    end
    if not IsNil(roomCfg) then
        ---关闭当前
        if self.CurSceneView then
            self.CurSceneView:Close()
        end
        if roomCfg.type == 1 then

            self.CurSceneView = self:LoadPublicRoom(roomCfg)
        else
            ---单间是2
            self.CurSceneView = self:LoadPersonalRoom(roomCfg)
        end
        if not IsNil(self.CurSceneView) then
            ---场景改变事件
            EventInst:Broadcast(GameEvent.SceneChangeEvent, self.SceneType, self.RoomId)
        end
    else
        Log.Error("无效的房间id.." .. roomId)
    end

end
---@private
---@param roomCfg house_Item
---@return SceneViewBase
function SceneManager:LoadPublicRoom(roomCfg)
    ---@type UISetting
    local sceneUISetting = {
        Layer = UILayerEnum.SceneLayer,
        ConstOrder = SceneManager.SceneConstSortOrder
    }
    if roomCfg.Id == SceneManager.eRoomId.Lounge then
        return UIInst:OpenUIDefine(UIDefine.LoungeSceneView, sceneUISetting)
    else
        return nil
    end
end

return SceneManager