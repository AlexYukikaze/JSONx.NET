using System.Collections.Generic;
using JSONx.Lexers;

namespace JSONx.Parsers
{
    public partial class Parser
    {
        private readonly List<Token> _tokens;
        private readonly int _tokensCount;
        private int _index;


        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
            _tokensCount = tokens.Count;
            _index = 0;
        }
    }
}