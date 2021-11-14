using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using advent.Solutions;
using advent.Util.Collections;

namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay01
    {
        [TestMethod]
        public void TestPartA()
        {
            var input = new[] { 1721, 979, 366, 299, 675, 1456 };
            const long answerA = 514579;

            var day = new Day01(input.ToStringCollection());
            var a = day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            var input = new[] { 1721, 979, 366, 299, 675, 1456 };
            const long answerB = 241861950;

            var day = new Day01(input.ToStringCollection());
            var b = day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}