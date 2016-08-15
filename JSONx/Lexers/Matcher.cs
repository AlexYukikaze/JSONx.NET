namespace JSONx.Lexers
{
    public abstract class Matcher
    {
        public Token Match(Tokenizer tokenizer)
        {
            tokenizer.TakeSnapshot();
            var token = MatchToken(tokenizer);
            if (token == null)
            {
                tokenizer.RollbackSnapshot();
            }
            else
            {
                token.Begin = tokenizer.PeekSnapshot();
                token.End = tokenizer.Position;
                tokenizer.CommitSnapshot();
            }
            return token;
        }

        protected abstract Token MatchToken(Tokenizer tokenizer);
    }
}