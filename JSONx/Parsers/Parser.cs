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
            var value = Ensure(ParseValue, "Expected <value> got {0}", _tokens[_index]);
            Ensure(TokenType.EOF, "Expected <EOF> got {0}", _tokens[_index]);
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

        protected JSONxNode ParseObject()
        {
            if (!Check(TokenType.LeftCurlyBracket)) return null;
            var props = Attempt(ParsePropertyList);
            Ensure(TokenType.RightCurlyBracket, "Expected '}}' got {0}", _tokens[_index].Type);
            return new ObjectNode(props);
        }

        private List<PropertyNode> ParsePropertyList()
        {
            var result = new List<PropertyNode>();
            var value = ParseProperty();
            while (value != null)
            {
                result.Add(value);
                if (!Check(TokenType.Comma))
                {
                    break;
                }
                value = Ensure(ParseProperty, "Expected <key> : <value> pair got {0}", _tokens[_index]);
            }
            return result;
        }

        private PropertyNode ParseProperty()
        {
            var key = Attempt(TokenType.String);
            if (key == null) return null;
            Ensure(TokenType.Colon, "Expected ':' got {0}", _tokens[_index]);
            var value = Ensure(ParseValue, "Expected <value> got {0}", _tokens[_index]);
            return new PropertyNode(new KeyNode(key.Value), value);
        }

        protected ListNode ParseList()
        {
            if (!Check(TokenType.LeftSquareBracket)) return null;
            var elements = Attempt(ParseListElements);
            Ensure(TokenType.RightSquareBracket, "Expected ']' got {0}", _tokens[_index].Type);
            return new ListNode(elements);
        }

        private List<JSONxNode> ParseListElements()
        {
            var result = new List<JSONxNode>();
            var value = ParseValue();
            while (value != null)
            {
                result.Add(value);
                if (!Check(TokenType.Comma))
                {
                    break;
                }
                value = Ensure(ParseValue, "Expected <value> got {0}", _tokens[_index]);
            }
            return result;
        }

        protected NumberNode ParseNumber()
        {
            var token = Attempt(TokenType.Number);
            if (token != null)
            {
                double value;
                if (!double.TryParse(token.Value, out value))
                {
                    Error("Bad number token '{0}'", token.Value);
                }
                return new NumberNode(value);
            }
            return null;
        }

        protected StringNode ParseString()
        {
            var token = Attempt(TokenType.String);
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