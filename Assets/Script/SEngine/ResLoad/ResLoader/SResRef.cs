using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SEngine
{
    //资源加载返回这个包裹类，可以利用析构函数来释放未引用对象
    public class SResRef
    {
        private SRes mSRes;
        private bool mIsRelease = false;

        public string AssetPathInit
        {
            get
            {
                if(mSRes != null)
                {
                    return mSRes.AssetPathInit;
                }
                else
                {
                    Debug.LogError("ResRef mSRes is null");
                }

                return "";
            }
        }

        public SResRef(SRes sRes)
        {
            mSRes = sRes;
        }

        ~SResRef()
        {
            if(!mIsRelease)
            {
                Release();
            }
        }

        public void Release()
        {
            mIsRelease = true;
            if (mSRes != null)
            {
                //由于析构函数调用的Release是在垃圾回收线程，无法调用unity的api,我们这里处理将释放请求返还给主线程
                ResLoadManager.Instance.AddReleaseRequest(mSRes);
            }
        }
    }

}