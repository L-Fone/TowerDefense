using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    public static class AnimationHelper
    {
        public static Ainmation8DirectionKey GetAnimation8DirectionKey(Vector3 vector3)
        {
            float x = vector3.x;
            float y = vector3.y;
            if (y > 0)
            {
                if (x > 0)
                {
                    return Ainmation8DirectionKey.Right_Up;
                }
                else
                    return Ainmation8DirectionKey.Left_Up;
            }
            else
            {
                if (x > 0)
                {
                       return Ainmation8DirectionKey.Right_Down;
                }
                else
                    return Ainmation8DirectionKey.Left_Down;
            }

        }
    }
}
