using System.Collections.Generic;

namespace JSONx.AST
{
    public class ListNode : JSONxNode
    {
        public List<JSONxNode> Children { get; }
        public ListNode() : this(new List<JSONxNode>(0)) { }
        public ListNode(List<JSONxNode> nodes) : base(NodeType.List)
        {
            Children = nodes;
        }
    }
}