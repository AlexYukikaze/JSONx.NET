using System.Linq;
using JSONx.JSON;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestJson
    {
        [Test]
        public void TestJsonValue()
        {
            JNode jNumber = new JValue(0);
            JNode jBoolean = new JValue(true);
            JNode jString = new JValue("value");
            JNode jNull = new JValue();

            Assert.AreEqual(new JValue(0), jNumber);
            Assert.AreEqual(new JValue(true), jBoolean);
            Assert.AreEqual(new JValue("value"), jString);
            Assert.AreEqual(JNode.Null, jNull);
        }

        [Test]
        public void TestJsonList()
        {
            JNode json = new JList(0, "value", true, JNode.Null);
            Assert.AreEqual(new JValue(0), json[0]);
            Assert.AreEqual(new JValue("value"), json[1]);
            Assert.AreEqual(new JValue(true), json[2]);
            Assert.AreEqual(JNode.Null, json[3]);

            Assert.AreEqual((JValue) 0, json[0]);
            Assert.AreEqual((JValue) "value", json[1]);
            Assert.AreEqual((JValue) true, json[2]);
            Assert.AreEqual(JNode.Null, json[3]);

            Assert.AreEqual(new JList(0, "value", true, JNode.Null), json);
            Assert.AreNotEqual(new JList(0, "value", true), json);

            foreach (var item in json)
            {
                Assert.IsInstanceOf<JValue>(item);
            }
        }

        [Test]
        public void TestJsonObject()
        {
            JNode json = new JObject(new JProperty("key", "value"), new JProperty("number", 123));
            Assert.AreEqual(new JValue(123), json["number"]);
            Assert.AreEqual(new JValue("value"), json["key"]);

            foreach (var item in json)
            {
                Assert.IsInstanceOf<JProperty>(item);
            }
        }

        [Test]
        public void TestNestedJson()
        {
            JNode json = new JObject
            {
                {
                    "list", new JList
                    {
                        0,
                        new JObject
                        {
                            {"inner", 1}
                        }
                    }
                },
                new JProperty("key", "value")
            };

            Assert.IsTrue(json["list"].HasChildren);
            Assert.AreEqual(2, json["list"].Count());
            Assert.AreEqual(new JValue(0), json["list"][0]);
            Assert.AreEqual(new JValue(1), json["list"][1]["inner"]);
            Assert.AreEqual(new JValue("value"), json["key"]);
        }

    }
}