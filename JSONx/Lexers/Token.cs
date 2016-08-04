using System;

namespace JSONx.Lexers
{
    public class Token : IEquatable<Token>
    {
        public TokenType Type { get; }
        public string Value { get; }
        public TokenSpan Span { get; private set; }

        public Token(TokenType type, TokenSpan span, string value = null)
        {
            Type = type;
            Span = span;
            Value = value;
        }

        public bool Equals(Token other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Type.Equals(other.Type) && string.Equals(Value, other.Value);
        }
    }
}