using System;

namespace JSONx.Lexers
{
    public class MatcherException : Exception
    {
        private const string MESSAGE_FORMAT = " at index {0} (line {1}, column {2}).";

        public PositionEntry Position { get; }

        public MatcherException(string message, PositionEntry pos) :
            base(message + string.Format(MESSAGE_FORMAT, pos.Index, pos.Row, pos.Column))
        {
            Position = pos;
        }
    }
}