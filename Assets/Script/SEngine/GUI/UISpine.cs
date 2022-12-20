using Spine;
using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace SEngine.UI
{
    public class UISpine : MonoBehaviour
    {
        class AnimParam
        {
            public AnimParam(string name, bool loop,Action complete)
            {
                Name = name;
                Loop = loop;
                Compltete = complete;
            }
            public string Name = "";
            public bool Loop = false;
            public Action Compltete = null;
        }

        public SkeletonGraphic SkelAnim;
        public Spine.AnimationState AnimState;
        public Spine.Skeleton Skel;

        public Action completeCallback;

        AnimParam SavedAnimParam = null;
        string SavedSkinName = "";

        private void Awake()
        {
            SkelAnim = gameObject.GetComponent<SkeletonGraphic>();
            if (SkelAnim !=null)
            {
                AnimState = SkelAnim.AnimationState;
                Skel = SkelAnim.Skeleton;

                // registering for events raised by any animation
                AnimState.Start += OnSpineAnimationStart;
                AnimState.Interrupt += OnSpineAnimationInterrupt;
                AnimState.End += OnSpineAnimationEnd;
                AnimState.Dispose += OnSpineAnimationDispose;
                AnimState.Complete += OnSpineAnimationComplete;

                AnimState.Event += OnUserDefinedEvent;

                if (SavedAnimParam != null)
                {
                    PlayAnim(SavedAnimParam.Name, SavedAnimParam.Loop, SavedAnimParam.Compltete);
                    SavedAnimParam = null;
                }
                if (SavedSkinName != "")
                {
                    SetSkin(SavedSkinName);
                    SavedSkinName = "";
                }
            }
            else
            {
                Log.Debug("spine load Error:no SkeletonGraphic");
            }
        }

        public void OnUserDefinedEvent(Spine.TrackEntry trackEntry, Spine.Event e)
        {

            if (e.Data.Name == "targetEvent")
            {
                // Add your implementation code here to react to user defined event
            }
        }

        public void OnSpineAnimationStart(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to start events
        }
        public void OnSpineAnimationInterrupt(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to interrupt events
        }
        public void OnSpineAnimationEnd(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to end events
        }
        public void OnSpineAnimationDispose(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to dispose events
        }
        public void OnSpineAnimationComplete(TrackEntry trackEntry)
        {
            // Add your implementation code here to react to complete events
        }

        public void PlayAnim(string name,bool loop,Action complete)
        {
            completeCallback = complete;
            if (AnimState != null)
            {
                try
                {
                    TrackEntry track = AnimState.SetAnimation(0, name, loop);
                    track.Complete += (TrackEntry trackEntry) => {
                        complete?.Invoke();
                    };
                }
                catch (Exception e)
                {
                    Log.Debug("spine play Error:" + e.Message + ",name:" + name);
                }
            }
            else
            {
                SavedAnimParam = new AnimParam(name,loop,complete);
            }

        }
        public void StopAnim(bool setSetupPose)
        {
            if (setSetupPose)
            {
                AnimState?.SetEmptyAnimation(0, 0.2f);
            }
            else
            {
                AnimState?.ClearTrack(0);
            }
        }

        /// <summary>
        ///  设置骨骼属性值
        /// </summary>
        /// <param name="boneName">骨骼名称</param>
        /// <param name="propName">属性名</param>
        /// <param name="propValue">属性值</param>
        public void SetBonePropValue(string boneName,string propName, float propValue)
        {
            Bone bone = Skel.FindBone(boneName);
            if (bone!= null)
            {
                //反射设置属性
                PropertyInfo pi = bone.GetType().GetProperty(propName);
                if(pi!= null){
                    pi.SetValue(bone, propValue);
                }
            }
        }
        /// <summary>
        /// 设置皮肤
        /// </summary>
        /// <param name="skinName"></param>
        public void SetSkin(string skinName)
        {
            if (Skel!= null)
            {
                Skel.SetSkin(skinName);
                Skel.SetSlotsToSetupPose();
            }
            else
            {
                SavedSkinName = skinName;
            }

        }
    }
}