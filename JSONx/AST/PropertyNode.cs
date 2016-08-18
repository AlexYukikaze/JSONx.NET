using System;
using System.Collections.Generic;

namespace JSONx.AST
{
    public class PropertyNode : JSONxNode, IEquatable<PropertyNode>
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
            key.Parent = this;
            value.Parent = this;
        }

        public bool Equals(PropertyNode other)
        {
            return Key.Equals(other.Key);
        }
    }
}