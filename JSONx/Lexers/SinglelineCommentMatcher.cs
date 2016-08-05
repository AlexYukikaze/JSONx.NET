namespace JSONx.Lexers
{
    public class SinglelineCommentMatcher : Matcher
    {
        protected override Token MatchToken(Tokenizer tokenizer)
        {
            if (tokenizer.Current != '/' && tokenizer.Peek(1) != '/')
                return null;

            var begin = tokenizer.Position;
            tokenizer.Consume(2);
            while (!tokenizer.End())
            {
                if (tokenizer.Current == '\n')
                {
                    tokenizer.NewLine();
                }
                else
                {
                    tokenizer.Consume();
                }
            }

            return new Token(TokenType.SinglelineComment, new TokenSpan(begin, tokenizer.Position));
        }
    }
}