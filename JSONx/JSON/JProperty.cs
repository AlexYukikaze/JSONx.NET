using System.Collections.Generic;

namespace JSONx.JSON
{
    public class JProperty : JNode
    {
        public string Key { get; }
        public JNode Value { get; set; }

        public override bool HasChildren
        {
            get { return true; }
        }

        public JProperty(string key, JNode value) : base(JType.Property, new { key, value })
        {
            Key = key;
            Value = value;
        }

        public override IEnumerator<JNode> GetEnumerator()
        {
            yield return Value;
        }
    }
}