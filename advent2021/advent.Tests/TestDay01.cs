namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay01
    {
        [TestMethod]
        public void TestPartA()
        {
            var input = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            const long answerA = 7;

            var day = new Day01(input.ToStringCollection());
            var a = (int)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            var input = new[] { 199, 200, 208, 210, 200, 207, 240, 269, 260, 263 };
            const long answerB = 5;

            var day = new Day01(input.ToStringCollection());
            var b = (int)day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}