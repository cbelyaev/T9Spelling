using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using T9Spelling;

namespace Tests
{
    [TestClass]
    public class SpellerTests
    {
        [TestMethod]
        public void TestNullInput()
        {
            var speller = new Speller();
            Assert.ThrowsException<ArgumentException>(() => speller.Spell((string)null));
        }

        [TestMethod]
        public void TestNullInputs()
        {
            var speller = new Speller();
            Assert.ThrowsException<ArgumentException>(() => speller.Spell((string[])null));
        }

        [TestMethod]
        public void TestEmptyInput()
        {
            var speller = new Speller();
            Assert.ThrowsException<ArgumentException>(() => speller.Spell(string.Empty));
        }

        [TestMethod]
        public void TestEmptyInputs()
        {
            var speller = new Speller();
            Assert.ThrowsException<ArgumentException>(() => speller.Spell(new string[0]));
        }

        [TestMethod]
        public void TestInvalidChar()
        {
            var speller = new Speller();
            Assert.ThrowsException<InvalidCharException>(() => speller.Spell("-"));
        }

        [TestMethod]
        public void TestInvalidCase()
        {
            var speller = new Speller();
            Assert.ThrowsException<InvalidCaseException>(() => speller.Spell(new []{"-"}));
        }

        [TestMethod]
        public void TestA()
        {
            var output = new Speller().Spell("a");
            Assert.AreEqual("2", output);
        }

        [TestMethod]
        public void TestHelloWorld()
        {
            var output = new Speller().Spell("hello world");
            Assert.AreEqual("4433555 555666096667775553", output);
        }

        [TestMethod]
        public void TestCases()
        {
            var output = new Speller().Spell(new []{"hello world", "foo  bar"});
            Assert.AreEqual(2, output.Length);
            Assert.AreEqual("4433555 555666096667775553", output[0]);
            Assert.AreEqual("333666 6660 022 2777", output[1]);
        }

        [TestMethod]
        public void TestPause()
        {
            var output = new Speller().Spell("aa");
            Assert.AreEqual("2 2", output);
        }
    }
}
