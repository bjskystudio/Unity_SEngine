using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ServerConfig))]
public class ServerConfigEditor : Editor
{
    private ServerConfig mTarget;
    private static string SERVER_CONFIG = "Assets/ServerConfig.asset";

    [MenuItem("Tools/����/����������")]
    public static void CreateAsset()
    {
        if (EditorUtility.DisplayDialog("�Ƿ�����", "�Ƿ�Ҫ���ɷ��������ã�", "ȷ��", "ȡ��"))
        {
            CreateServerCfg();
        }
    }
    /// <summary>
    /// ���ɷ���������
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
