namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay05
    {
        private readonly string[] input = { "0,9 -> 5,9", "8,0 -> 0,8", "9,4 -> 3,4", "2,2 -> 2,1", "7,0 -> 7,4", "6,4 -> 2,0", "0,9 -> 2,9", "3,4 -> 1,4", "0,0 -> 8,8", "5,5 -> 8,2" };

        [TestMethod]
        public void TestPartA()
        {
            const long answerA = 5;

            var day = new Day05(input);
            var a = (long)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            const long answerB = 12;

            var day = new Day05(input);
            var b = (long)day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}