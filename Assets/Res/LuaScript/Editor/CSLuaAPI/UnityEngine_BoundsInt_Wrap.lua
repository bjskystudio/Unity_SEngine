---===================== Author Qcbf 这是自动生成的代码 =====================

---@class UnityEngine.BoundsInt
---@field public x int32
---@field public y int32
---@field public z int32
---@field public center UnityEngine.Vector3
---@field public min UnityEngine.Vector3Int
---@field public max UnityEngine.Vector3Int
---@field public xMin int32
---@field public yMin int32
---@field public zMin int32
---@field public xMax int32
---@field public yMax int32
---@field public zMax int32
---@field public position UnityEngine.Vector3Int
---@field public size UnityEngine.Vector3Int
---@field public allPositionsWithin UnityEngine.BoundsInt.PositionEnumerator
local BoundsInt = {}

---@param minPosition UnityEngine.Vector3Int
---@param maxPosition UnityEngine.Vector3Int
function BoundsInt:SetMinMax(minPosition,maxPosition) end

---@param bounds UnityEngine.BoundsInt
function BoundsInt:ClampToBounds(bounds) end

---@param position UnityEngine.Vector3Int
---@return System.Boolean
function BoundsInt:Contains(position) end

---@return string
function BoundsInt:ToString() end

---@param format string
---@return string
function BoundsInt:ToString(format) end

---@param format string
---@param formatProvider System.IFormatProvider
---@return string
function BoundsInt:ToString(format,formatProvider) end

---@param other System.Object
---@return System.Boolean
function BoundsInt:Equals(other) end

---@param other UnityEngine.BoundsInt
---@return System.Boolean
function BoundsInt:Equals(other) end

---@return int32
function BoundsInt:GetHashCode() end

return BoundsInt
