using System;
using System.Collections.Generic;

namespace ET
{
    public static class DictionaryHelper
    {
        public static bool TryAdd<K,V>(this Dictionary<K,V> self, K k,V v)
        {
            if (self.ContainsKey(k))
            {
                self[k] = v;
                return false;
            }
            self.Add(k,v);
            return true;
        }
    }
}
