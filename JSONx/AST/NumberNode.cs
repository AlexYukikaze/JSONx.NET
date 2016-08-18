using System;

namespace JSONx.AST
{
    public class NumberNode : JSONxNode, IEquatable<NumberNode>
    {
        public double Value { get; set; }
        public NumberNode(double value) : base(NodeType.Number)
        {
            Value = value;
        }

        public bool Equals(NumberNode other)
        {
            return Equals(Value, other.Value);
        }
    }
}