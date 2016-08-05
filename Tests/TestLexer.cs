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
            var source = "";
            var tokenizer = new Tokenizer(source);
            Assert.AreEqual(tokenizer.Position, new PositionEntry(0, 1, 1));
        }

        [Test]
        public void TokenizerPeek()
        {
            var source = "foo";
            var tokenizer = new Tokenizer(source);
            Assert.AreEqual('f', tokenizer.Peek());
            Assert.AreEqual('o', tokenizer.Peek(1));
            Assert.AreEqual('o', tokenizer.Peek(2));
            Assert.AreEqual(default(char), tokenizer.Peek(3));
        }

        [Test]
        public void TokenizerConsume()
        {
            var source = "foo";
            var tokenizer = new Tokenizer(source);
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
            var source = "f\nb";
            var tokenizer = new Tokenizer(source);
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
            var source = "foo";
            var tokenizer = new Tokenizer(source);
            tokenizer.TakeSnapshot();
            tokenizer.Consume();
            Assert.AreEqual(new PositionEntry(1, 1, 2), tokenizer.Position);
            tokenizer.RollbackSnapshot();
            Assert.AreEqual(new PositionEntry(0, 1, 1), tokenizer.Position);
        }

        [Test]
        public void TokenizerEnd()
        {
            var source = "";
            var tokenizer = new Tokenizer(source);
            Assert.True(tokenizer.End(), "End of file expected");
        }

        [Test]
        public void MatchWhitespace()
        {
            var tokenizer = new Tokenizer(" \t\r\n ");
            var matcher = new WhitespaceMatcher();
            var token = matcher.Match(tokenizer);
            Assert.AreEqual(TokenType.Whitespace, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(5, 2, 2), token.Span.End);
        }

        [Test]
        public void MatchMultilineComment()
        {
            var tokenizer = new Tokenizer("/*\n*/");
            var matcher = new MultilineCommentMatcher();
            var token = matcher.Match(tokenizer);
            Assert.AreEqual(TokenType.MultilineComment, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(5, 2, 3), token.Span.End);
        }

        [Test]
        public void MatchSinglelineComment()
        {
            var tokenizer = new Tokenizer("// comment\n");
            var matcher = new SinglelineCommentMatcher();
            var token = matcher.Match(tokenizer);
            Assert.AreEqual(TokenType.SinglelineComment, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(11, 2, 1), token.Span.End);

            tokenizer = new Tokenizer("// comment");
            matcher = new SinglelineCommentMatcher();
            token = matcher.Match(tokenizer);
            Assert.AreEqual(TokenType.SinglelineComment, token.Type);
            Assert.AreEqual(new PositionEntry(0, 1, 1), token.Span.Begin);
            Assert.AreEqual(new PositionEntry(10, 1, 11), token.Span.End);
        }
    }
}