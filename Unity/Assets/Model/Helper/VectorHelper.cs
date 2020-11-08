using System;
using System.Collections.Generic;
using System.Numerics;

namespace ET
{
    public static class VectorHelper
    {
        public static Vector3 ToVector3(this UnityEngine.Vector3 vector3) => new Vector3(vector3.x, vector3.y, vector3.z);
        public static Vector2 ToVector2(this UnityEngine.Vector2 vector2) => new Vector2(vector2.x, vector2.y);
        public static UnityEngine.Vector3 ToUnityVector3(this Vector3 vector3) => new UnityEngine.Vector3(vector3.X, vector3.Y, vector3.Z);
        public static UnityEngine.Vector2 ToUnityVector2(this Vector2 vector2) => new UnityEngine.Vector2(vector2.X, vector2.Y);
    }
}
