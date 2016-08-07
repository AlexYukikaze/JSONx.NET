using System;
using System.Collections.Generic;

namespace JSONx.Lexers
{
    public abstract class TokenStream<T>
    {
        protected readonly List<T> Items;

        public int Index { get; protected set; }

        public virtual T Current
        {
            get
            {
                return EOF() ? default(T) : Items[Index];
            }
        }

        protected TokenStream(Func<List<T>> creator)
        {
            Index = 0;
            Items = creator();
        }

        public abstract void TakeSnapshot();
        public abstract void CommitSnapshot();
        public abstract void RollbackSnapshot();

        public virtual void Consume(int count = 1)
        {
            Index += count;
        }

        public virtual T Peek(int lookahead = 0)
        {
            return EOF(lookahead) ? default(T) : Items[Index + lookahead];
        }

        public bool End()
        {
            return EOF();
        }

        protected bool EOF(int lookahead = 0)
        {
            return Index + lookahead >= Items.Count;
        }
    }
}