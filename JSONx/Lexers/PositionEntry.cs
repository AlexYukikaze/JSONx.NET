using System;

namespace JSONx.Lexers
{
    public class PositionEntry : IEquatable<PositionEntry>
    {
        public int Row { get; }
        public int Column { get; }

        public PositionEntry(int row, int col)
        {
            Row = row;
            Column = col;
        }

        public bool Equals(PositionEntry other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Row.Equals(other.Row) && Column.Equals(other.Column);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Row, Column);
        }
    }
}