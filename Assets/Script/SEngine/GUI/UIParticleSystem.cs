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
            //����ȫ����������Ч��LayerΪUI,������ϵͳ��Render��sortingOrder����Ϊ����Canvas��OrderInLayer
            //Canvas��Order In LayerĬ��Ϊ0���������Ĭ������Ϊ1
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
        /// ����order
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
        /// ��������
        /// </summary>
        /// <param name="during">����ʱ��</param>
        /// <param name="complete">�ص�</param>
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
        /// �������Ӳ���
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