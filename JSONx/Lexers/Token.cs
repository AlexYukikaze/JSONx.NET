using System;

namespace JSONx.Lexers
{
    public class Token : IEquatable<Token>
    {
        public TokenType Type { get; }
        public string Value { get; }
        public TokenSpan Span { get; set; }

        public Token(TokenType type, TokenSpan span, string value)
        {
            Type = type;
            Span = span;
            Value = value;
        }

        public Token(TokenType type, TokenSpan span) :
            this(type, span, null) { }

        public Token(TokenType type, string value) :
            this(type, new TokenSpan(), value) { }

        public Token(TokenType type) :
            this(type, new TokenSpan(), null) { }

        public bool Equals(Token other)
        {
            if (ReferenceEquals(this, other)) return true;
            if (ReferenceEquals(null, other)) return false;
            return Type.Equals(other.Type) && string.Equals(Value, other.Value);
        }

        public override string ToString()
        {
            return string.Concat("JSONxToken(", Type, Value == null ? ")" : ", " + Value + ")");
        }
    }
}