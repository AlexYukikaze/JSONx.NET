using System;

namespace JSONx.AST
{
    public class StringNode : JSONxNode, IEquatable<StringNode>
    {
        public string Value { get; set; }
        public StringNode(string value) : base(NodeType.String)
        {
            Value = value;
        }

        public bool Equals(StringNode other)
        {
            return string.Equals(Value, other.Value);
        }
    }
}