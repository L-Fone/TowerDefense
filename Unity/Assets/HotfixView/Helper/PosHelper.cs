using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class PosHelper
    {
        public static Vector3 WorldToScreen(Vector3 position)
        {
            return new Vector3(position.x, Screen.height - position.y, position.z);
        }
    }
}
