using System.Collections.Generic;

namespace JSONx.AST
{
    public class ListNode : JSONxNode
    {
        private readonly List<JSONxNode> _items;

        public override IEnumerable<JSONxNode> Children => _items;
        public override bool HasChildren => _items.Count > 0;

        public ListNode() : this(new List<JSONxNode>(0)) { }
        public ListNode(List<JSONxNode> nodes) : base(NodeType.List)
        {
            _items = nodes;
        }
    }
}