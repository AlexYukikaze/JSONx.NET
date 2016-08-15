using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using JSONx.AST;
using JSONx.Lexers;

namespace JSONx.Parsers
{
    public partial class Parser
    {
        [DebuggerStepThrough]
        protected void Skip(int count = 1)
        {
            _index = Math.Min(_index + count, _tokensCount - 1);
        }

        [DebuggerStepThrough]
        protected void Error(string message, params object[] args)
        {
            var pos = _tokens[_index].Begin;
            var sb = new StringBuilder();
            sb.AppendFormat(message, args);
            sb.AppendFormat(Utils.POSITION_MESSAGE_FORMAT, pos.Index, pos.Row, pos.Column);
            throw new ParserException(sb.ToString(), _tokens[_index]);
        }

        [DebuggerStepThrough]
        protected bool Peek(params TokenType[] types)
        {
            var i = _index;
            foreach (var tokenType in types)
            {
                if (i >= _tokensCount) return false;
                var cur = _tokens[i++];
                if (cur.Type != tokenType) return false;
            }
            return true;
        }

        [DebuggerStepThrough]
        protected bool Consume(TokenType type)
        {
            var cur = _tokens[_index];
            if (cur.Type == type)
            {
                Skip();
                return true;
            }
            return false;
        }

        [DebuggerStepThrough]
        protected Token Ensure(TokenType type, string message, params object[] args)
        {
            var token = _tokens[_index];
            if (token.Type != type)
            {
                Error(message, args);
            }
            Skip();
            return token;
        }


        protected T Ensure<T>(Func<T> getter, string message, params object[] args) where T : PositionEntity
        {
            var result = BindPosition(getter);
            if (result == null)
            {
                Error(message, args);
            }
            return result;
        }

        [DebuggerStepThrough]
        protected T Attempt<T>(Func<T> getter) where T : PositionEntity
        {
            var backup = _index;
            var result = BindPosition(getter);
            if (result == null)
            {
                _index = backup;
            }
            return result;
        }

        [DebuggerStepThrough]
        protected List<T> Attempt<T>(Func<List<T>> getter) where T : PositionEntity
        {
            var backup = _index;
            var result = getter();
            if (result == null || result.Count == 0)
            {
                _index = backup;
            }
            return result;
        }

        [DebuggerStepThrough]
        protected T BindPosition<T>(Func<T> getter) where T : PositionEntity
        {
            var beginIndex = _index;
            var tokenStart = _tokens[_index];
            var result = getter();
            if (result != null)
            {
                result.Begin = tokenStart.Begin;
                var endIndex = _index;
                if (endIndex > beginIndex && endIndex > 0)
                {
                    var tokenEnd = _tokens[endIndex - 1];
                    result.End = tokenEnd.End;
                }
            }
            return result;
        }
    }
}