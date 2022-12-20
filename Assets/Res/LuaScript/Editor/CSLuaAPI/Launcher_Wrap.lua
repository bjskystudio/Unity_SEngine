---===================== Author Qcbf 这是自动生成的代码 =====================

---@class Launcher : SEngine.MonoSingleton
---@field public UsedAssetBundle System.Boolean
---@field public UsedLuaAssetBundle System.Boolean
---@field public mLogLevel SEngine.LogLevel
---@field public LuaDebugEnable System.Boolean
---@field public TileMapEnable System.Boolean
local Launcher = {}

function Launcher:StartUp() end

---@return System.Boolean
function Launcher:IsEditor() end

function Launcher:Dispose() end

return Launcher
