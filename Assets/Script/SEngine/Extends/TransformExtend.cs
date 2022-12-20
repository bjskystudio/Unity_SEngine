using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace SEngine
{
    /// <summary>
    /// ˵����Transform����չ����
    /// </summary>
    [LuaCallCSharp]

    public static class TransformExtend
    {
        #region ����ת��
        /// <summary>
        /// ת���ɻ��ڸ�transform������
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

        #region Dotween ��չ
        /// <summary>
        /// ��һ�����ױ������˶�
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="startPos">��ʼλ��</param>
        /// <param name="controlPos">���Ƶ�</param>
        /// <param name="endPos">������</param>
        /// <param name="during">����ʱ��</param>
        public static Tweener DoBezier2(this Transform transform,Vector3 startPos,Vector3 controlPos,Vector3 endPos ,float during)
        {
            Vector3[] pathvec = MathUtils.Bezier2Path(startPos, controlPos, endPos);
            return transform.DOLocalPath(pathvec, during);
        }
        #endregion
    }
}
