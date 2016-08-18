using System;

namespace JSONx.AST
{
    public class TrueNode : JSONxNode, IEquatable<TrueNode>
    {
        public TrueNode() : base(NodeType.Boolean)
        {
        }

        public bool Equals(TrueNode other)
        {
            return true;
        }
    }
}