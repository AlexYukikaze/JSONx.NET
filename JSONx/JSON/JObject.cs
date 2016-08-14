using System.Collections.Generic;
using System.Linq;
using JSONx.JSON;

namespace JSONx.JSON
{
    public class JObject : JNode
    {
        private readonly JPropertyCollection _items;

        public override bool HasChildren
        {
            get { return _items.Count > 0; }
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public IEnumerable<string> Keys
        {
            get { return _items.Keys; }
        }

        public IEnumerable<JNode> Values
        {
            get { return _items.Select(property => property.Value); }
        }

        public override JNode this[string key]
        {
            get { return _items[key].Value; }
            set
            {
                JProperty prop;
                if (_items.TryGetValue(key, out prop))
                {
                    prop.Value = value;
                }
                else
                {
                    _items[key] = new JProperty(key, value);
                }
            }
        }

        public JObject() : this(new JProperty[0]) { }
        public JObject(params JProperty[] items) : this((IEnumerable<JProperty>)items) { }
        public JObject(IEnumerable<JProperty> items) : base(JType.Object, new JPropertyCollection(items))
        {
            _items = (JPropertyCollection)Storage;
        }

        public void Clear()
        {
            _items.Clear();
        }


        public bool ContainsKey(string key)
        {
            return _items.ContainsKey(key);
        }

        public void Add(string key, JNode value)
        {
            _items.Add(key, new JProperty(key, value));
        }

        public void Add(JProperty item)
        {
            _items.Add(item);
        }

        public bool Remove(string key)
        {
            return _items.Remove(key);
        }

        public bool TryGetValue(string key, out JNode value)
        {
            JProperty prop;
            if (_items.TryGetValue(key, out prop))
            {
                value = prop.Value;
                return true;
            }
            value = null;
            return false;
        }

        public override IEnumerator<JNode> GetEnumerator()
        {
            return _items.GetEnumerator();
        }
    }
}