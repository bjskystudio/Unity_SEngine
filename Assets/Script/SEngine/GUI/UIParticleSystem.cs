using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine.UI
{
    public class UIParticleSystem : MonoBehaviour
    {
        private Action PlayComplete = null;

        private List<ParticleSystem> psList = new List<ParticleSystem>();

        private void Awake()
        {
            //设置全部的粒子特效的Layer为UI,把粒子系统的Render的sortingOrder设置为大于Canvas的OrderInLayer
            //Canvas的Order In Layer默认为0，因此这里默认设置为1
            SetLayerAndSortOrder(gameObject.transform, 5, 1);
        }
        void SetLayerAndSortOrder(Transform parent, int layer, int sortOrder)
        {
            parent.gameObject.layer = layer;
            ParticleSystem ps = parent.GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ParticleSystemRenderer psRender = parent.GetComponent<ParticleSystemRenderer>();
                if (psRender != null)
                {
                    psRender.sortingOrder = sortOrder;
                }
                psList.Add(ps);
            }

            foreach (Transform child in parent)
            {
                SetLayerAndSortOrder(child, layer, sortOrder);
            }
        }

        /// <summary>
        /// 重设order
        /// </summary>
        /// <param name="sortOrder"></param>
        public void ResetSortOrder(int sortOrder)
        {
            SetLayerAndSortOrder(gameObject.transform, 5, sortOrder);
        }

        public ParticleSystem FirstParticle
        {
            get{
                if (psList.Count > 0)
                {
                    return psList[0];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 播放粒子
        /// </summary>
        /// <param name="during">持续时间</param>
        /// <param name="complete">回调</param>
        public void Play(float during = 0,Action complete = null)
        {
            if (FirstParticle != null)
            {
                FirstParticle.Play();
                if (during > 0 && complete != null)
                {
                    PlayComplete = complete;
                    Invoke("PlayCallback", during);
                }
            }
        }
        private void PlayCallback()
        {
            PlayComplete?.Invoke();
        }

        /// <summary>
        /// 结束粒子播放
        /// </summary>
        public void StopAndClear()
        {
            ParticleSystem ps = GetComponent<ParticleSystem>();
            if (ps != null)
            {
                ps.Stop();
                ps.Clear();
            }
        }
    }
}