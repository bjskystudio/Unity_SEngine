using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SEngine.UI
{
    public class SText : Text, IGrayMember
    {

        public const string GrayColor = "#585858";

        public bool IsGray { get; private set; }

        private Color oldColor;

        public void SetGray(bool isGray)
        {
            if (IsGray == isGray)
                return;
            IsGray = isGray;
            if (isGray)
            {
                oldColor = color;
                ColorUtility.TryParseHtmlString(GrayColor, out Color newColor);
                color = newColor;
            }
            else
            {
                color = oldColor;
            }
        }
    }
}