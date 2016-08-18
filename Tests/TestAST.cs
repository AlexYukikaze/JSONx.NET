using System.Collections.Generic;
using System.Linq;
using JSONx.AST;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class TestAST
    {
        [Test]
        public void ScalarTypeNodes()
        {
            var stringNode = new StringNode("value");
            var numberNode = new NumberNode(3.14);
            var trueNode = new TrueNode();
            var falseNode = new FalseNode();
            var nullNode = new NullNode();
            var keyNode = new KeyNode("key");

            Assert.AreEqual("value", stringNode.Value);
            Assert.AreEqual(NodeType.String, stringNode.Type);
            Assert.Null(stringNode.Parent);
            Assert.False(stringNode.HasChildren);

            Assert.AreEqual(3.14, numberNode.Value);
            Assert.AreEqual(NodeType.Number, numberNode.Type);
            Assert.Null(numberNode.Parent);
            Assert.False(numberNode.HasChildren);

            Assert.AreEqual("key", keyNode.Value);
            Assert.AreEqual(NodeType.Key, keyNode.Type);
            Assert.Null(keyNode.Parent);
            Assert.False(keyNode.HasChildren);

            Assert.AreEqual(NodeType.Boolean, trueNode.Type);
            Assert.Null(trueNode.Parent);
            Assert.False(trueNode.HasChildren);

            Assert.AreEqual(NodeType.Boolean, falseNode.Type);
            Assert.Null(falseNode.Parent);
            Assert.False(falseNode.HasChildren);

            Assert.AreEqual(NodeType.Null, nullNode.Type);
            Assert.Null(nullNode.Parent);
            Assert.False(nullNode.HasChildren);
        }

        [Test]
        public void ListNode()
        {
            var emptyList = new ListNode();
            var notEmptyList = new ListNode(new List<JSONxNode>{ new NumberNode(1), new StringNode("value") });

            Assert.AreEqual(NodeType.List, emptyList.Type);
            Assert.False(emptyList.HasChildren);
            Assert.Null(emptyList.Parent);

            Assert.AreEqual(NodeType.List, notEmptyList.Type);
            Assert.True(notEmptyList.HasChildren);
            Assert.Null(notEmptyList.Parent);
            Assert.AreEqual(2, notEmptyList.Children.Count());
            foreach (var child in notEmptyList.Children)
            {
                Assert.AreSame(notEmptyList, child.Parent);
            }
        }

        [Test]
        public void PropertyNode()
        {
            var propertyNode = new PropertyNode(new KeyNode("key"), new StringNode("value"));
            Assert.AreEqual(NodeType.Property, propertyNode.Type);
            Assert.True(propertyNode.HasChildren);
            Assert.Null(propertyNode.Parent);
            Assert.AreEqual(2, propertyNode.Children.Count());
            foreach (var child in propertyNode.Children)
            {
                Assert.AreSame(propertyNode, child.Parent);
            }
        }

        [Test]
        public void ObjectNode()
        {
            var props = new List<PropertyNode>{ new PropertyNode(new KeyNode("key"), new StringNode("value")) };
            var objectNode = new ObjectNode(props);
            Assert.AreEqual(NodeType.Object, objectNode.Type);
            Assert.True(objectNode.HasChildren);
            Assert.Null(objectNode.Parent);
            Assert.AreEqual(1, objectNode.Children.Count());
            foreach (var child in objectNode.Children)
            {
                Assert.AreEqual(NodeType.Property, child.Type);
                Assert.AreSame(objectNode, child.Parent);
            }
        }
    }
}