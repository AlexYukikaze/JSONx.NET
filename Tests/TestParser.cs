using System;
using System.Collections.Generic;
using JSONx.Lexers;
using JSONx.Parsers;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestParser : Parser
    {
        public TestParser() :
            base(new List<Token>{new Token(TokenType.EOF)}) { }

        public TestParser(List<Token> tokens) :
            base(tokens) { }

        [Test]
        public void ParserConstructor()
        {
            Assert.Throws<ArgumentException>(() => new TestParser(new List<Token>()));
            Assert.Throws<ArgumentNullException>(() => new TestParser(null));
        }

        [Test]
        public void ParserSkip()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number),
                new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Assert.AreEqual(0, parser._index);
            parser.Skip();
            Assert.AreEqual(1, parser._index);
            parser.Skip();
            Assert.AreEqual(1, parser._index);
        }

        [Test]
        public void ParserPeek()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number),
                new Token(TokenType.Comma),
                new Token(TokenType.Number)
            };
            var parser = new TestParser(tokens);
            Assert.True(parser.Peek(TokenType.Number, TokenType.Comma, TokenType.Number));
            Assert.False(parser.Peek(TokenType.Number, TokenType.Number));
        }

        [Test]
        public void ParserError()
        {
            var tokens = new List<Token>{ new Token(TokenType.EOF) };
            var parser = new TestParser(tokens);
            Assert.Throws<ParserException>(() => parser.Error("UNIT_TEST"));
        }

        [Test]
        public void ParserConsume()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number),
                new Token(TokenType.Comma),
                new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Assert.AreEqual(0, parser._index);
            Assert.True(parser.Consume(TokenType.Number));
            Assert.AreEqual(1, parser._index);
            Assert.True(parser.Consume(TokenType.Comma));
            Assert.AreEqual(2, parser._index);
            Assert.False(parser.Consume(TokenType.Number));
        }

        [Test]
        public void ParserEnsure()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number),
                new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Assert.AreEqual(0, parser._index);
            Assert.AreEqual(TokenType.Number, parser.Ensure(TokenType.Number, "Number expected").Type);
            Assert.AreEqual(1, parser._index);
            Assert.Throws<ParserException>(() => parser.Ensure(TokenType.Number, "Number expected"));
        }

        [Test]
        public void ParsesEnsureT()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number), new Token(TokenType.Comma), new Token(TokenType.Number),
                new Token(TokenType.Number), new Token(TokenType.Comma), new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Func<Token> getter = () => parser.Consume(TokenType.Number) &&
                                       parser.Consume(TokenType.Comma) &&
                                       parser.Consume(TokenType.Number) ? new Token(TokenType.Null) : null;

            var result = parser.Ensure(getter, "List of numbers expected");
            Assert.NotNull(result);
            Assert.AreEqual(TokenType.Null, result.Type);
            Assert.AreEqual(3, parser._index);
            Assert.Throws<ParserException>(() => parser.Ensure(getter, "List of numbers expected"));
        }
    }
}