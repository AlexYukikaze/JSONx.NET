using JSONx.Lexers;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestMatcher
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
    }
}