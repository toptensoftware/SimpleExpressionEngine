using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleExpressionEngine;

namespace UnitTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void TokenizerTest()
        {
            var testString = "10 + 20 - 30.123";
            var t = new Tokenizer(new StringReader(testString));

            // "10"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 10);
            t.NextToken();

            // "+"
            Assert.AreEqual(t.Token, Token.Add);
            t.NextToken();

            // "20"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 20);
            t.NextToken();

            // "-"
            Assert.AreEqual(t.Token, Token.Subtract);
            t.NextToken();

            // "30.123"
            Assert.AreEqual(t.Token, Token.Number);
            Assert.AreEqual(t.Number, 30.123);
            t.NextToken();
        }

        [TestMethod]
        public void AddSubtractTest()
        {
            // Add 
            Assert.AreEqual(Parser.Parse("10 + 20").Eval(), 30);

            // Subtract 
            Assert.AreEqual(Parser.Parse("10 - 20").Eval(), -10);

            // Sequence
            Assert.AreEqual(Parser.Parse("10 + 20 - 40 + 100").Eval(), 90);
        }

        [TestMethod]
        public void UnaryTest()
        {
            // Negative
            Assert.AreEqual(Parser.Parse("-10").Eval(), -10);

            // Positive
            Assert.AreEqual(Parser.Parse("+10").Eval(), 10);

            // Negative of a negative
            Assert.AreEqual(Parser.Parse("--10").Eval(), 10);

            // Woah!
            Assert.AreEqual(Parser.Parse("--++-+-10").Eval(), 10);

            // All together now
            Assert.AreEqual(Parser.Parse("10 + -20 - +30").Eval(), -40);
        }
    }
}
