using SEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class ResLoadManager
    {

        private static ResLoadManager mInstance;
        public static ResLoadManager Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new ResLoadManager();
                }

                mInstance.CheckUpdate();
                return mInstance;
            }
        }

        private static ResLoadUpdater mUpdater;
        private static ResLoadConfig mConfig;
        private SResRef mShaderResRef;
        private Dictionary<string, Shader> mShaderMap = new Dictionary<string, Shader>(); //����shader�б�
        public Dictionary<string, SRes> mResMap = new Dictionary<string, SRes>(); //������Դ�б�
        public Dictionary<SRes, string> mRecycleBinMap = new Dictionary<SRes, string>(); //����վ�б�

        private List<SRes> mRecycleBinRemoveList = new List<SRes>(); //����վ���Ƴ�Ԫ��
        private List<SRes> mReleaseRequestList = new List<SRes>();
        private object mLocker = new object();
        public ResLoadMode LoadMode
        {
            get
            {
                if (Config != null)
                {
                    return Config.mResourceLoadMode;
                }
                else
                {
                    return ResLoadMode.eAssetDatabase;
                }
            }
        }
        public static ResLoadConfig Config
        {
            get
            {
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
                    Debug.LogError("!!!!!!!!!!!!!û�ٳ������ҵ�ResourceLoadConfig�ű�!!!!!!!!!!!!!!!!");
                }

                return mConfig;
            }
        }
        public void Init(Action completeAction)
        {
            StartCoroutine(CoInit(completeAction));
        }

        internal void StartCoroutine(IEnumerator routine)
        {
            if (mUpdater != null)
            {
                mUpdater.StartCoroutine(routine);
            }
        }
        IEnumerator CoInit(Action completeAction)
        {
            if (LoadMode == ResLoadMode.eAssetbundle)
            {
                SResAssetBundle.InitManifest();
                yield return null;// CoInitShader();

            }
            completeAction?.Invoke();
        }
        IEnumerator CoInitShader()
        {
            AsyncRequest request = LoadShaderAllCoRequest(Config.SHADER_AB_RELATIVE_PATH);
            yield return request;
            mShaderResRef = request.ResRef;
            if (request.Assets != null)
            {
                for (int i = 0; i < request.Assets.Count; i++)
                {
                    if (request.Assets[i] != null)
                    {
                        if (!mShaderMap.ContainsKey(request.Assets[i].name))
                        {
                            mShaderMap[request.Assets[i].name] = request.Assets[i] as Shader;
                        }
                        else
                        {
                            Debug.LogError(string.Format("{0}����ͬ���ͬ��{1}", Config.SHADER_AB_RELATIVE_PATH, request.Assets[i].name));
                        }
                    }
                }
            }
        }


        private void CheckUpdate()
        {
            if (!mUpdater)
            {
                mUpdater = GameObject.FindObjectOfType<ResLoadUpdater>();
                if (!mUpdater)
                {
                    mUpdater = new GameObject("ResLoadUpdater").AddComponent<ResLoadUpdater>();
                    mUpdater.gameObject.hideFlags = HideFlags.DontSave;
                }
            }
        }

        #region Load Res��Դ

        /// <summary>
        /// �ص���ʽ������AB�е�����Text(AssetDatabaseģʽ�޷�ʹ��)
        /// �ͷŷ�ʽ�����ûص��ڶ������ض���Release()ȥ�ͷ�
        /// </summary>
        /// <param name="path">·����</param>
        /// <param name="callback">�ص�</param>
        /// <param name="isSync">�Ƿ�ͬ��</param>
        public void LoadABTextAll(string path, Action<List<TextAsset>, SResRef> callback, bool isSync = false)
        {
            LoadAll<TextAsset, SResText>(path, AssetType.eText, callback, isSync);
        }
        /// �ͷŷ�ʽ������AsyncRequest�е�HRes�����Release()ȥ�ͷ�
        private AsyncRequest LoadShaderAllCoRequest(string path)
        {
            return LoadAllCoRequest<Shader, SResShader>(path, AssetType.eShader);
        }
        private AsyncRequest LoadAllCoRequest<T1, T2>(string path, AssetType assetType, bool isPreload = false) where T1 : UnityEngine.Object where T2 : SRes, new()
        {
            AsyncRequest request = new AsyncRequest();
            LoadAll<T1, T2>(path, assetType, (obj, resRef) =>
            {
                request.isDone = true;
                request.progress = 1;
                if (obj != null)
                {
                    request.Assets = obj.ConvertAll((item) => { return item as UnityEngine.Object; });
                    request.ResRef = resRef;
                }
            }, false, isPreload);

            return request;
        }
        /// <summary>
        /// ��ȡshader,�ⲿͳһ������
        /// </summary>
        /// <param name="shaderName">shader����</param>
        /// <returns></returns>
        public Shader GetShader(string shaderName)
        {
            if (LoadMode != ResLoadMode.eAssetbundle || Application.isEditor)
            {
                Shader shader = Shader.Find(shaderName);
                if (shader == null)
                {
                    Debug.LogError(string.Format("������ {0}", shaderName));
                }

                return shader;
            }
            else
            {
                if (mShaderMap.ContainsKey(shaderName))
                {
                    return mShaderMap[shaderName];
                }
                else
                {
                    Debug.LogError(string.Format("{0} �в����� {1} ", mConfig.SHADER_AB_RELATIVE_PATH, shaderName));
                    return null;
                }
            }
        }


        /// <summary>
        /// �������Ͳ���������Դ
        /// </summary>
        /// <param name="path">��Դ���·��</param>
        /// <param name="assetName">��Դ����</param>
        /// <param name="type">��Դ����</param>
        /// <param name="isSync">�Ƿ�ͬ������</param>
        /// <param name="callback">���ػص�</param>
        public void LoadRes(string path, AssetType type, Action<UnityEngine.Object, SResRef> callback = null, bool isSync = false)
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
                    Log.Warning( $"{type}û�ж�Ӧ����Դ���ط���");
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

        private void LoadAll<T1, T2>(string assetPath, AssetType assetType, Action<List<T1>, SResRef> callback, bool isSync = false, bool isPreload = false) where T1 : UnityEngine.Object where T2 : SRes, new()
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
                    if (asset != null)
                    {
                        List<System.Object> objectList = (asset as IEnumerable<System.Object>).Cast<System.Object>().ToList();
                        List<T1> assetList = objectList.ConvertAll((item) => { return item as T1; });
                        assetList.RemoveAll((item) => { return item == null; });
                        callback(assetList, resRef);
                    }
                    else
                    {
                        callback(null, null);
                    }
                };
            }

            T2 res = GetSRes<T2>(assetPath, "*", assetType, true);
            res.StartLoad(isSync, true, isPreload, tCallBack);
        }
        #endregion

        #region Load Resource��Դ
        /// <summary>
        /// �������Ͳ���������Դ
        /// </summary>
        /// <param name="path">��Դ���·��</param>
        /// <param name="type">��Դ����</param>
        public T LoadResourceRes<T>(string path, AssetType type) where T : UnityEngine.Object
        {
            UnityEngine.Object res = null;
            switch (type)
            {
                case AssetType.ePrefab:
                    GameObject resGO = Resources.Load<GameObject>(path);
                    res = GameObject.Instantiate(resGO);
                    (res as GameObject).ResetPRS();
                    break;
            }
            return res as T;
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
                    //��������
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
    }
}
