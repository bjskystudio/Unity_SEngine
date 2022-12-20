using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SEngine
{
    public class SResShader : SRes
    {
        public SResShader()
        {
        }

        public override Type GetRealType()
        {
            return typeof(Shader);
        }

#if UNITY_EDITOR
        public override List<string> GetExtesions()
        {
            return new List<string>() { ".shader" };
        }
#endif
    }
}
