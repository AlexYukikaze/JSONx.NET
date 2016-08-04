using System;
using System.Collections.Generic;

namespace JSONx.Lexers
{
    public class Tokenizer : TokenStream<char>
    {
        protected int Column, Row;
        protected readonly Stack<PositionEntry> PositionSnapshots;

        public PositionEntry Position
        {
            get { return new PositionEntry(Row, Column); }
            private set {
                Row = value.Row;
                Column = value.Column;
            }
        }

        public Tokenizer(Func<List<char>> creator) : base(creator)
        {
            Column = 1;
            Row = 1;
            PositionSnapshots = new Stack<PositionEntry>();
        }

        public override void Consume()
        {
            base.Consume();
            Column++;
        }

        public void NewLine()
        {
            Column = 1;
            Row++;
        }

        public override void TakeSnapshot()
        {
            base.TakeSnapshot();
            PositionSnapshots.Push(Position);
        }

        public override void CommitSnapshot()
        {
            base.CommitSnapshot();
            PositionSnapshots.Pop();
        }

        public override void RollbackSnapshot()
        {
            base.RollbackSnapshot();
            Position = PositionSnapshots.Pop();
        }
    }
}