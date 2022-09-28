using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SEngine
{
    public class SResText : SRes
    {
        public SResText()
        {
        }

        public override Type GetRealType()
        {
            return typeof(TextAsset);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".txt", ".bytes" };
        }
#endif
    }
}
