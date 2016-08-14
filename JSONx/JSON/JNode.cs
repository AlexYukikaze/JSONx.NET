using System;
using System.Collections;
using System.Collections.Generic;

namespace JSONx.JSON
{
    public abstract class JNode : IEnumerable<JNode>
    {
        public static readonly JNode Null = new JValue();
        protected readonly object Storage;
        public JType Type { get; }

        protected JNode(JType type, object value)
        {
            Type = type;
            Storage = value;
        }

        public virtual JNode this[string key]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public virtual JNode this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public abstract IEnumerator<JNode> GetEnumerator();
        public abstract bool HasChildren { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(obj, null)) return false;
            var node = obj as JNode;
            return node != null && Equals(Type, node.Type);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Storage != null ? Storage.GetHashCode() : 0) * 397) ^ (int) Type;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static implicit operator JNode(int value)
        {
            return new JValue(value);
        }

        public static implicit operator JNode(long value)
        {
            return new JValue(value);
        }

        public static implicit operator JNode(float value)
        {
            return new JValue(value);
        }

        public static implicit operator JNode(double value)
        {
            return new JValue(value);
        }

        public static implicit operator JNode(bool value)
        {
            return new JValue(value);
        }

        public static implicit operator JNode(string value)
        {
            return new JValue(value);
        }
    }
}