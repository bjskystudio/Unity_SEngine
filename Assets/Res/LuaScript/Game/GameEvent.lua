---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/9/30 11:25
---
local EventID = require("EventID")
---@class GameEvent
local GameEvent = {}

---测试事件
GameEvent.TestEvent1 = EventID:CreateEventID()

--region -------------Net-------------
--Http回调错误
GameEvent.HttpResponseError = EventID:CreateEventID()
--服务器连接成功
GameEvent.ServerConnected = EventID:CreateEventID()
--服务器关闭
GameEvent.ServerClosed = EventID:CreateEventID()
--服务器连接超时
GameEvent.ServerConnectTimeout = EventID:CreateEventID()
--服务器连接错误
GameEvent.ServerConnectError = EventID:CreateEventID()
--endregion -------------Net-------------


--region -------------GameData-------------
---玩家属性改变事件
GameEvent.GoldChange = EventID:CreateEventID()
GameEvent.DiamondChange = EventID:CreateEventID()
GameEvent.PopularChange = EventID:CreateEventID()
GameEvent.GhostChange = EventID:CreateEventID()
GameEvent.SpeedTimeChange = EventID:CreateEventID()
GameEvent.EnergyChange = EventID:CreateEventID()
--endregion

--region ------------- 场景事件 -------------
---场景改变事件
GameEvent.SceneChangeEvent = EventID:CreateEventID()

--endregion

--region -------------------- 房间事件 ------------------------
---房间场景完成运输家具
GameEvent.RoomFurnitureFinishTransport = EventID:CreateEventID()
---点击已运输完成家具  聚焦到房间中具体某个位置
GameEvent.RoomFurnitureLookAt = EventID:CreateEventID()
---特效播放完成
GameEvent.RoomFurnitureEffectFinish = EventID:CreateEventID()
--endregion -------------------- 房间事件 ------------------------

---家具升级事件
GameEvent.UpGradeFurnitureEvent = EventID:CreateEventID()
---房间栏位解锁事件
GameEvent.UpGradeFieldEvent = EventID:CreateEventID()
---更换当前某个家具事件
GameEvent.ChangeFurnitureEvent = EventID:CreateEventID()
---栏位未开启 点击后定位事件
GameEvent.ClickLockFieldEvent = EventID:CreateEventID()
---通知触发了家具更换事件
GameEvent.FurnitureTriggerEvent = EventID:CreateEventID()
---家具运输完成
GameEvent.FurnitureOnTheWayOverEvent = EventID:CreateEventID()
---花费时间 直接解锁家具
GameEvent.FurnitureUnlockByTimeEvent = EventID:CreateEventID()
---通知关闭当前界面弹窗
GameEvent.ClosePopEvent = EventID:CreateEventID()
---解锁栏位 或者钻石解锁  通知场景直接解锁
GameEvent.UnlockFurnitureImmediately = EventID:CreateEventID()

---旅馆属性升级事件
GameEvent.UpgradeAttributeEvent = EventID:CreateEventID()
---完成旅店任务事件
GameEvent.FinshHotelTaskEvent = EventID:CreateEventID()
---货币漂浮消失事件
GameEvent.GoinMoveEvent = EventID:CreateEventID()
---精力提升漂浮消失事件
GameEvent.EnergyMoveEvent = EventID:CreateEventID()
---掉落金币
GameEvent.TriggerCoin = EventID:CreateEventID()
---收取取金币
GameEvent.CoinCollected = EventID:CreateEventID()

---开启金币飞行
GameEvent.CoinFlyEvent = EventID:CreateEventID()

---货币数量改变完成时间 用于刷新货币UI中货币数量
GameEvent.CoinChangeFinsh = EventID:CreateEventID()

GameEvent.TileMapScale = EventID:CreateEventID()

GameEvent.TileMapPosition = EventID:CreateEventID()

---某个家具正在被使用
GameEvent.UseFurnitureEvent = EventID:CreateEventID()
---某个家具从正在被使用状态切换到未被使用状态
GameEvent.ExitFurnitureEvent = EventID:CreateEventID()

GameEvent.EggMachineProgress = EventID:CreateEventID()

GameEvent.BtnMachine = EventID:CreateEventID()
return GameEvent
