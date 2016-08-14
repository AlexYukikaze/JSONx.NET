using System;
using System.Collections;
using System.Collections.Generic;

namespace JSONx.JSON
{
    internal class JPropertyCollection : ICollection<JProperty>
    {
        private readonly Dictionary<string, JProperty> _items;

        public JPropertyCollection()
        {
            _items = new Dictionary<string, JProperty>(StringComparer.Ordinal);
        }

        public JPropertyCollection(IEnumerable<JProperty> items) : this()
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public IEnumerator<JProperty> GetEnumerator()
        {
            return _items.Values.GetEnumerator();
        }

        public void Add(JProperty item)
        {
            var prop = (JProperty) item;
            _items.Add(prop.Key, prop);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(JProperty item)
        {
            return ((ICollection<JProperty>) _items.Values).Contains(item);
        }

        public void CopyTo(JProperty[] array, int arrayIndex)
        {
            _items.Values.CopyTo(array, arrayIndex);
        }

        public bool Remove(JProperty item)
        {
            return _items.Remove(item.Key);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public void Add(string key, JProperty value)
        {
            _items.Add(key, value);
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public bool TryGetValue(string key, out JProperty value)
        {
            return _items.TryGetValue(key, out value);
        }

        public JProperty this[string key]
        {
            get { return _items[key]; }
            set { _items[key] = value; }
        }

        public ICollection<string> Keys
        {
            get { return _items.Keys; }
        }

        public ICollection<JProperty> Values
        {
            get { return _items.Values; }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}