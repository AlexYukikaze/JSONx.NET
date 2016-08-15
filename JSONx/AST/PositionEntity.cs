namespace JSONx.AST
{
    public class PositionEntity
    {
        public Position Begin { get; set; }
        public Position End { get; set; }

        public int Length
        {
            get { return End.Index - Begin.Index; }
        }

        public PositionEntity() : this(new Position(0, 1, 1), new Position(0, 1, 1))
        {
        }

        public PositionEntity(Position begin, Position end)
        {
            Begin = begin;
            End = end;
        }

        public bool IndexInRange(int index)
        {
            return index >= Begin.Index && index <= End.Index;
        }
    }
}