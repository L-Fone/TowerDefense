using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ET
{
    public static class ColorHelper
    {
        public static Color GetColorByStr(string colorStr)
        {
            ColorUtility.TryParseHtmlString(colorStr, out Color color);
            return color;
        }
    }
}
