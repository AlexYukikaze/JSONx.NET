namespace JSONx.Lexers
{
    public class KeywordMatcher : OperatorMatcher
    {
        public KeywordMatcher(TokenType type, string pattern) :
            base(type, pattern) { }

        protected override Token MatchToken(Tokenizer tokenizer)
        {
            var token = base.MatchToken(tokenizer);
            if (token == null)
            {
                return null;
            }

            var current = tokenizer.Current;
            if (char.IsLetterOrDigit(current) || current == '_')
            {
                return null;
            }

            return token;
        }
    }
}