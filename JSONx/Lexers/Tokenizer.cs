using System.Collections.Generic;
using System.Linq;

namespace JSONx.Lexers
{
    public class Tokenizer : TokenStream<char>
    {
        protected int Column, Row;
        protected readonly Stack<PositionEntry> PositionSnapshots;

        public PositionEntry Position
        {
            get { return new PositionEntry(Index, Row, Column); }
            private set
            {
                Index = value.Index;
                Row = value.Row;
                Column = value.Column;
            }
        }

        public Tokenizer(string source) : base(source.ToList)
        {
            Column = 1;
            Row = 1;
            PositionSnapshots = new Stack<PositionEntry>();
        }

        public override void Consume(int count = 1)
        {
            base.Consume(count);
            Column += count;
        }

        public void NewLine()
        {
            Consume();
            Column = 1;
            Row++;
        }

        public override void TakeSnapshot()
        {
            PositionSnapshots.Push(Position);
        }

        public override void CommitSnapshot()
        {
            PositionSnapshots.Pop();
        }

        public override void RollbackSnapshot()
        {
            Position = PositionSnapshots.Pop();
        }
    }
}