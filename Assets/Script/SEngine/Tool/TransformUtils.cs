using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public static class TransformUtils
    {

        /// <summary>
        /// 获取相对坐标
        /// </summary>
        /// <param name="position"></param>
        /// <param name="cam"></param>
        /// <param name="pRect"></param>
        /// <returns></returns>
        public static Vector3 ConvertLocalPositionToParent(Vector3 position, Camera cam, RectTransform pRect)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, position);
            Vector2 localPoint;
            bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(pRect, screenPoint, cam, out localPoint);
            if (success)
            {
                return new Vector3(localPoint.x, localPoint.y, 0);
            }
            else
            {
                return Vector3.zero;
            }
        }
    }
}