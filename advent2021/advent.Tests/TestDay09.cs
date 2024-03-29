﻿namespace advent.Tests
{
	[TestClass]
	[SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
	public class TestDay09
	{
		private readonly string[] input = { "" };

		[TestMethod]
		public void TestPartA()
		{
			const long answerA = 0;

			var day = new Day09(input);
			var a = (long)day.PartA();

			Assert.AreEqual(answerA, a);
		}

		[TestMethod]
		public void TestPartB()
		{
			const long answerB = 0;

			var day = new Day09(input);
			var b = (long)day.PartB();

			Assert.AreEqual(answerB, b);
		}
	}
}