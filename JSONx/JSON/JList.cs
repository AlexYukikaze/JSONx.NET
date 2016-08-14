using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;

namespace JSONx.JSON
{
    public class JList : JNode, IList<JNode>
    {
        private readonly List<JNode> _items;

        public override bool HasChildren
        {
            get { return _items.Count > 0; }
        }

        public JList() : this(new List<JNode>()) { }
        public JList(params JNode[] items) : this((IEnumerable<JNode>) items) { }
        public JList(IEnumerable<JNode> items) : base(JType.List, new List<JNode>(items))
        {
            _items = (List<JNode>)Storage;
        }

        public override JNode this[int index]
        {
            get { return _items[index]; }
            set { _items[index] = value; }
        }

        public int IndexOf(JNode item)
        {
            return _items.IndexOf(item);
        }

        public void Insert(int index, JNode item)
        {
            _items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
        }

        public override IEnumerator<JNode> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(JNode item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(JNode item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(JNode[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public bool Remove(JNode item)
        {
            return _items.Remove(item);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        bool ICollection<JNode>.IsReadOnly
        {
            get { return false; }
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            var other = obj as JList;
            return other != null  && _items.SequenceEqual(other._items);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (_items != null ? _items.GetHashCode() : 0);
            }
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            result.Append("[");
            result.Append(string.Join(", ", _items));
            result.Append("]");
            return result.ToString();
        }
    }
}