using Game.Network;
using SEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using XLua;

[Hotfix]
[LuaCallCSharp]
[CSharpCallLua]
public class XLuaManager : MonoSingleton<XLuaManager>
{
    /// <summary>
    /// Lua脚本相对目录路径
    /// </summary>
    private const string luaScriptsRelativeFolderPath = "/Res/LuaScript";

    /// <summary>
    /// Lua PB相对目录路径
    /// </summary>
    private const string luaPBRelativeFolderPath = "/Res/PBData";

    /// <summary>
    /// Lua启动脚本名
    /// </summary>
    private const string luaLauncherScriptName = "LuaLauncher";

    /// <summary>
    /// Lua Env
    /// </summary>
    private LuaEnv luaEnv = null;

    /// <summary>
    /// Lua更新脚本
    /// </summary>
    private LuaUpdater luaUpdater = null;

    /// <summary>
    /// Lua脚本二进制缓存
    /// </summary>
    public Dictionary<string, byte[]> LuaScriptsBytesCaching
    {
        get;
        private set;
    }

    /// <summary>
    /// Lua OB二进制缓存
    /// </summary>
    public Dictionary<string, byte[]> LuaPBBytesCaching
    {
        get;
        private set;
    }

    /// <summary>
    /// 游戏是否开始
    /// </summary>
    public bool HasGameStart
    {
        get;
        protected set;
    }

    /// <summary>
    /// Lua脚本重新载入回调绑定
    /// </summary>
    public static Action<string> LuaReimport;
    /// <summary>
    /// 重载指定脚本
    /// </summary>
    /// <param name="scriptName"></param>
    private void ReimportScript(string scriptName)
    {
        LuaReimport?.Invoke(scriptName);
    }


