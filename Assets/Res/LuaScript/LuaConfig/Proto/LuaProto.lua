---@type Protocol

local Protocol = {}

---@class PingReq
local PingReq = {}
PingReq.seq = {} --@ uint32
PingReq.client_time = {} --@ uint32

function Protocol.PingReq()
    return new(PingReq)
end


---@class PingRsp
local PingRsp = {}
PingRsp.seq = {} --@ uint32
PingRsp.client_time = {} --@ uint32
PingRsp.server_time = {} --@ uint32

function Protocol.PingRsp()
    return new(PingRsp)
end


---@class PlayerData
local PlayerData = {}
PlayerData.uid = {} --@ uint32
PlayerData.nickname = ""
PlayerData.level = {} --@ uint32

function Protocol.PlayerData()
    return new(PlayerData)
end


---@class LoginInfo
local LoginInfo = {}
LoginInfo.AccountType = 0
LoginInfo.Account = ""
LoginInfo.Token = ""
LoginInfo.UserID = 0
LoginInfo.ServerID = 0
LoginInfo.UserName = ""

function Protocol.LoginInfo()
    return new(LoginInfo)
end


---@class UserInfo
local UserInfo = {}
UserInfo.player_data = {} --@ PlayerData

function Protocol.UserInfo()
    return new(UserInfo)
end

return Protocol
