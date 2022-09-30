---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by Simon.L.
--- DateTime: 2022/9/30 15:15
---

local os = os
local os_date = os.date
local os_time = os.time
local Now = CS.System.DateTime.Now

---@class TimeUtil
local TimeUtil = {}

local HOUR_OF_DAY = 24
local SECOND_OF_MINUTE = 60
local SECOND_OF_HOUR = 3600
local SECOND_OF_DAY = HOUR_OF_DAY * SECOND_OF_HOUR
local WEEK_OF_DAY = 7
---时间差(秒)
local TimeSecOffset = 0

local function LocalTimeSec()
    return os_time()
end

local function LocalTimeMsec()
    return LocalTimeSec() * 1000 + (Now.Ticks / 10000) % 1000
end


---获取当前时间戳(秒)
function TimeUtil.GetSecTime()
    -- return _currServerTime
    return LocalTimeSec() - TimeSecOffset
end

---计算出服务器时间的时区
---@return number @时区
function TimeUtil.GetTimeZone()
    local time = TimeUtil.GetSecTime()
    local a = os_date('!*t', time) -- 中时区的时间
    local b = os_date('*t', time)
    local timeZone = (b.hour - a.hour) * 3600 + (b.min - a.min) * 60
    timeZone = timeZone / 3600
    return timeZone
end
-- endregion
---当前是一天的第几秒
function TimeUtil.GetSecondsOfDay(now)
    local now = now or TimeUtil.GetSecTime()
    local zero = TimeUtil.GetDayBegin(now)
    return now - zero
end
---往前追朔到某一天的零点, 单位秒数
---@param time number 时间戳 单位秒
---@param useTimeZone boolean 是否计算时区，默认不计算
function TimeUtil.GetDayBegin(time, useTimeZone)
    local timetable = os_date("*t", time)
    timetable.hour = 0
    timetable.min = 0
    timetable.sec = 0
    if (useTimeZone) then
        local tTimeZone = TimeUtil.GetTimeZone()
        timetable.hour = timetable.hour + tTimeZone
    end
    return os_time(timetable)
end

---将秒数转换为：00:00:00
---@param time number 时间戳秒数
---@param flag number nil为 时:分:秒 2为 时:分
---@param useTimeZone boolean 是否使用时区 默认不使用
---@return string
function TimeUtil.TimeToString(time, flag, useTimeZone)
    -- function TimeUtil.TimeToString(time, flag)
    local sec = time % SECOND_OF_MINUTE
    local min = math.floor(time / 60) % 60
    local hour = math.floor(time / SECOND_OF_HOUR)
    if (useTimeZone) then
        hour = hour + TimeUtil.GetTimeZone()
    end
    if flag == 2 then
        return string.format("%02d:%02d", hour, min)
        -- elseif flag == 1 then
    else
        return string.format("%02d:%02d:%02d", hour, min, sec)
    end
end

---将时间戳的秒数转换为今日的：00:00:00
---@param time number 时间戳秒数
---@param flag number nil为 时:分:秒 2为 时:分
---@param useTimeZone boolean 是否使用时区 默认不使用
---@return string
function TimeUtil.TimeStampToString(time, flag, useTimeZone)
    time = time % SECOND_OF_DAY
    -- function TimeUtil.TimeToString(time, flag)
    local sec = time % SECOND_OF_MINUTE
    local min = math.floor(time / 60) % 60
    local hour = math.floor(time / SECOND_OF_HOUR)
    if (useTimeZone) then
        hour = hour + TimeUtil.GetTimeZone()
    end
    if flag == 2 then
        return string.format("%02d:%02d", hour, min)
    else
        -- elseif flag == 1 then
        return string.format("%02d:%02d:%02d", hour, min, sec)
    end
end

--- 每天的小时数
TimeUtil.HOUR_OF_DAY = HOUR_OF_DAY
--- 每分钟的描述
TimeUtil.SECOND_OF_MINUTE = SECOND_OF_MINUTE
--- 每小时的描述
TimeUtil.SECOND_OF_HOUR = SECOND_OF_HOUR
--- 每天的秒数
TimeUtil.SECOND_OF_DAY = SECOND_OF_DAY
--- 每周的天数
TimeUtil.WEEK_OF_DAY = WEEK_OF_DAY

---@return TimeUtil TimeUtil
return TimeUtil