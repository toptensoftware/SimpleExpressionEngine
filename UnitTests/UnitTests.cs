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
            Assert.AreEqual(Parser.Parse("10 + 20").Eval(null), 30);

            // Subtract 
            Assert.AreEqual(Parser.Parse("10 - 20").Eval(null), -10);

            // Sequence
            Assert.AreEqual(Parser.Parse("10 + 20 - 40 + 100").Eval(null), 90);
        }

        [TestMethod]
        public void UnaryTest()
        {
            // Negative
            Assert.AreEqual(Parser.Parse("-10").Eval(null), -10);

            // Positive
            Assert.AreEqual(Parser.Parse("+10").Eval(null), 10);

            // Negative of a negative
            Assert.AreEqual(Parser.Parse("--10").Eval(null), 10);

            // Woah!
            Assert.AreEqual(Parser.Parse("--++-+-10").Eval(null), 10);

            // All together now
            Assert.AreEqual(Parser.Parse("10 + -20 - +30").Eval(null), -40);
        }

        [TestMethod]
        public void MultiplyDivideTest()
        {
            // Add 
            Assert.AreEqual(Parser.Parse("10 * 20").Eval(null), 200);

            // Subtract 
            Assert.AreEqual(Parser.Parse("10 / 20").Eval(null), 0.5);

            // Sequence
            Assert.AreEqual(Parser.Parse("10 * 20 / 50").Eval(null), 4);
        }

        [TestMethod]
        public void OrderOfOperation()
        {
            // No parens
            Assert.AreEqual(Parser.Parse("10 + 20 * 30").Eval(null), 610);

            // Parens
            Assert.AreEqual(Parser.Parse("(10 + 20) * 30").Eval(null), 900);

            // Parens and negative
            Assert.AreEqual(Parser.Parse("-(10 + 20) * 30").Eval(null), -900);

            // Nested
            Assert.AreEqual(Parser.Parse("-((10 + 20) * 5) * 30").Eval(null), -4500);
        }

        class MyContext : IContext
        {
            public MyContext(double r)
            {
                _r = r;
            }

            double _r;

            public double ResolveVariable(string name)
            {
                switch (name)
                {
                    case "pi": return Math.PI;
                    case "r": return _r;
                }

                throw new InvalidDataException($"Unknown variable: '{name}'");
            }

            public double CallFunction(string name, double[] arguments)
            {
                throw new NotImplementedException();
            }
        }

        [TestMethod]
        public void Variables()
        {
            var ctx = new MyContext(10);

            Assert.AreEqual(Parser.Parse("2 * pi * r").Eval(ctx), 2 * Math.PI * 10);
        }

        class MyFunctionContext : IContext
        {
            public MyFunctionContext()
            {
            }

            public double ResolveVariable(string name)
            {
                throw new InvalidDataException($"Unknown variable: '{name}'");
            }

            public double CallFunction(string name, double[] arguments)
            {
                if (name == "rectArea")
                {
                    return arguments[0] * arguments[1];
                }

                if (name == "rectPerimeter")
                {
                    return (arguments[0] + arguments[1]) * 2;
                }

                throw new InvalidDataException($"Unknown function: '{name}'");
            }
        }

        [TestMethod]
        public void Functions()
        {
            var ctx = new MyFunctionContext();
            Assert.AreEqual(Parser.Parse("rectArea(10,20)").Eval(ctx), 200);
            Assert.AreEqual(Parser.Parse("rectPerimeter(10,20)").Eval(ctx), 60);
        }

        class MyLibrary
        {
            public MyLibrary()
            {
                pi = Math.PI;
            }

            public double pi { get; private set; }
            public double r { get; set; }

            public double rectArea(double width, double height)
            {
                return width * height;
            }

            public double rectPerimeter(double width, double height)
            {
                return (width + height) * 2;
            }
        }

        [TestMethod]
        public void Reflection()
        {
            // Create a library of helper function
            var lib = new MyLibrary();
            lib.r = 10;

            // Create a context that uses the library
            var ctx = new ReflectionContext(lib);

            // Test
            Assert.AreEqual(Parser.Parse("rectArea(10,20)").Eval(ctx), 200);
            Assert.AreEqual(Parser.Parse("rectPerimeter(10,20)").Eval(ctx), 60);
            Assert.AreEqual(Parser.Parse("2 * pi * r").Eval(ctx), 2 * Math.PI * 10);
        }

    }
}