using Coffee.UIEffects;
using DG.Tweening;
using SEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace SEngine.UI
{
    /// <summary>
    /// ��̬����ͼƬ��Image
    /// </summary>
    public class SImage : Image, IGrayMember
    {

        //��ǰ���ڼ��ص���Դ·��
        private string mCurrentPath = "";
        private SResRef mResRef = null;

        protected override void Awake()
        {
            base.Awake();
        }

        /// <summary>
        /// ��̬������Դ
        /// </summary>
        /// <param name="path">·��</param>
        /// <param name="isNativeSize">���ʱ����ԭʼ��С</param>
        /// <param name="targetAlpha">���ʱ����͸����</param>
        /// <param name="callback">��ɻص�</param>
        /// <param name="isAtlasSprite">�Ƿ�ͼ����Դ</param>
        public void LoadSprite(string path, bool isNativeSize, float targetAlpha, Action callback = null, bool isAtlasSprite = true)
        {
            if (string.IsNullOrEmpty(path))
            {
                Clear();
                return;
            }
            if (!string.IsNullOrEmpty(mCurrentPath) && mCurrentPath.Equals(path))
            {
                if (mResRef != null && mResRef.AssetPathInit.Equals(path))
                {
                    Alpha = targetAlpha;
                }
            }
            else
            {
                mCurrentPath = path;

                if (isAtlasSprite)
                {
                    ResLoadManager.Instance.LoadRes(path, AssetType.eAtlasSprite, (sp, resRef) =>
                     {
                         OnLoadSuccess(sp as Sprite, resRef, path, isNativeSize, targetAlpha, callback);
                     });
                }
                else
                {
                    ResLoadManager.Instance.LoadRes(path, AssetType.eSprite, (sp, resRef) =>
                     {
                         OnLoadSuccess(sp as Sprite, resRef, path, isNativeSize, targetAlpha, callback);
                     });
                }
            }
        }

        private void OnLoadSuccess(Sprite sp, SResRef resRef, string path, bool isNativeSize, float targetAlpha, Action callback)
        {
            if (sp != null)
            {
                if (this == null || gameObject == null)
                {
                    resRef.Release();
                    return;
                }
                if (mResRef != null)
                {
                    mResRef.Release();
                    mResRef = null;
                }
                sprite = sp;
                Alpha = targetAlpha;
                mResRef = resRef;
                if (isNativeSize)
                {
                    SetNativeSize();
                }
                callback?.Invoke();
            }
            else
            {
                if (resRef != null)
                {
                    resRef.Release();
                }
                Log.Error($"LoadSprite Error ������:{path}");
            }
        }

        public bool IsGray { get; private set; }

        private Color oldColor;

        public void SetGray(bool isGray)
        {
            if (IsGray == isGray)
                return;
            IsGray = isGray;
            if (isGray)
            {
                oldColor = color;
                color = new Color32(254, 254, 254, (byte)(255 * Alpha));
            }
            else
            {
                color = oldColor;
            }
        }

        public float Alpha
        {
            get
            {
                return color.a;
            }
            set
            {
                Color n = color;
                n.a = Mathf.Clamp(value, 0, 1);
                color = n;
            }
        }

        public void Clear(bool initAlpha = true)
        {
            if (initAlpha)
                Alpha = 0;
            sprite = null;
            mCurrentPath = null;
            if (mResRef != null)
            {
                mResRef.Release();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Clear();
        }
        /// <summary>
        /// ����Ч��
        /// </summary>
        /// <param name="during"></param>
        /// <param name="complete"></param>
        public void ShowUIShiny(float during,Action complete)
        {
            UIShiny effectShiny = gameObject.GetComponent<UIShiny>();
            if(effectShiny == null)
            {
                effectShiny = gameObject.AddComponent<UIShiny>();
            }
            effectShiny.effectFactor = 0;
            Tweener tweener = DoShiny(effectShiny, 1, during);
            tweener.OnComplete(() =>
            {
                complete?.Invoke();
            });
        }

        private Tweener DoShiny(UIShiny target, float endValue, float duration)
        {
            return DOTween.To(() => target.effectFactor, x => target.effectFactor = x, endValue, duration)
                .SetTarget(target);
        }

    }
}
