namespace JSONx.Lexers
{
    public class OperatorMatcher : Matcher
    {
        private readonly TokenType _type;
        private readonly string _pattern;

        public OperatorMatcher(TokenType type, string pattern)
        {
            _type = type;
            _pattern = pattern;
        }

        protected override Token MatchToken(Tokenizer tokenizer)
        {
            foreach (var ch in _pattern)
            {
                if (!tokenizer.Current.Equals(ch))
                    return null;
                tokenizer.Consume();
            }
            return new Token(_type);
        }
    }
}