namespace JSONx.Lexers
{
    public class PositionEntry
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        public PositionEntry(int row, int col)
        {
            Row = row;
            Column = col;
        }
    }
}