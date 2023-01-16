---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Game.Network.NetworkKit
---@field static DefaultSocketChannel Game.Network.ISocketChannel
---@field static IsIOSMJ System.Boolean
local NetworkKit = {}

function NetworkKit.Init() end

function NetworkKit.UnInit() end

---@param ip string
---@param port int32
---@param eventCollect System.Action
function NetworkKit.Connect(ip,port,eventCollect) end

---@param url string
---@param postData string
---@param callback System.Action
function NetworkKit.SendServerHttpRequest(url,postData,callback) end

function NetworkKit.DisConnect() end

---@return System.Boolean
function NetworkKit.IsConnected() end

---@param cmd Game.Network.ProtocolEnum
---@param msg Google.Protobuf.IMessage
function NetworkKit.Send(cmd,msg) end

---@param cmd int16
---@param bytes System.Byte[]
function NetworkKit.SendLua(cmd,bytes) end

---@param cmd int32
---@param msg Google.Protobuf.IMessage
function NetworkKit.HandleCSharpMessage(cmd,msg) end

function NetworkKit.TestMessage() end

return NetworkKit
