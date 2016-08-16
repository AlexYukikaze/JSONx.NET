namespace JSONx.AST
{
    public class KeyNode : JSONxNode
    {
        public string Value { get; }
        public KeyNode(string value) : base(NodeType.Key)
        {
            Value = value;
        }
    }
}