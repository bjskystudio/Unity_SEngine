using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SEngine.UI
{
    public class SScrollRect : ScrollRect
    {

        public enum Direction
        {
            Horizontal,
            Vertical
        }
        //滑动方向
        public Direction m_direction = Direction.Horizontal;
        //当前操作方向
        private Direction m_BeginDragDirection = Direction.Horizontal;
        /// <summary>
        /// 关联滚动的父级scroll
        /// </summary>
        public ScrollRect ParentScrollRect;

        protected override void Awake()
        {
            base.Awake();
            m_direction = horizontal ? Direction.Horizontal : Direction.Vertical;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            if (ParentScrollRect)
            {
                m_BeginDragDirection = Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y) ? Direction.Horizontal : Direction.Vertical;
                if (m_BeginDragDirection != m_direction)
                {
                    ParentScrollRect.OnBeginDrag(eventData);
                }
            }
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            base.OnEndDrag(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            if (ParentScrollRect && m_BeginDragDirection != m_direction)
            {
                ParentScrollRect.OnEndDrag(eventData);
            }
        }

        public override void OnDrag(PointerEventData eventData)
        {
            base.OnDrag(eventData);
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            if (ParentScrollRect && m_BeginDragDirection != m_direction)
            {
                ParentScrollRect.OnDrag(eventData);
            }
        }
    }
}