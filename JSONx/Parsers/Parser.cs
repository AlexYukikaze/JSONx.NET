using System;
using System.Collections.Generic;
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
    }
}