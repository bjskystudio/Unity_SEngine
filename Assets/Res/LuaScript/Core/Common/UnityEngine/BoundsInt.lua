---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by asd.
--- DateTime: 2022/11/24 16:53
---

local Vector3 = Vector3
---@class BoundsInt
---@field position Vector3
---@field size Vector3
local BoundsInt = {
    position = Vector3.zero,
    size = Vector3.zero,
}
local _getter = {}
local unity_boundsInt = CS.UnityEngine.BoundsInt

BoundsInt.__index = function(t, k)
    local var = rawget(BoundsInt, k)
    if var ~= nil then
        return var
    end

    var = rawget(_getter, k)
    if var ~= nil then
        return var(t)
    end

    return rawget(unity_boundsInt, k)
end

BoundsInt.__call = function(t, pos, size)
    return setmetatable({ position = pos or Vector3.zero, size = size or Vector3.zero }, BoundsInt)
end

function BoundsInt.New(pos, size)
    return setmetatable({ position = pos or Vector3.zero, size = size or Vector3.zero }, BoundsInt)
end


BoundsInt.unity_boundsInt = CS.UnityEngine.BoundsInt
setmetatable(BoundsInt, BoundsInt)
return BoundsInt