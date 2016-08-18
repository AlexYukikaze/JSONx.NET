namespace JSONx.AST
{
    public class NumberNode : JSONxNode
    {
        public double Value { get; set; }
        public NumberNode(double value) : base(NodeType.Number)
        {
            Value = value;
        }
    }
}