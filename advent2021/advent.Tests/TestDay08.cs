namespace advent.Tests
{
    [TestClass]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class TestDay08
    {
        private readonly string[] input = { "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe", "edbfga begcd cbg gc gcadebf fbgde acbgfdabcde gfcbed gfec | fcgedb cgb dgebacf gc", "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg", "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb", "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegbdgceab fcbdga | gecf egdcabf bgf bfgea", "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb", "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe", "bdfegc cbegaf gecbf dfcage bdacg ed bedf cedadcbefg gebcd | ed bcgafe cdgba cbgef", "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb", "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce" };

        [TestMethod]
        public void TestPartA()
        {
            const long answerA = 26;

            var day = new Day08(input);
            var a = (long)day.PartA();

            Assert.AreEqual(answerA, a);
        }

        [TestMethod]
        public void TestPartB()
        {
            const long answerB = 61229;

            var day = new Day08(input);
            var b = (long)day.PartB();

            Assert.AreEqual(answerB, b);
        }
    }
}