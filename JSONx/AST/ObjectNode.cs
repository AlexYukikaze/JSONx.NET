using System.Collections.Generic;

namespace JSONx.AST
{
    public class ObjectNode : JSONxNode
    {
        private readonly List<PropertyNode> _items;

        public override IEnumerable<JSONxNode> Children => _items;
        public override bool HasChildren => _items.Count > 0;

        public ObjectNode(List<PropertyNode> props) : base(NodeType.Object)
        {
            _items = props;
            foreach (var node in props)
            {
                node.Parent = this;
            }
        }
    }
}