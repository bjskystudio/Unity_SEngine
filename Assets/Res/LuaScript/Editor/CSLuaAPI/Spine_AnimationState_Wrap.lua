---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Spine.AnimationState
---@field public TimeScale number
---@field public Data Spine.AnimationStateData
---@field public Tracks Spine.ExposedList
local AnimationState = {}

---@param src Spine.AnimationState
function AnimationState:AssignEventSubscribersFrom(src) end

---@param src Spine.AnimationState
function AnimationState:AddEventSubscribersFrom(src) end

---@param delta number
function AnimationState:Update(delta) end

---@param skeleton Spine.Skeleton
---@return System.Boolean
function AnimationState:Apply(skeleton) end

---@param skeleton Spine.Skeleton
---@return System.Boolean
function AnimationState:ApplyEventTimelinesOnly(skeleton) end

function AnimationState:ClearTracks() end

---@param trackIndex int32
function AnimationState:ClearTrack(trackIndex) end

---@param trackIndex int32
---@param animationName string
---@param loop System.Boolean
---@return Spine.TrackEntry
function AnimationState:SetAnimation(trackIndex,animationName,loop) end

---@param trackIndex int32
---@param animation Spine.Animation
---@param loop System.Boolean
---@return Spine.TrackEntry
function AnimationState:SetAnimation(trackIndex,animation,loop) end

---@param trackIndex int32
---@param animationName string
---@param loop System.Boolean
---@param delay number
---@return Spine.TrackEntry
function AnimationState:AddAnimation(trackIndex,animationName,loop,delay) end

---@param trackIndex int32
---@param animation Spine.Animation
---@param loop System.Boolean
---@param delay number
---@return Spine.TrackEntry
function AnimationState:AddAnimation(trackIndex,animation,loop,delay) end

---@param trackIndex int32
---@param mixDuration number
---@return Spine.TrackEntry
function AnimationState:SetEmptyAnimation(trackIndex,mixDuration) end

---@param trackIndex int32
---@param mixDuration number
---@param delay number
---@return Spine.TrackEntry
function AnimationState:AddEmptyAnimation(trackIndex,mixDuration,delay) end

---@param mixDuration number
function AnimationState:SetEmptyAnimations(mixDuration) end

---@param trackIndex int32
---@return Spine.TrackEntry
function AnimationState:GetCurrent(trackIndex) end

function AnimationState:ClearListenerNotifications() end

---@return string
function AnimationState:ToString() end

return AnimationState
