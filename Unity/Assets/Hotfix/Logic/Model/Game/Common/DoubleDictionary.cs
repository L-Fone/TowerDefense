using ET;
using System;
using System.Collections.Generic;

namespace ET
{
    public class DoubleDictionary<T1, T2, K> : Dictionary<KeyValuePair<T1, T2>, K>
         //where T1 : notnull
         //where T2 : notnull
    {
        private readonly Dictionary<T1, K> _key1Dic = new Dictionary<T1, K>();
        private readonly MultiMap<T2, K> _key2Dic = new MultiMap<T2, K>();
        public DoubleDictionary() : base()
        {

        }
        public void Add(T1 key1, T2 key2, K value)
        {
            base.Add(new KeyValuePair<T1, T2>(key1, key2), value);
            _key1Dic.Add(key1, value);
            _key2Dic.Add(key2, value);
        }
        public bool Remove(T1 key1, T2 key2, K value)
        {
            _key1Dic.Remove(key1);
            _key2Dic.Remove(key2, value);
            return base.Remove(new KeyValuePair<T1, T2>(key1, key2));
        }
        public bool TryGetValueByKey1(T1 key1, out K value)
        {
            return _key1Dic.TryGetValue(key1, out value);
        }
        public bool TryGetValueByKey2(T2 key2, out List<K> list)
        {
            list = _key2Dic[key2];
            return list != null && list.Count != 0;
        }
        public IEnumerable<List<K>> GetAllByKey2()
        {
            return _key2Dic.GetDictionary().Values;
        }
        public new void Clear()
        {
            base.Clear();
            _key1Dic.Clear();
            _key2Dic.Clear();
        }
        public void OnDeserialization()
        {
            foreach (var item in this)
            {
                _key1Dic.Add(item.Key.Key, item.Value);
                _key2Dic.Add(item.Key.Value, item.Value);
            }
        }
    }
}
