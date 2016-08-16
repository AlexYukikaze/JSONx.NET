namespace JSONx.AST
{
    public class PropertyNode : JSONxNode
    {
        public KeyNode Key { get; }
        public JSONxNode Value { get; set; }
        public PropertyNode(KeyNode key, JSONxNode value) : base(NodeType.Property)
        {
            Key = key;
            Value = value;
        }
    }
}