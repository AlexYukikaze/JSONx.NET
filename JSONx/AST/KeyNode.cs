using System;

namespace JSONx.AST
{
    public class KeyNode : JSONxNode, IEquatable<KeyNode>
    {
        public string Value { get; }
        public KeyNode(string value) : base(NodeType.Key)
        {
            Value = value;
        }

        public bool Equals(KeyNode other)
        {
            return string.Equals(Value, other.Value);
        }
    }
}