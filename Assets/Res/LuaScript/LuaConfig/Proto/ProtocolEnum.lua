---@class ProtocolEnum
local ProtocolEnum = Class('ProtocolEnum')
ProtocolEnum.packageName = "protocol"
ProtocolEnum.MessageName = {}

ProtocolEnum.LOGIN_REQ = 1
ProtocolEnum.MessageName[1] = "LoginInfo"

ProtocolEnum.LOGIN_RSP = 2
ProtocolEnum.MessageName[2] = "UserInfo"

ProtocolEnum.PING_REQ = 19999
ProtocolEnum.MessageName[19999] = "PingReq"

ProtocolEnum.PING_RSP = 20000
ProtocolEnum.MessageName[20000] = "PingRsp"

return ProtocolEnum
