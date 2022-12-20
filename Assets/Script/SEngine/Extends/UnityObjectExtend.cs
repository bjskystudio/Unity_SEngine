using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace SEngine
{
    /// <summary>
    /// 说明：Unity Object的扩展方法
    /// </summary>
    [LuaCallCSharp]
    public static class UnityObjectExtends
    {
        public static bool IsNull(this Object o) // 或者名字叫IsDestroyed等等
        {
            return o == null;
        }

        #region ResetPRS 重置本地位置：坐标、旋转、缩放

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


        #region SetLocalScaleXYZ 设置本地缩放XYZ 参数s

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

        #region DestroyObject/DestroyGameObjDelay/ClearChildren 销毁

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