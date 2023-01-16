using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ServerConfig))]
public class ServerConfigEditor : Editor
{
    private ServerConfig mTarget;
    private static string SERVER_CONFIG = "Assets/ServerConfig.asset";

    [MenuItem("Tools/配置/服务器配置")]
    public static void CreateAsset()
    {
        if (EditorUtility.DisplayDialog("是否生成", "是否要生成服务器配置？", "确认", "取消"))
        {
            CreateServerCfg();
        }
    }
    /// <summary>
    /// 生成服务器配置
    /// </summary>
    public static void CreateServerCfg()
    {
        ServerConfig asset = AssetDatabase.LoadAssetAtPath<ServerConfig>(SERVER_CONFIG);
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<ServerConfig>();
            AssetDatabase.CreateAsset(asset, SERVER_CONFIG);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        asset.Server_IP = "192.168.1.23";
        asset.Server_Port = 2000;
    }

    void Awake()
    {
        mTarget = target as ServerConfig;
    }
}
