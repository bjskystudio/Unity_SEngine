---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Spine.TrackEntry
---@field public TrackIndex int32
---@field public Animation Spine.Animation
---@field public Loop System.Boolean
---@field public Delay number
---@field public TrackTime number
---@field public TrackEnd number
---@field public AnimationStart number
---@field public AnimationEnd number
---@field public AnimationLast number
---@field public AnimationTime number
---@field public TimeScale number
---@field public Alpha number
---@field public EventThreshold number
---@field public AttachmentThreshold number
---@field public DrawOrderThreshold number
---@field public Next Spine.TrackEntry
---@field public IsComplete System.Boolean
---@field public MixTime number
---@field public MixDuration number
---@field public MixBlend Spine.MixBlend
---@field public MixingFrom Spine.TrackEntry
---@field public MixingTo Spine.TrackEntry
---@field public HoldPrevious System.Boolean
local TrackEntry = {}

function TrackEntry:Reset() end

function TrackEntry:ResetRotationDirections() end

---@return string
function TrackEntry:ToString() end

return TrackEntry
