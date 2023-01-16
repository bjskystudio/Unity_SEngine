---===================== Author Qcbf 这是自动生成的代码 =====================

---@class XLuaManager : SEngine.MonoSingleton
---@field public LuaScriptsBytesCaching System.Collections.Generic.Dictionary
---@field public LuaPBBytesCaching System.Collections.Generic.Dictionary
---@field public HasGameStart System.Boolean
---@field static LuaReimport System.Action
local XLuaManager = {}

---@param loadcompletedcb System.Action
function XLuaManager:LoadLuaScriptsRes(loadcompletedcb) end

---@param loadcompletedcb System.Action
function XLuaManager:LoadLuaPBRes(loadcompletedcb) end

function XLuaManager:InitLuaEnv() end

---@param cmd int32
---@param cacheBuff System.Byte[]
---@param len int32
function XLuaManager:DispatchNetMessage(cmd,cacheBuff,len) end

---@return XLua.LuaEnv
function XLuaManager:GetLuaEnv() end

---@param scriptName string
function XLuaManager:LoadScript(scriptName) end

function XLuaManager:StopLuaEnv() end

function XLuaManager:Restart() end

function XLuaManager:Dispose() end

function XLuaManager:DeleteDelegate() end

---@param scriptContent string
function XLuaManager:SafeDoString(scriptContent) end

---@param scriptCode System.Byte[]
---@param file string
---@return System.Object
function XLuaManager:ExecuteScript(scriptCode,file) end

---@param scriptCode System.Byte[]
---@param file string
---@return System.Boolean
function XLuaManager:ExecuteScript(scriptCode,file) end

---@return XLua.LuaTable
function XLuaManager:CreateNewTable() end

return XLuaManager
