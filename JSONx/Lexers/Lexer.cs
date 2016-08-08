using System.Collections.Generic;

namespace JSONx.Lexers
{
    public partial class Lexer
    {
        private readonly Tokenizer _tokenizer;

        public Lexer(string source)
        {
            _tokenizer = new Tokenizer(source);
        }

        public Token Next()
        {
        skip:
            foreach (var matcher in IgnoredMatches)
            {
                var token = matcher.Match(_tokenizer);
                if (token != null)
                {
                    goto skip;
                }
            }

            foreach (var matcher in Matchers)
            {
                var token = matcher.Match(_tokenizer);
                if (token != null)
                {
                    return token;
                }
            }

            if (_tokenizer.End())
            {
                var pos = _tokenizer.Position;
                return new Token(TokenType.EOF, new TokenSpan(pos, pos));
            }

            throw new LexerException("Unknown token", _tokenizer.Position);
        }

        public IEnumerable<Token> Tokenize()
        {
            Token current;
            do
            {
                current = Next();
                yield return current;
            } while (current.Type != TokenType.EOF);
        }
    }
}