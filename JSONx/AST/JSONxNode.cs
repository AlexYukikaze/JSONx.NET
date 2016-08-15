namespace JSONx.AST
{
    abstract class JSONxNode : PositionEntity
    {
        protected JSONxNode(Position begin, Position end) : base(begin, end)
        {
        }
    }
}