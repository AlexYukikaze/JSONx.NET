using System;
using System.Collections.Generic;

namespace JSONx.JSON
{
    public class JValue : JNode
    {
        public override bool HasChildren
        {
            get { return false; }
        }

        public JValue() : base(JType.Null, null) {}
        public JValue(int value) : base(JType.Integer, value) {}
        public JValue(long value) : base(JType.Integer, value) {}
        public JValue(float value) : base(JType.Float, value) {}
        public JValue(double value) : base(JType.Float, value) {}
        public JValue(bool value) : base(JType.Boolean, value) {}
        public JValue(string value) : base(JType.String, value) {}

        public override IEnumerator<JNode> GetEnumerator()
        {
            yield break;
        }

        public override bool Equals(object obj)
        {
            if (!base.Equals(obj)) return false;
            var other = obj as JValue;
            if (other == null) return false;
            switch (other.Type)
            {
                case JType.Null:
                case JType.Integer:
                case JType.Float:
                case JType.Boolean:
                case JType.String:
                    return Equals(Storage, other.Storage);
                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "JSON(" + (Type == JType.Null ? "Null" : Storage.ToString()) + ")";
        }
    }
}