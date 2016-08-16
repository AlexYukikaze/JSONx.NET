using System.Collections.Generic;

namespace JSONx.AST
{
    public class ObjectNode : JSONxNode
    {
        public List<PropertyNode> Children { get; }
        public ObjectNode(List<PropertyNode> props) : base(NodeType.Object)
        {
            Children = props;
        }
    }
}