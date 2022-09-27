using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public class CameraManager : MonoBehaviour
    {
        private static CameraManager inst;
        public Camera maincamera;
        MonoBehaviour cameradata;
        public Transform HudRoot { get; private set; }
        public Transform StoryRoot { get; private set; }

        private CinemachineBrain cinemachineBrain;
        private CinemachineBrain CinemachineCamera
        {
            get
            {
                if (cinemachineBrain == null)
                {
                    cinemachineBrain = maincamera.GetComponent<CinemachineBrain>();
                }
                return cinemachineBrain;
            }
        }

        public static CameraManager Get()
        {
            return inst;
        }

        public void Start()
        {
            maincamera = transform.Find("MainCamera").GetComponent<Camera>();
            inst = this;
            HudRoot = transform.Find("Canvas/HudRoot");
            StoryRoot = transform.Find("Canvas/StoryRoot");
            DontDestroyOnLoad(gameObject);
        }

        private void OnDestroy()
        {
            inst = null;
        }

        private void SetMainCameraCullingMask(int cullingMask)
        {
            maincamera.cullingMask = cullingMask;
        }

        public void VisibilityLayer(int layer, bool flag)
        {
            if (flag)
            {
                maincamera.cullingMask = (maincamera.cullingMask | 1 << layer);
            }
            else
            {
                maincamera.cullingMask = maincamera.cullingMask & ~(1 << layer);
            }
        }

        #region Cinemachine摄像机动画控制

        //private ResRef CBSettingResRef;

        //public void SetDefaultBlend(int style = 0, float time = 2)
        //{
        //    SetDefaultBlend((CinemachineBlendDefinition.Style)style, time);
        //}

        //public void SetDefaultBlend(CinemachineBlendDefinition.Style style, float time)
        //{
        //    SetBlendData(null);
        //    CinemachineCamera.m_DefaultBlend = new CinemachineBlendDefinition(style, time);
        //}

        //public void LoadBlendData(string settingPath)
        //{
        //    if (string.IsNullOrEmpty(settingPath))
        //    {
        //        SetBlendData(null);
        //    }
        //    else
        //    {
        //        ResourceManager.Instance.LoadScriptableObject(settingPath, (so, res) =>
        //        {
        //            SetBlendData(so as CinemachineBlenderSettings);
        //            CBSettingResRef = res;
        //        }, true);
        //    }
        //}

        //public void SetBlendData(CinemachineBlenderSettings settings)
        //{
        //    ClearSBSetting();
        //    CinemachineCamera.m_CustomBlends = settings;
        //}

        //private void ClearSBSetting()
        //{
        //    if (CBSettingResRef != null)
        //    {
        //        CBSettingResRef.Release();
        //    }
        //}

        #endregion
    }
}