using System.Linq;
using System.Xml;
using JSONx.Lexers;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestLexer
    {
        [Test]
        public void TokenEquality()
        {
            var span = new TokenSpan();
            var a = new Token(TokenType.EOF, span);
            var b = new Token(TokenType.EOF, span);
            var c = new Token(TokenType.Number, span);
            Assert.AreEqual(a, b);
            Assert.AreNotEqual(a, c);
        }

        [Test]
        public void TokenizerPosition()
        {
            var tokenizer = new Tokenizer(" ");
            Assert.AreEqual(tokenizer.Position, new PositionEntry(0, 1, 1));
            tokenizer.Consume();
            Assert.AreEqual(tokenizer.Position, new PositionEntry(1, 1, 2));
        }

        [Test]
        public void TokenizerPeek()
        {
            var tokenizer = new Tokenizer("foo");
            Assert.AreEqual('f', tokenizer.Peek());
            Assert.AreEqual('o', tokenizer.Peek(1));
            Assert.AreEqual('o', tokenizer.Peek(2));
            Assert.AreEqual(default(char), tokenizer.Peek(3));
        }

        [Test]
        public void TokenizerConsume()
        {
            var tokenizer = new Tokenizer("foo");
            Assert.AreEqual('f', tokenizer.Current);
            Assert.AreEqual(new PositionEntry(0, 1, 1), tokenizer.Position);
            tokenizer.Consume();
            Assert.AreEqual('o', tokenizer.Current);
            Assert.AreEqual(new PositionEntry(1, 1, 2), tokenizer.Position);
            tokenizer.Consume();
            Assert.AreEqual('o', tokenizer.Current);
            Assert.AreEqual(new PositionEntry(2, 1, 3), tokenizer.Position);
            tokenizer.Consume();
            Assert.AreEqual(default(char), tokenizer.Current);
        }

        [Test]
        public void TokenizerNewLine()
        {
            var tokenizer = new Tokenizer("f\nb");
            Assert.AreEqual(new PositionEntry(0, 1, 1), tokenizer.Position);
            tokenizer.Consume();
            Assert.AreEqual(new PositionEntry(1, 1, 2), tokenizer.Position);
            tokenizer.NewLine();
            Assert.AreEqual(new PositionEntry(2, 2, 1), tokenizer.Position);
            tokenizer.Consume();
            Assert.AreEqual(new PositionEntry(3, 2, 2), tokenizer.Position);
        }

        [Test]
        public void TokenizerSnapshots()
        {
            var tokenizer = new Tokenizer("foo");
            tokenizer.TakeSnapshot();
            tokenizer.Consume();
            Assert.AreEqual(new PositionEntry(1, 1, 2), tokenizer.Position);
            tokenizer.RollbackSnapshot();
            Assert.AreEqual(new PositionEntry(0, 1, 1), tokenizer.Position);
        }

        [Test]
        public void TokenizerEnd()
        {
            var tokenizer = new Tokenizer("");
            Assert.True(tokenizer.End(), "End of file expected");
        }

        [Test]
        public void MatchWhitespace()
        {
            var matcher = new WhitespaceMatcher();
            var token = MatchToken(" \t\r\n ", matcher);
            Assert.AreEqual(TokenType.Whitespace, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(5, 2, 2), token.Span.End);
        }

        [Test]
        public void MatchMultilineComment()
        {
            var matcher = new MultilineCommentMatcher();
            var token = MatchToken("/*\n*/", matcher);
            Assert.AreEqual(TokenType.MultilineComment, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(5, 2, 3), token.Span.End);
        }

        [Test]
        public void MatchSinglelineComment()
        {
            var matcher = new SinglelineCommentMatcher();
            var token = MatchToken("// comment\n", matcher);
            Assert.AreEqual(TokenType.SinglelineComment, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(11, 2, 1), token.Span.End);

            token = MatchToken("// comment", matcher);
            Assert.AreEqual(TokenType.SinglelineComment, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(10, 1, 11), token.Span.End);
        }

        [Test]
        public void MatchString()
        {
            var singleQuoteMatcher = new StringMatcher(StringMatcher.SINGLE_QUOTE);
            var doubleQuoteMatcher = new StringMatcher(StringMatcher.DOUBLE_QUOTE);
            var token = MatchToken("\"example\"", doubleQuoteMatcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(9, 1, 10), token.Span.End);
            Assert.AreEqual("example", token.Value);

            token = MatchToken("'example'", singleQuoteMatcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(9, 1, 10), token.Span.End);
            Assert.AreEqual("example", token.Value);

            token = MatchToken("'\\x41\\u0042\\U00000043\\j'", singleQuoteMatcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(24, 1, 25), token.Span.End);
            Assert.AreEqual("ABC\\j", token.Value);

            token = MatchToken("'\\xZZ\\x1R'", singleQuoteMatcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("\\xZZ\\x1R", token.Value);
        }

        [Test]
        public void MatchNumber()
        {
            var matcher = new NumberMatcher();
            var token = MatchToken("0", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(1, 1, 2), token.Span.End);
            Assert.AreEqual("0", token.Value);

            token = MatchToken("-1", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(2, 1, 3), token.Span.End);
            Assert.AreEqual("-1", token.Value);

            token = MatchToken("123", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(3, 1, 4), token.Span.End);
            Assert.AreEqual("123", token.Value);

            token = MatchToken("3.14", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(4, 1, 5), token.Span.End);
            Assert.AreEqual("3.14", token.Value);

            token = MatchToken("1e1", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(3, 1, 4), token.Span.End);
            Assert.AreEqual("1e1", token.Value);

            token = MatchToken("1.1e1", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.Number, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(5, 1, 6), token.Span.End);
            Assert.AreEqual("1.1e1", token.Value);

            token = MatchToken("1.", matcher);
            Assert.Null(token);

            token = MatchToken(".1", matcher);
            Assert.Null(token);

            token = MatchToken("1.e1", matcher);
            Assert.Null(token);
        }

        [Test]
        public void MatchOperator()
        {
            var matcher = new OperatorMatcher(TokenType.LeftCurlyBracket, "{");
            var token = MatchToken("{{", matcher);
            Assert.NotNull(token);
            Assert.AreEqual(TokenType.LeftCurlyBracket, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(1, 1, 2), token.Span.End);

            token = MatchToken("[", matcher);
            Assert.Null(token);

        }

        private static Token MatchToken(string source, Matcher matcher)
        {
            var tokenizer = new Tokenizer(source);
            return matcher.Match(tokenizer);
        }
    }
}