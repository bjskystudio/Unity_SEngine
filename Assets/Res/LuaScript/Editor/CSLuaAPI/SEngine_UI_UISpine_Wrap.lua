---===================== Author Qcbf 这是自动生成的代码 =====================

---@class SEngine.UI.UISpine : UnityEngine.MonoBehaviour
---@field public SkelAnim Spine.Unity.SkeletonGraphic
---@field public AnimState Spine.AnimationState
---@field public Skel Spine.Skeleton
---@field public completeCallback System.Action
local UISpine = {}

---@param trackEntry Spine.TrackEntry
---@param e Spine.Event
function UISpine:OnUserDefinedEvent(trackEntry,e) end

---@param trackEntry Spine.TrackEntry
function UISpine:OnSpineAnimationStart(trackEntry) end

---@param trackEntry Spine.TrackEntry
function UISpine:OnSpineAnimationInterrupt(trackEntry) end

---@param trackEntry Spine.TrackEntry
function UISpine:OnSpineAnimationEnd(trackEntry) end

---@param trackEntry Spine.TrackEntry
function UISpine:OnSpineAnimationDispose(trackEntry) end

---@param trackEntry Spine.TrackEntry
function UISpine:OnSpineAnimationComplete(trackEntry) end

---@param name string
---@param loop System.Boolean
---@param complete System.Action
function UISpine:PlayAnim(name,loop,complete) end

---@param setSetupPose System.Boolean
function UISpine:StopAnim(setSetupPose) end

---@param boneName string
---@param propName string
---@param propValue number
function UISpine:SetBonePropValue(boneName,propName,propValue) end

---@param skinName string
function UISpine:SetSkin(skinName) end

return UISpine
