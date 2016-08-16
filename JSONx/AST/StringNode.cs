namespace JSONx.AST
{
    public class StringNode : JSONxNode
    {
        public string Value { get; set; }
        public StringNode(string value) : base(NodeType.String)
        {
            Value = value;
        }
    }
}