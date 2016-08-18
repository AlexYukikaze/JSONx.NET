using System;
using System.Collections.Generic;
using JSONx.AST;
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
        public void ParserCheck()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number),
                new Token(TokenType.Comma),
                new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Assert.AreEqual(0, parser._index);
            Assert.True(parser.Check(TokenType.Number));
            Assert.AreEqual(1, parser._index);
            Assert.True(parser.Check(TokenType.Comma));
            Assert.AreEqual(2, parser._index);
            Assert.False(parser.Check(TokenType.Number));
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
        public void ParserEnsureT()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number), new Token(TokenType.Comma), new Token(TokenType.Number),
                new Token(TokenType.Number), new Token(TokenType.Comma), new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Func<Token> getter = () => parser.Check(TokenType.Number) &&
                                       parser.Check(TokenType.Comma) &&
                                       parser.Check(TokenType.Number) ? new Token(TokenType.Null) : null;

            var result = parser.Ensure(getter, "List of numbers expected");
            Assert.NotNull(result);
            Assert.AreEqual(TokenType.Null, result.Type);
            Assert.AreEqual(3, parser._index);
            Assert.Throws<ParserException>(() => parser.Ensure(getter, "List of numbers expected"));
        }

        public void ParserAttempt()
        {
            var tokens = new List<Token> {
                new Token(TokenType.Number), new Token(TokenType.Comma), new Token(TokenType.Number),
                new Token(TokenType.Number), new Token(TokenType.Comma), new Token(TokenType.EOF)
            };

            var parser = new TestParser(tokens);
            Func<Token> getter = () => parser.Check(TokenType.Number) &&
                                       parser.Check(TokenType.Comma) &&
                                       parser.Check(TokenType.Number) ? new Token(TokenType.Null) : null;

            var result = parser.Attempt(getter);
            Assert.NotNull(result);
            Assert.AreEqual(TokenType.Null, result.Type);
            Assert.AreEqual(3, parser._index);
            Assert.Null(parser.Attempt(getter));
            Assert.AreEqual(3, parser._index);
        }

        [Test]
        public void ParserListParse()
        {
            var tokens = new List<Token> {
                new Token(TokenType.LeftSquareBracket),
                new Token(TokenType.Number, "0"),
                new Token(TokenType.Comma),
                new Token(TokenType.Number, "1"),
                new Token(TokenType.Comma),
                new Token(TokenType.Number, "2"),
                new Token(TokenType.RightSquareBracket)
            };
            var parser = new TestParser(tokens);
            var listNode = parser.ParseList();
            Assert.NotNull(listNode);
            Assert.True(listNode.HasChildren);
            var expected = new List<JSONxNode> {
                new NumberNode(0),
                new NumberNode(1),
                new NumberNode(2)
            };
            Assert.That(listNode.Children, Is.EquivalentTo(expected));
        }

        [Test]
        public void ParserListParseThrows()
        {
            var tokens = new List<Token> {
                new Token(TokenType.LeftSquareBracket),
                new Token(TokenType.Number, "0"),
                new Token(TokenType.Comma),
                new Token(TokenType.Number, "1"),
                new Token(TokenType.Comma),
                new Token(TokenType.RightSquareBracket)
            };
            var parser = new TestParser(tokens);
            Assert.Throws<ParserException>(() => parser.ParseList());

            tokens = new List<Token>
            {
                new Token(TokenType.LeftSquareBracket),
                new Token(TokenType.Number, "0"),
                new Token(TokenType.Comma),
                new Token(TokenType.Number, "1"),
                new Token(TokenType.Comma),
                new Token(TokenType.Number, "2"),
                new Token(TokenType.EOF)
            };
            parser = new TestParser(tokens);
            Assert.Throws<ParserException>(() => parser.ParseList());
        }
    }
}