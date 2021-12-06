namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay06
    {
        private readonly string[] input = { "3,4,3,1,2", "" };

        [TestMethod]
        public void TestPartA()
        {
            const long answerA = 5934;

            var day = new Day06(input);
            var a = (long)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            const long answerB = 26984457539;

            var day = new Day06(input);
            var b = (long)day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}