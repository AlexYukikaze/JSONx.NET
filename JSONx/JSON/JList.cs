using System;
using System.Collections.Generic;

namespace JSONx.JSON
{
    public class JList : JValue, IList<JValue>, IEquatable<JList>
    {
        private IList<JValue> items
        {
            get { return (IList<JValue>)_value; }
        }

        public JList() : base(JType.List)
        {
            _value = new List<JValue>();
        }

        public JList(JList other) : base(other.Type)
        {
            _value = new List<JValue>(other.items.Count);
            foreach (var item in other.items)
            {
                items.Add((JValue)item.Clone());
            }
        }

        public bool Equals(JList other)
        {
            if (ReferenceEquals(other, null)) return false;

            if (items.Count != other.Count) return false;

            for (int i = 0; i < items.Count; i++)
            {
                var a = items[i];
                var b = other.items[i];
                if (!a.Equals(b)) return false;
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JList);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override object Clone()
        {
            return new JList(this);
        }

        public override string ToString()
        {
            return string.Format("[{0}]", string.Join(", ", items));
        }

        #region IList<JValue> implimentation

        IEnumerator<JValue> IEnumerable<JValue>.GetEnumerator()
        {
            return items.GetEnumerator();
        }

        public void Add(JValue item)
        {
            items.Add(item);
        }

        public void Clear()
        {
            items.Clear();
        }

        public bool Contains(JValue item)
        {
            return items.Contains(item);
        }

        public void CopyTo(JValue[] array, int arrayIndex)
        {
            items.CopyTo(array, arrayIndex);
        }

        public bool Remove(JValue item)
        {
            return items.Remove(item);
        }

        public int Count
        {
            get { return items.Count; }
        }

        public bool IsReadOnly
        {
            get { return items.IsReadOnly; }
        }

        public int IndexOf(JValue item)
        {
            return items.IndexOf(item);
        }

        public void Insert(int index, JValue item)
        {
            items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }

        public JValue this[int index1]
        {
            get { return items[index1]; }
            set { items[index1] = value; }
        }

        #endregion
    }
}