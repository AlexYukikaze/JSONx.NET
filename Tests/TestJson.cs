using JSONx.JSON;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestJson
    {
        [Test]
        public void ValueEquality()
        {
            var intValue = new JValue(3);
            var intCopy = intValue.Clone();
            var floatValue = new JValue(3.14);

            Assert.AreEqual(intValue, intCopy);
            Assert.AreNotEqual(intValue, floatValue);
        }
    }
}