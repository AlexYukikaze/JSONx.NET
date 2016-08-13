using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace JSONx.JSON
{
    public abstract class JNode : IEnumerable<JNode>, ICloneable, IEquatable<JNode>
    {
        public virtual JType Type { get; }

        protected JNode(JType type)
        {
            Type = type;
        }

        protected JNode(JNode other)
            : this(other.Type) { }

        public virtual IEnumerator<JNode> GetEnumerator()
        {
            return Enumerable.Empty<JNode>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool Equals(JNode other)
        {
            if (ReferenceEquals(this, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Type, other.Type);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JNode);
        }

        public override int GetHashCode()
        {
            return (int) Type;
        }

        public abstract object Clone();
    }
}