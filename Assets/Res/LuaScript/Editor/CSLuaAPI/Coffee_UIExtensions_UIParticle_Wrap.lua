---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Coffee.UIExtensions.UIParticle : UnityEngine.UI.MaskableGraphic
---@field public raycastTarget System.Boolean
---@field public meshSharing Coffee.UIExtensions.UIParticle.MeshSharing
---@field public groupId int32
---@field public groupMaxId int32
---@field public absoluteMode System.Boolean
---@field public scale number
---@field public scale3D UnityEngine.Vector3
---@field public particles System.Collections.Generic.List
---@field public materials System.Collections.Generic.IEnumerable
---@field public materialForRendering UnityEngine.Material
---@field public isPaused System.Boolean
local UIParticle = {}

function UIParticle:Play() end

function UIParticle:Pause() end

function UIParticle:Resume() end

function UIParticle:Stop() end

function UIParticle:Clear() end

---@param instance UnityEngine.GameObject
function UIParticle:SetParticleSystemInstance(instance) end

---@param instance UnityEngine.GameObject
---@param destroyOldParticles System.Boolean
function UIParticle:SetParticleSystemInstance(instance,destroyOldParticles) end

---@param prefab UnityEngine.GameObject
function UIParticle:SetParticleSystemPrefab(prefab) end

function UIParticle:RefreshParticles() end

---@param particles System.Collections.Generic.List
function UIParticle:RefreshParticles(particles) end

return UIParticle
