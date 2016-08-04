namespace JSONx.Lexers
{
    public class TokenSpan
    {
        public int Index { get; set; }
        public int Length { get; set; }
        public PositionEntry Begin { get; set; }
        public PositionEntry End { get; set; }

        public TokenSpan() : this(0, 0, new PositionEntry(1, 1), new PositionEntry(1, 1))
        {
        }

        public TokenSpan(int index, int length, PositionEntry begin, PositionEntry end)
        {
            Index = index;
            Length = length;
            Begin = begin;
            End = end;
        }
    }
}