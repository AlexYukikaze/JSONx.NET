using System;
using JSONx.AST;

namespace JSONx.Lexers
{
    public class Token : PositionEntity, IEquatable<Token>
    {
        public TokenType Type { get; }
        public string Value { get; }

        public Token(TokenType type, Position begin, Position end, string value) : base(begin, end)
        {
            Type = type;
            Value = value;
        }

        public Token(TokenType type, Position begin, Position end) :
            this(type, begin, end, null) { }

        public Token(TokenType type, string value) :
            this(type, new Position(0, 1, 1), new Position(0, 1, 1), value) { }

        public Token(TokenType type) :
            this(type, new Position(0, 1, 1), new Position(0, 1, 1), null) { }

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