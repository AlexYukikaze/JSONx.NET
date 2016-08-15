using System;
using JSONx.AST;

namespace JSONx.Lexers
{
    public class MatcherException : Exception
    {
        public Position Position { get; }

        public MatcherException(string message, Position pos) :
            base(message + string.Format(Utils.POSITION_MESSAGE_FORMAT, pos.Index, pos.Row, pos.Column))
        {
            Position = pos;
        }
    }

    public class LexerException : MatcherException
    {
        public LexerException(string message, Position pos) :
            base(message, pos) { }
    }
}