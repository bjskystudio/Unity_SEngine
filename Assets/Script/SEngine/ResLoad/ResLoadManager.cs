using SEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace SEngine
{
    public enum AssetType
    {
        eNone,
        eAB,
        ePrefab,
        eTexture,
        eAudioClip,
        eAnimationClip,
        eText,
        eShader,
        eAtlasSprite,
        eSprite,
        eManifest,
        eMaterial,
        eFont,
    }
    public enum ResLoadMode
    {
        eAssetDatabase,
        eAssetbundle,
    }
    public class ResLoadManager : MonoSingleton<ResLoadManager>
    {


        private ResLoadConfig mConfig;
        public Dictionary<string, SRes> mResMap = new Dictionary<string, SRes>(); //所有资源列表
        public Dictionary<SRes, string> mRecycleBinMap = new Dictionary<SRes, string>(); //回收站列表

        private List<SRes> mRecycleBinRemoveList = new List<SRes>(); //回收站待移除元素
        private List<SRes> mReleaseRequestList = new List<SRes>();
        private object mLocker = new object();
        public ResLoadMode LoadMode
        {
            get
            {
                if (mConfig != null)
                {
                    return mConfig.mResourceLoadMode;
                }
                else
                {
                    return ResLoadMode.eAssetDatabase;
                }
            }
        }
        public ResLoadConfig Config
        {
            get
            {
                if (mConfig != null)
                {
                    return mConfig;
                }
                else
                {
                    return null;
                }
               }
        }
        public override void Startup()
        {
            base.Startup();
        }
        protected override void Init()
        {
            base.Init();
            if (mConfig == null)
            {
                ResLoadConfigRef configRef = GameObject.FindObjectOfType<ResLoadConfigRef>();
                if (configRef != null)
                {
                    mConfig = configRef.mConfig;
                }
            }

            if (mConfig == null)
            {
                Log.Error("!!!!!!!!!!!!!没再场景中找到ResourceLoadConfig脚本!!!!!!!!!!!!!!!!");
                return;
            }
            {
                StartCoroutine(CoInit());
            }
        }

        IEnumerator CoInit()
        {
            if (LoadMode == ResLoadMode.eAssetbundle)
            {
                SResAssetBundle.InitManifest();
                yield return null;

            }
        }

        #region load资源类型

        /// <summary>
        /// 根据类型参数加载资源
        /// </summary>
        /// <param name="path">资源相对路径</param>
        /// <param name="assetName">资源名称</param>
        /// <param name="type">资源类型</param>
        /// <param name="isSync">是否同步加载</param>
        /// <param name="callback">加载回调</param>
        public void LoadObj(string path, AssetType type, Action<UnityEngine.Object, SResRef> callback = null, bool isSync = false)
        {
            string assetName = Path.GetFileName(path);
            switch (type)
            {
                case AssetType.ePrefab:
                    Load<GameObject, SResPrefab>(path, assetName, AssetType.ePrefab, callback, isSync);
                    break;
                case AssetType.eAtlasSprite:
                    if (LoadMode == ResLoadMode.eAssetbundle)
                    {
                        path = Path.GetDirectoryName(path).Replace("\\", "/");
                    }
                    Load<Sprite, SResSprite>(path, assetName, AssetType.eSprite, callback, isSync, false);
                    break;
                case AssetType.eSprite:
                    Load<Sprite, SResSprite>(path, assetName, AssetType.eSprite, callback, isSync);
                    break;
                case AssetType.eTexture:
                    Load<Texture, SResTexture>(path, assetName, AssetType.eTexture, callback, isSync);
                    break;
                case AssetType.eAudioClip:
                    Load<AudioClip, SResAudioCilp>(path, assetName, AssetType.eAudioClip, callback, isSync);
                    break;
                case AssetType.eAnimationClip:
                    Load<AnimationClip, SResAnimationClip>(path, assetName, AssetType.eAnimationClip, callback, isSync);
                    break;
                case AssetType.eMaterial:
                    Load<Material, SResMaterial>(path, assetName, AssetType.eMaterial, callback, isSync);
                    break;
                case AssetType.eText:
                    Load<TextAsset, SResText>(path, assetName, AssetType.eText, callback, isSync);
                    break;
                case AssetType.eFont:
                    Load<Font, SResFont>(path, assetName, AssetType.eFont, callback, isSync);
                    break;
                default:
                    Log.Warning( $"{type}没有对应的资源加载方法");
                    break;
            }
        }
        private string GetResName(string assetPath, string assetName, AssetType assetType, bool isAll)
        {
            assetPath = assetPath.ToLower();
            string resName = string.IsNullOrEmpty(assetName) ? string.Format("{0}", assetPath) : string.Format("{0}/{1}({2})", assetPath, assetName, assetType);
            return resName.ToLower();
        }

        internal T GetSRes<T>(string assetPath, string assetName, AssetType assetType, bool isAll = false, bool isDep = false) where T : SRes, new()
        {
            string resName = GetResName(assetPath, assetName, assetType, isAll);
            SRes res = null;
            if (!mResMap.TryGetValue(resName, out res))
            {
                res = new T();
                mResMap.Add(resName, res);
                res.Init(assetPath, assetName, resName, assetType, isAll);
            }

            res.RefCount++;
            RemoveRecycleBin(res);
            return res as T;
        }

        private void Load<T1, T2>(string assetPath, string assetName, AssetType assetType, Action<T1, SResRef> callback, bool isSync = false, bool isAll = false, bool isPreload = false) where T1 : UnityEngine.Object where T2 : SRes, new()
        {
            if (string.IsNullOrEmpty(assetPath))
            {
                Debug.LogError("assetPath is null!!!");
                if (callback != null)
                {
                    callback(null, null);
                }
                return;
            }

            Action<System.Object, SResRef> tCallBack = null;
            if (callback != null)
            {
                tCallBack = (asset, resRef) =>
                {
                    callback(asset as T1, resRef);
                };
            }

            T2 res = GetSRes<T2>(assetPath, assetName, assetType, isAll);
            res.StartLoad(isSync, isAll, isPreload, tCallBack);
        }
        #endregion

        internal void AddRecycleBin(SRes res)
        {
            if (!mRecycleBinMap.ContainsKey(res))
            {
                Debug.Log("res recycle bin : " + res.ResName + "/" + Time.realtimeSinceStartup);
                res.RecycleBinPutInTime = Time.realtimeSinceStartup;
                mRecycleBinMap.Add(res, res.ResName);
            }
        }

        internal void RemoveRecycleBin(SRes res)
        {
            if (mRecycleBinMap.ContainsKey(res))
            {
                res.RecycleBinPutInTime = -1;
                mRecycleBinMap.Remove(res);
            }
        }
        public void Update()
        {
            foreach (var item in mRecycleBinMap)
            {
                SRes res = item.Key;
                float time = Time.realtimeSinceStartup - res.RecycleBinPutInTime;
                if (time > mConfig.RECYBLEBIN_RES_DESTROY_TIME)
                {
                    //真正销毁
                    if (res.RefCount == 0)
                    {
                        res.ReleaseReal();
                        mRecycleBinRemoveList.Add(res);
                    }
                }
            }

            for (int i = 0; i < mRecycleBinRemoveList.Count; i++)
            {
                mRecycleBinMap.Remove(mRecycleBinRemoveList[i]);
            }

            mRecycleBinRemoveList.Clear();

            lock (mLocker)
            {
                if (mReleaseRequestList.Count > 0)
                {
                    for (int i = 0; i < mReleaseRequestList.Count; i++)
                    {
                        mReleaseRequestList[i].Release();
                    }
                    mReleaseRequestList.Clear();
                }
            }
        }

        public void AddReleaseRequest(SRes sRes)
        {
            lock (mLocker)
            {
                mReleaseRequestList.Add(sRes);
            }
        }
        public override void Dispose()
        {

        }
    }
}
