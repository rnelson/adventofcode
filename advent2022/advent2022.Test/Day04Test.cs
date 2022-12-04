using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day04Test
{
	private readonly IDay sample = new Day04(); 
	private readonly IDay real = new Day04();
	private object? sampleA;
	private object? sampleB;
	private object? realA;
	private object? realB;

	[TestInitialize]
	public void Setup()
	{
		#region Load sample and real input data
		var sampleInput = new[]
		{
			"2-4,6-8",
			"2-3,4-5",
			"5-7,7-9",
			"2-8,3-7",
			"6-6,4-6",
			"2-6,4-8"
		};
		sample.Input = sampleInput;

		var realInput = real.LoadInput();
		real.Input = realInput;
		#endregion Load sample and real input data

		// Solve all four problems at once
		(sampleA, sampleB) = sample.Solve();
		(realA, realB) = real.Solve();
	}
    
	[TestMethod]
	public void SampleA()
	{
		const int solution = 2;
		Assert.AreEqual(solution, sampleA);
	}
    
	[TestMethod]
	public void SampleB()
	{
		const int solution = 4;
		Assert.AreEqual(solution, sampleB);
	}
    
	[TestMethod]
	public void SolutionA()
	{
		const int solution = 538;
		Assert.AreEqual(solution, realA);
	}
    
	[TestMethod]
	public void SolutionB()
	{
		const int solution = 792;
		Assert.AreEqual(solution, realB);
	}
}