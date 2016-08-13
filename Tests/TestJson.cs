using System.Collections.Generic;
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
            var intCopy = (JValue)intValue.Clone();
            var floatValue = new JValue(3.14);

            Assert.AreEqual(JType.Integer, intValue.Type);
            Assert.AreEqual(JType.Integer, intCopy.Type);
            Assert.AreEqual(intValue, intCopy);
            Assert.AreNotEqual(intValue, floatValue);
        }

        [Test]
        public void ListEquality()
        {
            var one = new JList
            {
                new JValue(0), new JValue(true), new JValue("hello")
            };

            var two = new JList
            {
                new JValue(0),
                new JValue(true),
                new JValue("hello")
            };

            var three = new JList
            {
                new JValue(0),
                new JValue(true),
                new JValue("BANG!")
            };

            var four = new JList
            {
                new JValue(0),
                one
            };

            var five = new JList
            {
                new JValue(0),
                two
            };

            var six = new JList
            {
                new JValue(0),
                new JList()
            };

            Assert.AreEqual(one, two);
            Assert.AreEqual(four, five);
            Assert.AreNotEqual(one, three);
            Assert.AreNotEqual(five, six);
        }

        [Test]
        public void ListCloneEquality()
        {
            var value = new JList
            {
                new JValue(0),
                new JList
                {
                    new JValue("value")
                }
            };

            var copy = (JList)value.Clone();
            Assert.AreEqual(JType.List, copy.Type);
            Assert.AreEqual(value, copy);
        }
    }
}