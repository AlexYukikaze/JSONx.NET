using System;
using System.Collections.Generic;
using JSONx.AST;
using JSONx.Lexers;

namespace JSONx.Parsers
{
    public partial class Parser
    {
        private readonly List<Token> _tokens;
        private readonly int _tokensCount;
        protected int _index;


        public Parser(List<Token> tokens)
        {
            if(tokens == null)
                throw new ArgumentNullException(nameof(tokens));
            if (tokens.Count == 0)
                throw new ArgumentException("List of token can't be empty.", nameof(tokens));

            _tokens = tokens;
            _tokensCount = tokens.Count;
            _index = 0;
        }

        public JSONxNode Parse()
        {
            var value = Ensure(ParseValue, "Value expected");
            Ensure(TokenType.EOF, "End of file expected");
            return value;
        }

        protected JSONxNode ParseValue()
        {
            return Attempt(ParseTrue) ??
                   Attempt(ParseFalse) ??
                   Attempt(ParseNull) ??
                   Attempt(ParseString) ??
                   Attempt(ParseNumber) ??
                   Attempt(ParseList) ??
                   Attempt(ParseObject);
        }

        protected ListNode ParseList()
        {
            throw new NotImplementedException();
        }

        protected JSONxNode ParseObject()
        {
            throw new NotImplementedException();
        }

        protected NumberNode ParseNumber()
        {
            var token = Attempt(TokenType.Number);
            if (token != null)
            {
                decimal value;
                if (!decimal.TryParse(token.Value, out value))
                {
                    Error("Bad number token '{0}'", token.Value);
                }
                return new NumberNode(value);
            }
            return null;
        }

        protected StringNode ParseString()
        {
            var token = Attempt(TokenType.Number);
            if (token != null)
            {
                return new StringNode(token.Value);
            }
            return null;
        }

        protected TrueNode ParseTrue()
        {
            if (Check(TokenType.True))
            {
                return new TrueNode();
            }
            return null;
        }

        protected FalseNode ParseFalse()
        {
            if (Check(TokenType.False))
            {
                return new FalseNode();
            }
            return null;
        }

        protected NullNode ParseNull()
        {
            if (Check(TokenType.Null))
            {
                return new NullNode();
            }
            return null;
        }
    }
}