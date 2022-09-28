using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
namespace SEngine
{
    public class ResPath
    {
        /// <summary>
        /// 本地assetdatabase加载的路径
        /// </summary>
        public static string RES_LOCAL_ASSETDATABASE_RELATIVE_PATH
        {
            get
            {
                if (ResLoadManager.Instance.Config != null)
                {
                    return ResLoadManager.Instance.Config.RES_LOCAL_ASSETDATABASE_RELATIVE_PATH;
                }

                return "";
            }
        }

        /// <summary>
        /// 本地AB根路径
        /// </summary>
        public static string RES_LOCAL_AB_ROOT_PATH
        {
            get
            {
                if (ResLoadManager.Instance.Config != null)
                {
                    return Application.dataPath + "/" + ResLoadManager.Instance.Config.RES_LOCAL_AB_RELATIVE_PATH;
                }

                return "";
            }
        }

        /// <summary>
        /// 流式AB根路径
        /// </summary>
        public static string RES_STREAM_AB_ROOT_PATH
        {
            get
            {
                if (ResLoadManager.Instance.Config != null)
                {
                    return Application.streamingAssetsPath + "/" + ResLoadManager.Instance.Config.RES_STREAM_AB_RELATIVE_PATH;
                }

                return "";
            }
        }

        /// <summary>
        /// 沙盒资源根路径
        /// </summary>
        public static string RES_PERSISTENT_ROOT_PATH
        {
            get
            {
                if (ResLoadManager.Instance.Config != null)
                {
                    if (string.IsNullOrEmpty(ResLoadManager.Instance.Config.RES_PERSISTENT_RELATIVE_PATH))
                    {
                        return Application.persistentDataPath;
                    }
                    else
                    {
                        return Application.persistentDataPath + "/" + ResLoadManager.Instance.Config.RES_PERSISTENT_RELATIVE_PATH;
                    }
                }

                return "";
            }
        }

        /// <summary>
        /// SDK指定得热更新目录
        /// </summary>
        public static string RES_SDK_UPDATE_ROOT_PATH
        {
            get;
            set;
        }

#if UNITY_EDITOR
        //就是为了解决Assetdatabase需要传入资源后缀名才能加载的问题
        public static string GetExtension(string assetPath, SRes res)
        {
            if (res == null)
            {
                return "";
            }

            List<string> extensions = res.GetExtesions();
            if (extensions.Count == 1)
            {
                //某些资源只有一种扩展，就不用走下面的匹配
                return extensions[0];
            }

            //找到资源所在目录下的所有文件，然后依次比较名字，是否相同，然后得到扩展名（扩展名注意还要再extensions中才行，解决那种同名，但是不同后缀的问题）
            string fullPath = Application.dataPath.Replace("Assets", "") + assetPath;
            string directoryPath = Path.GetDirectoryName(fullPath);
            string fileName = Path.GetFileNameWithoutExtension(fullPath).ToLower();
            if (Directory.Exists(directoryPath))
            {
                string[] allFiles = Directory.GetFiles(directoryPath);
                for (int i = 0; i < allFiles.Length; i++)
                {
                    string extension = Path.GetExtension(allFiles[i]);
                    if (extension != ".meta" && extensions.Contains(extension))
                    {
                        string tempFileName = Path.GetFileNameWithoutExtension(allFiles[i]).ToLower();
                        if (fileName == tempFileName)
                        {
                            return extension;
                        }
                    }
                }

                return "";
            }
            else
            {
                Debug.LogError(string.Format("加载资源的目录{0}不存在，请确定！！！！", directoryPath));
                return "";
            }
        }
#endif

        public static string URL(string assetPath, SRes res = null)
        {
            StringBuilder result = new StringBuilder();

#if UNITY_EDITOR
            if (ResLoadManager.Instance.LoadMode == ResLoadMode.eAssetDatabase)
            {
                result.Append(RES_LOCAL_ASSETDATABASE_RELATIVE_PATH);
                result.Append("/");
                result.Append(assetPath);
                result.Append(GetExtension(result.ToString(), res));
            }
            else if (ResLoadManager.Instance.LoadMode == ResLoadMode.eAssetbundle)
#endif
            {
                //先判断热更新目录
                switch (Application.platform)
                {
                    case RuntimePlatform.IPhonePlayer:
                    case RuntimePlatform.Android:
                        {
                            result.Append(RES_SDK_UPDATE_ROOT_PATH);
                            if (!string.IsNullOrEmpty(ResLoadManager.Instance.Config.RES_PERSISTENT_AB_RELATIVE_PATH))
                            {
                                result.Append("/");
                                result.Append(ResLoadManager.Instance.Config.RES_PERSISTENT_AB_RELATIVE_PATH);
                            }
                        }
                        break;
                    case RuntimePlatform.OSXEditor:
                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.WindowsPlayer:
                    case RuntimePlatform.OSXPlayer:
                        {
                            result.Append(RES_PERSISTENT_ROOT_PATH);
                            if (!string.IsNullOrEmpty(ResLoadManager.Instance.Config.RES_PERSISTENT_AB_RELATIVE_PATH))
                            {
                                result.Append("/");
                                result.Append(ResLoadManager.Instance.Config.RES_PERSISTENT_AB_RELATIVE_PATH);
                            }
                        }
                        break;
                }

                result.Append("/");
                result.Append(assetPath);
                string tResult = result.ToString();
                if (File.Exists(tResult))
                {
                    return tResult;
                }
                else
                {
                    //更新目录没有，那么走流式目录
                    result.Clear();
                    switch (Application.platform)
                    {
                        case RuntimePlatform.Android:
                            {
                                result.Append(RES_STREAM_AB_ROOT_PATH);
                            }
                            break;
                        case RuntimePlatform.IPhonePlayer:
                        case RuntimePlatform.OSXPlayer:
                        case RuntimePlatform.WindowsPlayer:
                            {
                                result.Append(RES_STREAM_AB_ROOT_PATH);
                            }
                            break;
                        case RuntimePlatform.OSXEditor:
                        case RuntimePlatform.WindowsEditor:
                            {
                                result.Append(RES_LOCAL_AB_ROOT_PATH);
                            }
                            break;
                    }

                    result.Append("/");
                    result.Append(assetPath);
                }
            }
            return result.ToString();
        }
    }
}