---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Spine.Unity.SkeletonAnimation : Spine.Unity.SkeletonRenderer
---@field public AnimationState Spine.AnimationState
---@field public AnimationName string
---@field public state Spine.AnimationState
---@field public loop System.Boolean
---@field public timeScale number
local SkeletonAnimation = {}

function SkeletonAnimation:ClearState() end

---@param overwrite System.Boolean
---@param quiet System.Boolean
function SkeletonAnimation:Initialize(overwrite,quiet) end

---@param deltaTime number
function SkeletonAnimation:Update(deltaTime) end

function SkeletonAnimation:LateUpdate() end

---@param gameObject UnityEngine.GameObject
---@param skeletonDataAsset Spine.Unity.SkeletonDataAsset
---@param quiet System.Boolean
---@return Spine.Unity.SkeletonAnimation
function SkeletonAnimation.AddToGameObject(gameObject,skeletonDataAsset,quiet) end

---@param skeletonDataAsset Spine.Unity.SkeletonDataAsset
---@param quiet System.Boolean
---@return Spine.Unity.SkeletonAnimation
function SkeletonAnimation.NewSkeletonAnimationGameObject(skeletonDataAsset,quiet) end

return SkeletonAnimation
