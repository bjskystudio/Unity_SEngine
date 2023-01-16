using SEngine;
using UnityEngine;

[XLua.LuaCallCSharp]
public class Launcher : MonoSingleton<Launcher>
{
    [HeaderAttribute("======出包选======")]
    /// <summary>
    /// 是否使用加载AB方式
    /// </summary>
    [Label("是否使用AB")]
    public bool UsedAssetBundle = false;
    /// <summary>
    /// 是否加载Lua资源包
    /// </summary>
    [Label("是否使用LuaAB")]
    public bool UsedLuaAssetBundle = false;

    [HeaderAttribute("======内网用，出包别选======")]
    /// <summary>
    /// 打印级别
    /// </summary>
    [Label("打印级别")]
    public LogLevel mLogLevel = LogLevel.All;
    /// <summary>
    /// 开启emmylua调试
    /// </summary>
    [Label("开启emmylua调试")]
    public bool LuaDebugEnable = false;
    /// <summary>
    /// 不联网运行
    /// </summary>
    [Label("不联网运行")]
    public bool RunOffline = false;
    /// <summary>
    /// 开启TileMap寻路
    /// </summary>
    //[Label("开启TileMap寻路")]
    //public bool TileMapEnable = false;

    private ServerConfig mServerConfig;
    public ServerConfig ServerConf
    {
        get
        {
            if (mServerConfig == null)
            {
                ServerConfigRef configRef = GameObject.FindObjectOfType<ServerConfigRef>();
                if (configRef != null)
                {
                    mServerConfig = configRef.mConfig;
                }
            }

            if (mServerConfig == null)
            {
                Debug.LogError("!!!!!!!!!!!!!没再场景中找到ServerConfig脚本!!!!!!!!!!!!!!!!");
            }

            return mServerConfig;
        }
    }

    protected override void Init()
    {
        base.Init();
        Log.Init(new UnityLogger(), mLogLevel);
        string path = "test";
        Log.Debug("SDKUpdatePath>>>" + path + "\n\r"
            + "persistentDataPath>>>" + Application.persistentDataPath + "\n\r"
            + "temporaryCachePath>>>" + Application.temporaryCachePath + "\n\r"
            + "streamingAssetsPath>>>" + Application.streamingAssetsPath + "\n\r"
            + "dataPath>>>" + Application.dataPath);
        Loom.Initialize();
        StartUp();
    }

    public void StartUp()
    {
        EventManager.Instance.AddListener(EventDef.OnGameStartUp, OnGameStartUp);
        StartUpManager.Instance.Execute();
    }
    private void OnGameStartUp(object[] param)
    {
        EventManager.Instance.RemoveListener(EventDef.OnGameStartUp, OnGameStartUp);
        Log.Debug("Game Startup complete...");
    }

    /// <summary>
    /// 是否编辑器模式
    /// </summary>
    public bool IsEditor()
    {
#if UNITY_EDITOR
        return true;
#else
        return false;
#endif
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
