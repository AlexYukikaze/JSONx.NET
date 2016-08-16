using System.Collections.Generic;

namespace JSONx.AST
{
    public class PropertyNode : JSONxNode
    {
        public KeyNode Key { get; }
        public JSONxNode Value { get; set; }

        public override IEnumerable<JSONxNode> Children
        {
            get
            {
                yield return Key;
                yield return Value;
            }
        }

        public override bool HasChildren => true;

        public PropertyNode(KeyNode key, JSONxNode value) : base(NodeType.Property)
        {
            Key = key;
            Value = value;
        }
    }
}