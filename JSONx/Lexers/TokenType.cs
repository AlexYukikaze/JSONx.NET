namespace JSONx.Lexers
{
    public enum TokenType
    {
        LeftCurlyBracket,
        RightCurlyBracket,
        LeftSquareBracket,
        RightSquareBracket,
        Dollar,
        Colon,
        Comma,

        Number,
        String,
        True,
        False,
        Null,

        EOF
    }
}