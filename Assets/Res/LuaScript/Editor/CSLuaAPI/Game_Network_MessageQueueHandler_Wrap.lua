---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Game.Network.MessageQueueHandler : SEngine.MonoSingleton
---@field static luaOnRecvMessage Game.Network.MessageQueueHandler.LuaOnRecvMessage
local MessageQueueHandler = {}

function MessageQueueHandler:UnInit() end

---@param cmd int32
---@param msgData Game.Network.MessageData
function MessageQueueHandler:PushQueue(cmd,msgData) end

---@param msg string
---@param state int16
function MessageQueueHandler:PushError(msg,state) end

---@param cmd int32
---@param cacheBuff System.Byte[]
---@param len int32
function MessageQueueHandler:DispatchNetMessage(cmd,cacheBuff,len) end

return MessageQueueHandler
