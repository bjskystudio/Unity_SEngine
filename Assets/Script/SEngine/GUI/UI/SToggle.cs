using SEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SEngine.UI
{
    public class SToggle : Toggle, IGrayMember
    {

        /// <summary>
        /// �����ı�
        /// </summary>
        public STMP Text;
        protected override void Awake()
        {
            base.Awake();
            if (Text)
                Text.raycastTarget = false;
        }

        /// <summary>
        /// ���ð�ť�ı�
        /// </summary>
        /// <param name="desc"></param>
        public void SetText(string desc)
        {
            if (Text)
                Text.text = desc;
        }

        public bool IsGray { get; private set; }

        /// <summary>
        /// ���
        /// </summary>
        public void SetGray(bool bo)
        {

        }
        public void SetGrayWithInteractable(bool bo)
        {
            interactable = !bo;
            SetGray(bo);
        }

    }
}