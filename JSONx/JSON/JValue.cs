
using System;

namespace JSONx.JSON
{
    public class JValue : JNode, IEquatable<JValue>
    {
        protected object _value;

        public JValue(JType type) : base(type) { }

        public JValue(JValue other) : base(other.Type)
        {
            _value = other._value;
        }

        private JValue(JType type, object value) : base(type)
        {
            _value = value;
        }

        public JValue(int value) : this(JType.Integer, value) { }
        public JValue(long value) : this(JType.Integer, value) { }
        public JValue(float value) : this(JType.Float, value) { }
        public JValue(double value) : this(JType.Float, value) { }
        public JValue(bool value) : this(JType.Boolean, value) { }
        public JValue(string value) : this(JType.String, value) { }

        public bool Equals(JValue other)
        {
            if (!base.Equals(other)) return false;
            if (Type == JType.List) return Equals(this, other);
            return _value.Equals(other._value);
        }

        public override object Clone()
        {
            return new JValue(this);
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as JList);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (_value != null ? _value.GetHashCode() : 0);
            }
        }
    }
}