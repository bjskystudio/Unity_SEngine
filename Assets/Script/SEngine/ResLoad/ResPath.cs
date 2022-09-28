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
        /// ����assetdatabase���ص�·��
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
        /// ����AB��·��
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
        /// ��ʽAB��·��
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
        /// ɳ����Դ��·��
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
        /// SDKָ�����ȸ���Ŀ¼
        /// </summary>
        public static string RES_SDK_UPDATE_ROOT_PATH
        {
            get;
            set;
        }

#if UNITY_EDITOR
        //����Ϊ�˽��Assetdatabase��Ҫ������Դ��׺�����ܼ��ص�����
        public static string GetExtension(string assetPath, SRes res)
        {
            if (res == null)
            {
                return "";
            }

            List<string> extensions = res.GetExtesions();
            if (extensions.Count == 1)
            {
                //ĳЩ��Դֻ��һ����չ���Ͳ����������ƥ��
                return extensions[0];
            }

            //�ҵ���Դ����Ŀ¼�µ������ļ���Ȼ�����αȽ����֣��Ƿ���ͬ��Ȼ��õ���չ������չ��ע�⻹Ҫ��extensions�в��У��������ͬ�������ǲ�ͬ��׺�����⣩
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
                Debug.LogError(string.Format("������Դ��Ŀ¼{0}�����ڣ���ȷ����������", directoryPath));
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
                //���ж��ȸ���Ŀ¼
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
                    //����Ŀ¼û�У���ô����ʽĿ¼
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