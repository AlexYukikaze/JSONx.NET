using System;
using System.Collections.Generic;

namespace JSONx.Lexers
{
    public class TokenStream<T>
    {
        protected readonly List<T> Items;
        protected readonly Stack<int> Snapshots;

        public int Index { get; protected set; }

        public virtual T Current
        {
            get
            {
                return EOF() ? default(T) : Items[Index];
            }
        }

        public TokenStream(Func<List<T>> creator)
        {
            Index = 0;
            Snapshots = new Stack<int>();
            Items = creator();
        }

        public virtual void Consume()
        {
            Index++;
        }

        public virtual T Peek(int lookahead = 0)
        {
            return EOF(lookahead) ? default(T) : Items[Index + lookahead];
        }

        public bool End()
        {
            return EOF();
        }

        public virtual void TakeSnapshot()
        {
            Snapshots.Push(Index);
        }

        public virtual void CommitSnapshot()
        {
            Snapshots.Pop();
        }

        public virtual void RollbackSnapshot()
        {
            Index = Snapshots.Pop();
        }

        protected bool EOF(int lookahead = 0)
        {
            return Index + lookahead >= Items.Count;
        }
    }
}