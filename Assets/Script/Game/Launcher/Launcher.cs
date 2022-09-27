using Game;
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


    protected override void Init()
    {
        base.Init();
        Log.Init(new UnityLogger());
        InitResLoadCfg();
        string path = "test";
        Log.Debug("SDKUpdatePath>>>" + path + "\n\r"
            + "persistentDataPath>>>" + Application.persistentDataPath + "\n\r"
            + "temporaryCachePath>>>" + Application.temporaryCachePath + "\n\r"
            + "streamingAssetsPath>>>" + Application.streamingAssetsPath + "\n\r"
            + "dataPath>>>" + Application.dataPath);

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

    private void InitResLoadCfg()
    {
        //ResourceLoadConfigRef configRef = gameObject.GetComponent<ResourceLoadConfigRef>();
        //if (configRef == null || configRef.mConfig == null)
        //{
        //    Debug.LogError("资源加载配置缺失");
        //    return;
        //}
        //configRef.mConfig.mResourceLoadMode = UsedAssetBundle ? ResourceLoadMode.eAssetbundle : ResourceLoadMode.eAssetDatabase;
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
