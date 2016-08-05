using System;
using System.Globalization;
using System.Text;

namespace JSONx.Lexers
{
    public class StringMatcher : Matcher
    {
        public const char SINGLE_QUOTE = '\'';
        public const char DOUBLE_QUOTE = '"';
        private readonly char QUOTE;

        public StringMatcher(char quoteChar)
        {
            QUOTE = quoteChar;
        }

        protected override Token MatchToken(Tokenizer tokenizer)
        {
            if (tokenizer.Current != QUOTE)
                return null;

            var result = new StringBuilder();
            var begin = tokenizer.Position;

            while (!tokenizer.End())
            {
                tokenizer.Consume();
                if (tokenizer.Current == QUOTE)
                {
                    tokenizer.Consume();
                    return new Token(TokenType.String, new TokenSpan(begin, tokenizer.Position), result.ToString());
                }

                if (tokenizer.Current == '\n')
                {
                    throw new LexerException("Not closed string", begin);
                }

                if (tokenizer.Current == '\\')
                {
                    tokenizer.TakeSnapshot();
                    if (HandleEscapeSequences(tokenizer, result))
                    {
                        tokenizer.CommitSnapshot();
                    }
                    else
                    {
                        tokenizer.RollbackSnapshot();
                    }
                }
                else
                {
                    result.Append(tokenizer.Current);
                }
            }
            throw new LexerException("Not closed string", begin);
        }

        private bool HandleEscapeSequences(Tokenizer tokenizer, StringBuilder sb)
        {
            tokenizer.Consume();
            switch (tokenizer.Current)
            {
                case '\'':
                case '\"':
                case '\\':
                    sb.Append(tokenizer.Current);
                    break;
                case 'a':
                    sb.Append('\a');
                    break;
                case 'b':
                    sb.Append('\b');
                    break;
                case 'f':
                    sb.Append('\f');
                    break;
                case 'n':
                    sb.Append('\n');
                    break;
                case 'r':
                    sb.Append('\r');
                    break;
                case 't':
                    sb.Append('\t');
                    break;
                case 'v':
                    sb.Append('\v');
                    break;

                    // OMG! So ugly...
                case 'x':
                {
                    var result = ParseUnicodeSequence(tokenizer, 2);
                    if (result == null)
                    {
                        sb.Append("\\");
                        return false;
                    }
                    sb.Append(result);
                    break;
                }
                case 'u':
                {
                    var result = ParseUnicodeSequence(tokenizer, 4);
                    if (result == null)
                    {
                        sb.Append("\\");
                        return false;
                    }
                    sb.Append(result);
                    break;
                }
                case 'U':
                {
                    var result = ParseUnicodeSequence(tokenizer, 8);
                    if (result == null)
                    {
                        sb.Append("\\");
                        return false;
                    }
                    sb.Append(result);
                    break;
                }
                default:
                    sb.Append("\\");
                    return false;
            }
            return true;
        }

        private string ParseUnicodeSequence(Tokenizer tokenizer, int length)
        {
            var builder = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                tokenizer.Consume();
                builder.Append(tokenizer.Current);
            }
            int ord;
            if(int.TryParse(builder.ToString(), NumberStyles.AllowHexSpecifier, new NumberFormatInfo(), out ord))
                return char.ConvertFromUtf32(ord);
            return null;
        }
    }
}