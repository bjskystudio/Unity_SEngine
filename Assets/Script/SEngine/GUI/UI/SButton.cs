using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SEngine.UI
{
    public class SButton : Button, IGrayMember
    {
        /// <summary>
        /// �ϴε��ʱ��
        /// </summary>
        private static float LastClickTime = -1;
        /// <summary>
        /// ������
        /// </summary>
        private readonly float ClickInterval = 0.2f;

        private float time;
        /// <summary>
        /// �Ƿ���Ե���������
        /// </summary>
        public bool IgnoreClickInterval = false;

        /// <summary>
        /// ��ť�����ı�
        /// </summary>
        public STMP BtnText;

        protected override void Awake()
        {
            base.Awake();
            if (BtnText)
                BtnText.raycastTarget = false;
        }

        /// <summary>
        /// ���ð�ť�ı�
        /// </summary>
        /// <param name="desc"></param>
        public void SetText(string desc)
        {
            if (BtnText)
                BtnText.text = desc;
        }


        public override void OnPointerClick(PointerEventData eventData)
        {
            OnClickHandle(eventData);
        }

        protected virtual void OnClickHandle(PointerEventData eventData)
        {
            if (Input.touchCount >= 2) return;

            if (IgnoreClickInterval || Time.realtimeSinceStartup - LastClickTime >= ClickInterval)
            {
                time = Time.realtimeSinceStartup;

                OnClickInvoke();

                LastClickTime = Time.realtimeSinceStartup;

            }
        }
        protected virtual void OnClickInvoke()
        {
            onClick?.Invoke();
        }
        public bool IsGray { get; private set; }

        /// <summary>
        /// ���
        /// </summary>
        public void SetGray(bool bo)
        {
            //this.GetComponent<Image>().color = Color.gray;
           // Material mat = Resources.Load<Material>("Assets/Res/UI/Common/Button/ImageGrayShader");
            Material mat = new Material(Shader.Find("Assets/Res/Shader/UIGrayShader/ImageGrayShader"));
            if (transform.GetComponent<Image>()!=null)
            {
                transform.GetComponent<Image>().material = mat;
            }
           
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Image>() != null)
                {
                    transform.GetChild(i).GetComponent<Image>().material = mat;
                }
                
            }
        }

        public void SetGrayWithInteractable(bool bo)
        {
            interactable = !bo;
            SetGray(bo);
        }

    }
}
