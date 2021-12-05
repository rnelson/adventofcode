namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay02
    {
        private readonly string[] input = { "forward 5", "down 5", "forward 8", "up 3", "down 8", "forward 2" };

        [TestMethod]
        public void TestPartA()
        {
            const long answerA = 150;

            var day = new Day02(input);
            var a = (long)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            const long answerB = 900;
            
            var day = new Day02(input);
            var b = (long)day.PartB();
            
            Assert.AreEqual(answerB, b);
        }
    }
}