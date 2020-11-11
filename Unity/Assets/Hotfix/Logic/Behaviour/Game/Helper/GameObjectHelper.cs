using System;
using System.Collections.Generic;

namespace ET
{
    public static class GameObjectHelper
    {
        public static K GetOrAddComponent<K>(this UnityEngine.GameObject go)where K:UnityEngine.Component
        {
            if(!go.TryGetComponent(out K k))
            {
                k = go.AddComponent<K>();
            }
            return k;
        }
    }
}
