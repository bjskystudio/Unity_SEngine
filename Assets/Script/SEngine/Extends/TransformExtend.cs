using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace SEngine
{
    /// <summary>
    /// 说明：Transform的扩展方法
    /// </summary>
    [LuaCallCSharp]

    public static class TransformExtend
    {
        #region 坐标转换
        /// <summary>
        /// 转换成基于父transform的坐标
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="cam"></param>
        /// <param name="pRect"></param>
        /// <returns></returns>
        public static Vector3 ConvertLocalPositionToParent(this Transform rect,Camera cam ,RectTransform pRect)
        {
            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(cam, rect.position);
            Vector2 localPoint;
            bool success = RectTransformUtility.ScreenPointToLocalPointInRectangle(pRect, screenPoint, cam, out localPoint);
            if (success)
            {
                return new Vector3(localPoint.x, localPoint.y, 0);
            }else
            {
                return Vector3.zero;
            }
        }
        #endregion

        #region Dotween 扩展
        /// <summary>
        /// 做一个二阶贝塞尔运动
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="startPos">开始位点</param>
        /// <param name="controlPos">控制点</param>
        /// <param name="endPos">结束点</param>
        /// <param name="during">持续时间</param>
        public static Tweener DoBezier2(this Transform transform,Vector3 startPos,Vector3 controlPos,Vector3 endPos ,float during)
        {
            Vector3[] pathvec = MathUtils.Bezier2Path(startPos, controlPos, endPos);
            return transform.DOLocalPath(pathvec, during);
        }
        #endregion
    }
}
