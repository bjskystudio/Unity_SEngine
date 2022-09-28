using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SEngine
{
    public class SResAudioCilp : SRes
    {
        public SResAudioCilp()
        { 
        }

        public override Type GetRealType()
        {
            return typeof(AudioClip);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".ogg", ".mp3", ".wav"};
        }
#endif
    }
}
