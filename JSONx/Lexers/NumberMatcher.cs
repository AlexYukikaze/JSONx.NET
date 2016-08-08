using System;
using System.Text;

namespace JSONx.Lexers
{
    public class NumberMatcher : Matcher
    {
        protected override Token MatchToken(Tokenizer tokenizer)
        {
            if (!char.IsNumber(tokenizer.Current) && tokenizer.Current != '+' && tokenizer.Current != '-')
                return null;

            if (tokenizer.Current == '0')
            {
                tokenizer.Consume();
                return new Token(TokenType.Number, "0");
            }

            var result = new StringBuilder();

            try
            {
                var sign = ParseSign(tokenizer);
                var integer = ParseInteger(tokenizer, true);
                var floating = ParseFloating(tokenizer);
                var exponent = ParseExponent(tokenizer);

                result.Append(sign);
                result.Append(integer);
                if (!string.IsNullOrEmpty(floating))
                {
                    result.Append("." + floating);
                }
                if (!string.IsNullOrEmpty(exponent))
                {
                    result.Append("e" + exponent);
                }
            }
            catch (Exception)
            {
                return null;
            }
            return new Token(TokenType.Number, result.ToString());
        }

        private static string ParseSign(Tokenizer tokenizer)
        {
            var current = tokenizer.Current;
            if (current == '+' || current == '-')
            {
                tokenizer.Consume();
                return current.ToString();
            }
            return string.Empty;
        }

        private static string ParseInteger(Tokenizer tokenizer, bool ensure = false)
        {
            var result = new StringBuilder();
            while (!tokenizer.End())
            {
                var current = tokenizer.Current;
                if (!char.IsDigit(current))
                    break;
                result.Append(current);
                tokenizer.Consume();
            }
            if(!ensure || result.Length > 0)
                return result.ToString();
            throw new NumberMatchException();
        }

        private static string ParseFloating(Tokenizer tokenizer)
        {
            var current = tokenizer.Current;
            if (current != '.')
                return string.Empty;
            tokenizer.Consume();
            var floating = ParseInteger(tokenizer);
            if (floating.Length > 0)
                return floating;
            throw new NumberMatchException();
        }

        private static string ParseExponent(Tokenizer tokenizer)
        {
            var current = tokenizer.Current;
            if (current != 'e' && current != 'E')
                return string.Empty;
            tokenizer.Consume();
            var sign = ParseSign(tokenizer);
            var exp = ParseInteger(tokenizer);
            return string.Concat(sign, exp);
        }

        private class NumberMatchException : Exception {}
    }
}