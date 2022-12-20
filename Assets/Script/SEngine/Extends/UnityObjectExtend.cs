using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace SEngine
{
    /// <summary>
    /// ˵����Unity Object����չ����
    /// </summary>
    [LuaCallCSharp]
    public static class UnityObjectExtends
    {
        public static bool IsNull(this Object o) // �������ֽ�IsDestroyed�ȵ�
        {
            return o == null;
        }

        #region ResetPRS ���ñ���λ�ã����ꡢ��ת������

        public static void ResetPRS(this Transform target)
        {
            if (target == null)
            {
                return;
            }
            target.localPosition = Vector3.zero;
            target.localEulerAngles = Vector3.zero;
            target.localScale = Vector3.one;
        }

        public static void ResetPRS(this Component target)
        {
            ResetPRS(target?.transform);
        }
        public static void ResetPRS(this GameObject target)
        {
            ResetPRS(target?.transform);
        }

        #endregion


        #region SetLocalScaleXYZ ���ñ�������XYZ ����s

        public static void SetLocalScaleXYZ(this Transform target, float s)
        {
            if (target == null)
            {
                return;
            }
            target.localScale = Vector3.one * s;
        }

        public static void SetLocalScaleXYZ(this Component target, float s)
        {
            SetLocalScaleXYZ(target?.transform, s);
        }

        public static void SetLocalScaleXYZ(this GameObject target, float s)
        {
            SetLocalScaleXYZ(target?.transform, s);
        }
        #endregion

        #region DestroyObject/DestroyGameObjDelay/ClearChildren ����

        public static void DestroyGameObj(this GameObject target)
        {
            if (target == null)
            {
                return;
            }
            GameObject.Destroy(target);
        }

        public static void DestroyGameObj(this Component target)
        {
            DestroyGameObj(target?.gameObject);
        }

        public static void DestroyGameObjDelay(this GameObject target, float time)
        {
            if (target == null)
            {
                return;
            }
            GameObject.Destroy(target, time);
        }

        public static void DestroyGameObjDelay(this Component target, float time)
        {
            DestroyGameObjDelay(target?.gameObject, time);
        }

        public static void ClearChildren(this Transform target, int index = 0)
        {
            if (target == null)
            {
                return;
            }
            int len = target.childCount;
            for (int i = len - 1; i >= index; i--)
            {
                Transform child = target.GetChild(i);
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void ClearChildren(this Component target, int index = 0)
        {
            ClearChildren(target?.transform, index);
        }

        public static void ClearChildren(this GameObject target, int index = 0)
        {
            ClearChildren(target?.transform, index);
        }

        #endregion
    }
}