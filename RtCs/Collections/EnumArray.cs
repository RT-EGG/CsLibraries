using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RtCs
{
    public interface IReadOnlyEnumArray<Key, Value>
    {
        Value this[Key inKey] { get; }
    }

    public class EnumArray<Key, Value> : IReadOnlyEnumArray<Key, Value>, IEnumerable<KeyValuePair<Key, Value>> where Key : Enum
    {
        public EnumArray()
        {
            foreach (Key key in Enum.GetValues(typeof(Key))) {
                m_List.Add(key, default);
            }
            return;
        }

        public Value this[Key inKey]
        {
            get => m_List[inKey];
            set => m_List[inKey] = value;
        }

        public IEnumerable<Key> Keys
            => Enum.GetValues(typeof(Key)).Cast<Key>();
        public IEnumerable<Value> Values
            => m_List.Values;

        IEnumerator<KeyValuePair<Key, Value>> IEnumerable<KeyValuePair<Key, Value>>.GetEnumerator()
            => m_List.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator()
            => m_List.GetEnumerator();

        private Dictionary<Key, Value> m_List = new Dictionary<Key, Value>();
    }
}
