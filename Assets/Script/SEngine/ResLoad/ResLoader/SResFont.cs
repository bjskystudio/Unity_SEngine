using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SEngine
{
    public class SResFont : SRes
    {
        public SResFont()
        {
        }

        public override Type GetRealType()
        {
            return typeof(Font);
        }

#if UNITY_EDITOR

        public override List<string> GetExtesions()
        {
            return new List<string>() { ".ttf", ".fontsettings" };
        }
#endif
    }
}
