using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    public class PrefabAutoDestroy : MonoBehaviour
    {
        public SRes mRes;
        public SResRef mResRef;
        internal string mAssetPath;
        internal string mAssetPathInit;

        void OnDestroy()
        {
            if (mResRef != null)
            {
                mResRef.Release();
            }
        }

        void Copy(SRes res)
        {
            mRes = res;
            mResRef = new SResRef(res);
            mAssetPath = res.AssetPath;
            mAssetPathInit = res.AssetPathInit;
        }

        //外部实例化带有PrefabAutoDestroy的对象，需要调用这个借口增加引用
        public void AddRef(SRes res)
        {
            Copy(res);
            if (mRes != null)
            {
                mRes.AddRef();
            }
        }
    }

}