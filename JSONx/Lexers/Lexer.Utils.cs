namespace JSONx.Lexers
{
    public partial class Lexer
    {
        private static readonly Matcher[] Matchers =
        {


            new OperatorMatcher(TokenType.LeftCurlyBracket, "{"),
            new OperatorMatcher(TokenType.RightCurlyBracket, "}"),
            new OperatorMatcher(TokenType.LeftSquareBracket, "["),
            new OperatorMatcher(TokenType.RightSquareBracket, "]"),
            new OperatorMatcher(TokenType.Colon, ":"),
            new OperatorMatcher(TokenType.Comma, ","),
            new OperatorMatcher(TokenType.Dollar, "$"),

            new KeywordMatcher(TokenType.True, "true"),
            new KeywordMatcher(TokenType.False, "false"),
            new KeywordMatcher(TokenType.Null, "null"),

            new NumberMatcher(),
            new StringMatcher(StringMatcher.SINGLE_QUOTE),
            new StringMatcher(StringMatcher.DOUBLE_QUOTE),


        };

        private static readonly Matcher[] IgnoredMatches =
        {
            new WhitespaceMatcher(),
            new SinglelineCommentMatcher(),
            new MultilineCommentMatcher(),
        };
    }
}