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
            Assert.AreEqual(tokenizer.Position, new PositionEntry(1, 1));
            Assert.AreEqual(tokenizer.Index, 0);
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
            Assert.AreEqual(default(char), '\0');
        }

        [Test]
        public void TokenizerConsume()
        {
            var source = "foo";
            var tokenizer = new Tokenizer(source);
            Assert.AreEqual('f', tokenizer.Current);
            Assert.AreEqual(new PositionEntry(1, 1), tokenizer.Position);
            Assert.AreEqual(tokenizer.Index, 0);
            tokenizer.Consume();
            Assert.AreEqual('o', tokenizer.Current);
            Assert.AreEqual(new PositionEntry(1, 2), tokenizer.Position);
            Assert.AreEqual(tokenizer.Index, 1);
            tokenizer.Consume();
            Assert.AreEqual('o', tokenizer.Current);
            Assert.AreEqual(new PositionEntry(1, 3), tokenizer.Position);
            Assert.AreEqual(tokenizer.Index, 2);
            tokenizer.Consume();
            Assert.AreEqual(default(char), tokenizer.Current);
        }

        [Test]
        public void TokenizerNewLine()
        {
            var source = "f\nb";
            var tokenizer = new Tokenizer(source);
            Assert.AreEqual(tokenizer.Position, new PositionEntry(1, 1));
            Assert.AreEqual(tokenizer.Index, 0);
            tokenizer.Consume();
            Assert.AreEqual(tokenizer.Position, new PositionEntry(1, 2));
            Assert.AreEqual(tokenizer.Index, 1);
            tokenizer.Consume();
            tokenizer.NewLine();
            Assert.AreEqual(tokenizer.Position, new PositionEntry(2, 1));
            Assert.AreEqual(tokenizer.Index, 2);
            tokenizer.Consume();
            Assert.AreEqual(tokenizer.Position, new PositionEntry(2, 2));
            Assert.AreEqual(tokenizer.Index, 3);
        }

        [Test]
        public void TokenizerSnapshots()
        {
            var source = "foo";
            var tokenizer = new Tokenizer(source);
            tokenizer.TakeSnapshot();
            tokenizer.Consume();
            Assert.AreEqual(tokenizer.Position, new PositionEntry(1, 2));
            Assert.AreEqual(tokenizer.Index, 1);
            tokenizer.RollbackSnapshot();
            Assert.AreEqual(tokenizer.Position, new PositionEntry(1, 1));
            Assert.AreEqual(tokenizer.Index, 0);
        }

        [Test]
        public void TokenizerEnd()
        {
            var source = "";
            var tokenizer = new Tokenizer(source);
            Assert.True(tokenizer.End(), "End of file expected");
        }
    }
}