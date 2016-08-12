using System.Globalization;

namespace JSONx.JSON
{
    public class JValue : JNode
    {
        private readonly object _value;

        public dynamic Value
        {
            get { return _value; }
        }

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
            return _value.Equals(other._value);
        }

        public override object Clone()
        {
            return new JValue(this);
        }
    }
}