using System;

namespace JSONx.AST
{
    public class Position : IEquatable<Position>
    {
        public int Index { get; }
        public int Row { get; }
        public int Column { get; }

        public Position(int index, int row, int col)
        {
            Index = index;
            Row = row;
            Column = col;
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Index.Equals(other.Index) && Row.Equals(other.Row) && Column.Equals(other.Column);
        }

        public override string ToString()
        {
            return string.Format(Utils.POSITION_MESSAGE_FORMAT, Index, Row, Column);
        }
    }
}