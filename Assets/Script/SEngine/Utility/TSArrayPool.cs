// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/08/12 17:05:25
// ========================================================

//数组对象缓存池(线程安全)
//所有类型的数组对象都可以

using System;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public static class TSArrayPool<TType>
    {
        private static readonly object _ThreadHandle = new object();
        private static readonly Dictionary<int, Stack<TType[]>> _Pools = new Dictionary<int, Stack<TType[]>>();

        //bRange=true 目前暂定超过给定size的1.5容量都可以返回
        public static TType[] Get(int size, bool bRange = false)
        {
            lock (_ThreadHandle)
            {
                if (size <= 0)
                {
                    return null;
                }

                if (true == bRange)
                {
                    var maxSize = (int)(size * 1.5f);
                    
                    foreach (var pool in _Pools)
                    {
                        var currSize = pool.Key;
                        if (currSize == size
                            || (currSize > size && currSize <= maxSize)
                        )
                        {
                            var stack = pool.Value;
                            if (stack.Count > 0)
                            {
                                return stack.Pop();
                            }
                            
                            return new TType[size];
                        }
                    }
                    
                    return new TType[size];
                }
                else
                {
                    _Pools.TryGetValue(size, out var stack);
                    if (null == stack)
                    {
                        stack = new Stack<TType[]>();
                        _Pools[size] = stack;
                    }

                    if (stack.Count <= 0)
                    {
                        return new TType[size];
                    }
                    
                    return stack.Pop();
                }
            }
        }

        public static void Release(TType[] array)
        {
            lock (_ThreadHandle)
            {
                if (null == array)
                {
                    return;
                }
            
                _Pools.TryGetValue(array.Length, out var stack);
                if (null == stack)
                {
                    stack = new Stack<TType[]>();
                    _Pools[array.Length] = stack;
                }

                foreach (var item in stack)
                {
                    if (item == array)
                    {
                        int a = 0;
                     //   Debug.Log("QQQQQQQ: same");
                    }
                }
                
                Array.Clear(array, 0, array.Length);
                stack.Push(array);
                
               // Debug.Log($"#########Count: {_Pools.Count}");
            }
        }
    }
}