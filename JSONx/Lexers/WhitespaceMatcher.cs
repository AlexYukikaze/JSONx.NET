namespace JSONx.Lexers
{
    public class WhitespaceMatcher : Matcher
    {
        protected override Token MatchToken(Tokenizer tokenizer)
        {
            if (!char.IsWhiteSpace(tokenizer.Current))
                return null;

            var begin = tokenizer.Position;

            while (!tokenizer.End() && char.IsWhiteSpace(tokenizer.Current))
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
            return new Token(TokenType.Whitespace, new TokenSpan(begin, tokenizer.Position));
        }
    }
}