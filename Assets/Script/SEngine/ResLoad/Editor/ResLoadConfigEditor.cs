using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace SEngine
{
    [CustomEditor(typeof(ResLoadConfig))]
    public class ResLoadEditor : Editor
    {
        private ResLoadConfig mTarget;
        private static string RESOURCE_LOAD_CONFIG = "Assets/ResourceLoadConfig.asset";

        [MenuItem("Tools/配置/资源加载配置")]
        public static void CreateAsset()
        {
            if (EditorUtility.DisplayDialog("是否生成", "是否要生成资源配置？", "确认", "取消"))
            {
                CreateResLoadCfg();
            }
        }
        /// <summary>
        /// 生成资源加载配置
        /// </summary>
        public static void CreateResLoadCfg()
        {
            ResLoadConfig asset = AssetDatabase.LoadAssetAtPath<ResLoadConfig>(RESOURCE_LOAD_CONFIG);
            if (asset == null)
            {
                asset = ScriptableObject.CreateInstance<ResLoadConfig>();
                asset.mResourceLoadMode = ResLoadMode.eAssetDatabase;
                AssetDatabase.CreateAsset(asset, RESOURCE_LOAD_CONFIG);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            //AD
            asset.DEBUG_MODE = true;
            asset.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH = "Assets/Res";
            //AB
            asset.ASSETBUNDLE_SUFFIX_NAME = ".assetbundle";
            asset.MANIFEST_NAME = "StreamingResources";
            asset.SHADER_AB_RELATIVE_PATH = "shader/allshader";
            asset.RES_LOCAL_AB_RELATIVE_PATH = "StreamingAssets/StreamingResources";
            asset.RES_STREAM_AB_RELATIVE_PATH = "StreamingResources";
            asset.RES_PERSISTENT_RELATIVE_PATH = "GameRes";
            asset.RECYBLEBIN_RES_DESTROY_TIME = 3;
        }

        void Awake()
        {
            mTarget = target as ResLoadConfig;
        }
    }
}

