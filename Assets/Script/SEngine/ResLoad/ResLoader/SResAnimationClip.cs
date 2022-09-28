using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SEngine
{
    public class SResAnimationClip : SRes
    {
        public SResAnimationClip()
        { 
        }

        public override Type GetRealType()
        {
            return typeof(AnimationClip);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".anim" };
        }
#endif
    }
}
