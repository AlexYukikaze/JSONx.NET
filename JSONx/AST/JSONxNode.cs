namespace JSONx.AST
{
    public abstract class JSONxNode : PositionEntity
    {
        public NodeType Type { get; }
        protected JSONxNode(NodeType type) : base(new Position(0, 1, 1), new Position(0, 1, 1))
        {
            Type = type;
        }
    }
}