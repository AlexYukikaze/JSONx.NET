using System;

namespace JSONx.AST
{
    public class NullNode : JSONxNode, IEquatable<NullNode>
    {
        public NullNode() : base(NodeType.Null)
        {
        }

        public bool Equals(NullNode other)
        {
            return true;
        }
    }
}