namespace JSONx.Lexers
{
    public class WhitespaceMatcher : Matcher
    {
        protected override Token MatchToken(Tokenizer tokenizer)
        {
            if (!char.IsWhiteSpace(tokenizer.Current))
                return null;

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
            return new Token(TokenType.Whitespace);
        }
    }
}