    /// <summary>
    /// 加载Lua脚本资源
    /// </summary>
    /// <param name="loadcompletedcb">加载完成回调</param>
    public void LoadLuaScriptsRes(Action loadcompletedcb = null)
    {
        LuaScriptsBytesCaching.Clear();
        if (Launcher.Instance.UsedAssetBundle && Launcher.Instance.UsedLuaAssetBundle)
        {
            ResLoadManager.Instance.LoadABTextAll("luascriptsbyte/luascriptsbyte_bundle", (list, res) =>
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Debug.Log("添加AB脚本：" + list[i].name);
                    LuaScriptsBytesCaching.Add(list[i].name, list[i].bytes);
                }
                Debug.Log("添加AB脚本完成");
                res.Release();
                loadcompletedcb?.Invoke();
            }, false);
        }
        else
        {
            string luascriptfolderfullpath = Application.dataPath + luaScriptsRelativeFolderPath;
            string[] files = Directory.GetFiles(luascriptfolderfullpath, "*.lua", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string luaname = Path.GetFileNameWithoutExtension(files[i]);
                var bytes = File.ReadAllBytes(files[i]);
                if (!LuaScriptsBytesCaching.ContainsKey(luaname))
                {
                    LuaScriptsBytesCaching.Add(luaname, bytes);
                }
                else
                {
                    Debug.LogError($"有重名的Lua脚本:{luaname}!");
                }
            }
            loadcompletedcb?.Invoke();
        }
    }

    /// <summary>
    /// 加载Lua PB资源
    /// </summary>
    /// <param name="loadcompletedcb">加载完成回调</param>
    public void LoadLuaPBRes(Action loadcompletedcb = null)
    {
        LuaPBBytesCaching.Clear();
        if (Launcher.Instance.UsedAssetBundle && Launcher.Instance.UsedLuaAssetBundle)
        {
            ResLoadManager.Instance.LoadABTextAll("pbdata/pbdata_bundle", (list, res) =>
            {
                for (int i = 0; i < list.Count; i++)
                {
                    LuaPBBytesCaching.Add(list[i].name, list[i].bytes);
                }
                res.Release();
                loadcompletedcb?.Invoke();
            }, false);
        }
        else
        {
            string luapbfolderfullpath = Application.dataPath + luaPBRelativeFolderPath;
            string[] files = Directory.GetFiles(luapbfolderfullpath, "*.bytes", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                string pbname = Path.GetFileNameWithoutExtension(files[i]);
                var bytes = File.ReadAllBytes(files[i]);
                if (!LuaPBBytesCaching.ContainsKey(pbname))
                {
                    LuaPBBytesCaching.Add(pbname, bytes);
                }
                else
                {
                    Debug.LogError($"有重名的PB文件:{pbname}!");
                }
            }
            loadcompletedcb?.Invoke();
        }
    }

    protected override void Init()
    {
        LuaScriptsBytesCaching = new Dictionary<string, byte[]>();
        LuaPBBytesCaching = new Dictionary<string, byte[]>();
    }

    /// <summary>
    /// 初始化Lua环境
    /// </summary>
    public void InitLuaEnv()
    {
        if (luaEnv != null)
        {
            Debug.LogError($"重复初始化Lua环境!");
            return;
        }
        Debug.Log($"初始化Lua环境!");
        luaEnv = new LuaEnv();
        luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadLuaProfobuf);
        HasGameStart = false;
        if (luaEnv != null)
        {
            luaEnv.AddLoader(CustomLoader);
        }
        else
        {
            Debug.LogError("InitLuaEnv null!!!");
        }

        //加载LuaLauncher.lua
        LoadScript(luaLauncherScriptName);
        StartEmmyLuaDebugger();
        StartGame();

        luaUpdater = gameObject.GetComponent<LuaUpdater>();
        if (luaUpdater == null)
        {
            luaUpdater = gameObject.AddComponent<LuaUpdater>();
        }
        luaUpdater.OnInit(luaEnv);
    }

    private LuaTable NetMessageScript = null;
    public void DispatchNetMessage(int cmd, byte[] cacheBuff, int len)
    {
        if (null == luaEnv
        )
        {
            return;
        }
        if(NetMessageScript == null)
        {
            string luaScript = @"
            local NetManager = require(""NetManager"")

-- - Net静态工具
local NetUtil = { }


function NetUtil.OnRecvMessage(iCmd, bytes)
    NetManager: GetInstance():OnRecvMessage(iCmd, bytes)
end

return NetUtil";

            byte[] bytes = System.Text.Encoding.Default.GetBytes(luaScript);
            NetMessageScript = ExecuteScript(bytes) as LuaTable;
        }
        if (null == NetMessageScript)
        {
            return;
        }

        var retFunc = NetMessageScript.Get<LuaFunction>("OnRecvMessage");

        retFunc?.CallSpecial(cmd, cacheBuff, len);

    }

    /// <summary>
    /// 获取当前Lua Env
    /// </summary>
    /// <returns></returns>
    public LuaEnv GetLuaEnv()
    {
        return luaEnv;
    }
    private void StartEmmyLuaDebugger()
    {
        if (Launcher.Instance.LuaDebugEnable)
        {
            Debug.Log("Start EmmyLua");
            string debugDll = UnityEngine.Application.dataPath + "/../EmmyLuaDebugger/windows/x64/?.dll";
            byte[] byteArray = System.Text.Encoding.Default.GetBytes($"package.cpath = package.cpath .. ';{debugDll}'\nlocal dbg = require('emmy_core')\ndbg.tcpConnect('localhost', 9966)");
            ExecuteScript(byteArray);
        }
    }

    /// <summary>
    /// 游戏开始
    /// </summary>
    private void StartGame()
    {
        if (luaEnv != null)
        {
            SafeDoString(luaLauncherScriptName + ":Start()");
            HasGameStart = true;
        }
    }

    /// <summary>
    /// 游戏Stop
    /// </summary>
    private void StopGame()
    {
        if (luaEnv != null && HasGameStart)
        {
            SafeDoString(luaLauncherScriptName + ":Stop()");
        }
    }

    /// <summary>
    /// 加载指定脚本
    /// </summary>
    /// <param name="scriptName"></param>
    public void LoadScript(string scriptName)
    {
        SafeDoString(string.Format("require('{0}')", scriptName));
    }

    /// <summary>
    /// 自定义Lua脚本加载回调
    /// </summary>
    /// <param name="filepath"></param>
    /// <returns></returns>
    private byte[] CustomLoader(ref string filepath)
    {
        string scriptPath = string.Empty;

        filepath = filepath.Replace(".", "/");
        var luaNames = filepath.Split('/');
        var luaName = luaNames[luaNames.Length - 1];
        scriptPath = filepath;
        byte[] bytes = null;
        LuaScriptsBytesCaching.TryGetValue(luaName, out bytes);
        if (bytes == null)
        {
            if (scriptPath == "emmy_core")
            {
                return null;
            }
            Debug.LogError("找不到脚本：" + scriptPath);
        }
        return bytes;
    }

    private void Update()
    {
        if (luaEnv != null)
        {
            luaEnv.Tick();
        }
    }

    /// <summary>
    /// 停止虚拟机
    /// </summary>
    public void StopLuaEnv()
    {
        DeleteDelegate();
    }

    /// <summary>
    /// 重启虚拟机
    /// </summary>
    public void Restart()
    {
        StopGame();
        StopLuaEnv();
        LuaScriptsBytesCaching.Clear();
        LuaPBBytesCaching.Clear();
        string script = @"
            local load = {[""math""] = true,[""io""] = true,[""coroutine""] = true,[""debug""] = true,[""utf8""] = true,[""package""] = true,[""string""] = true,[""os""] = true,[""_G""] = true,[""table""] = true,}
            for key,val in pairs(package.loaded) do
                if load[key] ~= true  then
                    package.loaded[key] = nil;
                end
            end
            local t = {[""realRequire""] = true,[""rawlen""] = true,[""setfenv""] = true,[""tostring""] = true,[""table""] = true,[""typeof""] = true,[""type""] = true,[""assert""] = true,[""getmetatable""] = true,[""tonumber""] = true,[""xlua""] = true,[""print""] = true,[""require""] = true,[""rawset""] = true,[""next""] = true,[""package""] = true,[""collectgarbage""] = true,[""_VERSION""] = true,[""select""] = true,[""utf8""] = true,[""_G""] = true,[""xpcall""] = true,[""error""] = true,[""debug""] = true,[""dofile""] = true,[""rawequal""] = true,[""string""] = true,[""cast""] = true,[""pairs""] = true,[""io""] = true,[""template""] = true,[""uint64""] = true,[""CS""] = true,[""getfenv""] = true,[""ipairs""] = true,[""rawget""] = true,[""os""] = true,[""setmetatable""] = true,[""loadfile""] = true,[""math""] = true,[""pcall""] = true,[""load""] = true,[""base""] = true,[""coroutine""] = true,}
            for key, val in pairs(_G) do
                if t[key] ~= true  then
                    _G[key] = nil
                end
            end
            collectgarbage(""collect"")";
        SafeDoString(script);
    }

    public override void Dispose()
    {
        LuaReimport = null;
        //删除委托
        DeleteDelegate();
#if !UNITY_EDITOR
        // 关闭虚拟机
        if (luaEnv != null)
        {
            try
            {
                luaEnv.Dispose();
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("xLua exception : {0}\n {1}", ex.Message, ex.StackTrace);
                Debug.LogError(msg, null);
            }
            finally
            {
                luaEnv = null;
            }
        }
#endif
    }

    /// <summary>
    ///  删除委托
    /// </summary>
    public void DeleteDelegate()
    {

    }

    public void SafeDoString(string scriptContent)
    {
        if (luaEnv != null)
        {
            try
            {
                luaEnv.DoString(scriptContent);
            }
            catch (System.Exception ex)
            {
                string msg = string.Format("xLua exception : {2} >> {0}\n {1}", ex.Message, ex.StackTrace, scriptContent);
                Debug.LogError(msg, null);
            }
        }
    }
    
    
    /// <summary>
    /// Execute lua script directly!
    /// </summary>
    /// <param name="scriptCode"></param>
    /// <returns></returns>
    public object ExecuteScript(byte[] scriptCode, string file = "chunk")
    {
        object ret;
        ExecuteScript(scriptCode, out ret, file);
        return ret;
    }
    
    /// <summary>
    /// Execute lua script directly!
    /// </summary>
    /// <param name="scriptCode"></param>
    /// <param name="ret">return result</param>
    /// <returns></returns>
    public bool ExecuteScript(byte[] scriptCode, out object ret, string file = "chunk")
    {
        if (luaEnv!= null)
        {
            var results = luaEnv.DoString(Encoding.UTF8.GetString(scriptCode), file);

            if (results != null && results.Length == 1)
            {
                ret = results[0];
            }
            else
            {
                ret = results;
            }

            return true;
        }
        else
        {
            ret = null;
            return false;
        }
    }
    
    public LuaTable CreateNewTable()
    {
        return luaEnv != null ? luaEnv.NewTable() : null;
    }
}
