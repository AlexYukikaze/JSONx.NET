namespace JSONx.AST
{
    public class NumberNode : JSONxNode
    {
        public decimal Value { get; set; }
        public NumberNode(decimal value) : base(NodeType.Number)
        {
            Value = value;
        }
    }
}