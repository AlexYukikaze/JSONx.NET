namespace JSONx.Lexers
{
    public class TokenSpan
    {
        public PositionEntry Begin { get; set; }
        public PositionEntry End { get; set; }

        public int Length
        {
            get { return End.Index - Begin.Index; }
        }

        public TokenSpan() : this(new PositionEntry(0, 1, 1), new PositionEntry(0, 1, 1))
        {
        }

        public TokenSpan(PositionEntry begin, PositionEntry end)
        {
            Begin = begin;
            End = end;
        }
    }
}