using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public class ResLoadConfig : ScriptableObject
    {
        public ResLoadMode mResourceLoadMode = ResLoadMode.eAssetDatabase;
        public bool DEBUG_MODE = false;

        public float RECYBLEBIN_RES_DESTROY_TIME = 10;  //10秒回收一次

        public string MANIFEST_NAME = "Assetbundle";
        public string ASSETBUNDLE_SUFFIX_NAME = "";

        public string RES_LOCAL_ASSETDATABASE_RELATIVE_PATH = "Assets/Export";
        public string RES_LOCAL_AB_RELATIVE_PATH = "../ClientRes/Android/Assetbundle";
        public string RES_STREAM_AB_RELATIVE_PATH = "ClientRes/Android/Assetbundle";
        public string RES_PERSISTENT_RELATIVE_PATH = "ClientRes/Android/Assetbundle";

        public string RES_PERSISTENT_AB_RELATIVE_PATH = "Assetbundle";
    }

}