namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay03
    {
        private readonly string[] input = { "00100", "11110", "10110", "10111", "10101", "01111", "00111", "11100", "10000", "11001", "00010", "01010" };

        [TestMethod]
        public void TestPartA()
        {
            const long answerA = 198;

            var day = new Day03(input);
            var a = (long)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            const long answerB = 230;

            var day = new Day03(input);
            var b = (long)day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}