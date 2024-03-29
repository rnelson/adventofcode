using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day09Test
{
	private readonly IDay sample = new Day09(); 
	private readonly IDay real = new Day09();
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
			""
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
		const int solution = 0;
		Assert.AreEqual(solution, sampleA);
	}
    
	[TestMethod]
	public void SampleB()
	{
		const int solution = 0;
		Assert.AreEqual(solution, sampleB);
	}
    
	[TestMethod]
	public void SolutionA()
	{
		const int solution = 0;
		Assert.AreEqual(solution, realA);
	}
    
	[TestMethod]
	public void SolutionB()
	{
		const int solution = 0;
		Assert.AreEqual(solution, realB);
	}
}