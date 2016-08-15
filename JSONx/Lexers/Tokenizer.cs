using System.Collections.Generic;
using System.Linq;
using JSONx.AST;

namespace JSONx.Lexers
{
    public class Tokenizer : TokenStream<char>
    {
        protected int Column, Row;
        protected readonly Stack<Position> PositionSnapshots;

        public Position Position
        {
            get { return new Position(Index, Row, Column); }
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
            PositionSnapshots = new Stack<Position>();
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


        public Position PeekSnapshot()
        {

            return PositionSnapshots.Peek();
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