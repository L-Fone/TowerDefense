using System;
using System.Collections.Generic;

namespace ET
{
    public static class GameObjectHelper
    {
        public static K GetOrAddComponent<K>(this UnityEngine.GameObject go)where K:UnityEngine.Component
        {
            return go.GetComponent<K>() ?? go.AddComponent<K>();
        }
    }
}
