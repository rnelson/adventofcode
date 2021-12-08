namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay07
    {
        private readonly string[] input = { "16,1,2,0,4,2,7,1,2,14", "" };

        [TestMethod]
        public void TestPartA()
        {
            const long answerA = 37;

            var day = new Day07(input);
            var a = (long)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            const long answerB = 168;

            var day = new Day07(input);
            var b = (long)day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}