using System;

namespace JSONx.Lexers
{
    public class MultilineCommentMatcher : Matcher
    {
        protected override Token MatchToken(Tokenizer tokenizer)
        {
            if (tokenizer.Current != '/' || tokenizer.Peek(1) != '*')
                return null;

            tokenizer.Consume(2);
            while (!tokenizer.End())
            {
                if (tokenizer.Current == '*' && tokenizer.Peek(1) == '/')
                {
                    tokenizer.Consume(2);
                    return new Token(TokenType.MultilineComment);
                }

                if (tokenizer.Current == '\n')
                {
                    tokenizer.NewLine();
                }
                else
                {
                    tokenizer.Consume();
                }
            }
            throw new MatcherException("Comment not closed", tokenizer.PeekSnapshot());
        }
    }
}