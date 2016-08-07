namespace JSONx.Lexers
{
    public abstract class Matcher
    {
        public Token Match(Tokenizer tokenizer)
        {
            if (tokenizer.End())
            {
                var position = tokenizer.Position;
                return new Token(TokenType.EOF, new TokenSpan(position, position));
            }

            tokenizer.TakeSnapshot();
            var token = MatchToken(tokenizer);
            if (token == null)
            {
                tokenizer.RollbackSnapshot();
            }
            else
            {
                token.Span.Begin = tokenizer.PeekSnapshot();
                token.Span.End = tokenizer.Position;
                tokenizer.CommitSnapshot();
            }
            return token;
        }

        protected abstract Token MatchToken(Tokenizer tokenizer);
    }
}