using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class SerializableDictionary<K, V>: IEnumerable
    {
        [Serializable]
        private class DictItem
        {
            [SerializeField]
            public K key;
            [SerializeField]
            public V value;
        }

        [Serializable]
        private class Dict
        {
            [SerializeField]
            DictItem[] dictItems;

            public Dictionary<K, V> ToDictionary()
            {
                Dictionary<K, V> dict = new Dictionary<K, V>();

                foreach (var item in dictItems)
                {
                    dict.Add(item.key, item.value);
                }

                return dict;
            }
        }

        [SerializeField]
        private Dict _serialDict;

        public Dictionary<K, V> dictionary => _serialDict.ToDictionary();

        public IEnumerator GetEnumerator()
        {
            return (IEnumerator)dictionary;
        }
    }
}
