using System.Collections.Generic;
using System.Linq;

namespace JSONx.AST
{
    public abstract class JSONxNode : PositionEntity
    {
        internal static readonly IEnumerable<JSONxNode> Empty = Enumerable.Empty<JSONxNode>();

        public NodeType Type { get; }
        public JSONxNode Parent { get; internal set; }
        public virtual IEnumerable<JSONxNode> Children => Empty;
        public virtual bool HasChildren => false;

        protected JSONxNode(NodeType type) : base(new Position(0, 1, 1), new Position(0, 1, 1))
        {
            Type = type;
        }
    }
}