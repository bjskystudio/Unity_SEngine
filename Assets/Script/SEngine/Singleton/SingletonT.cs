// ========================================================
// Copyright: Vavavoom Software Chengdu LLC 
// Author: SIMB Team 
// CreateTime: 2022/08/03 14:37:03
// ========================================================

//单体模板;

namespace SEngine
{
    public class SingletonT<T> where T : class, new()
    {
        private static T _instance;

        public static T I
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        public static void Free()
        {
            _instance = null;
        }
    }
}
