using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

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
            return Extensions.Empty.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual bool Equals(JNode other)
        {
            if (ReferenceEquals(this, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Type.Equals(other.Type);
        }

        public abstract object Clone();
    }
}