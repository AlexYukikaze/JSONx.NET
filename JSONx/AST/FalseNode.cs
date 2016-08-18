using System;

namespace JSONx.AST
{
    public class FalseNode : JSONxNode, IEquatable<FalseNode>
    {
        public FalseNode() : base(NodeType.Boolean)
        {
        }

        public bool Equals(FalseNode other)
        {
            return true;
        }
    }
}