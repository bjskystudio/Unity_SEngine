using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
namespace SEngine
{
    /// <summary>
    /// 说明：GameObject的扩展方法
    /// </summary>
    [LuaCallCSharp]
    public static class UnityObjectExtends
    {

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
    }
}