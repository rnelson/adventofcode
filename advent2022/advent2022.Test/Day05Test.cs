using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day05Test
{
	private readonly IDay sample = new Day05(); 
	private readonly IDay real = new Day05();
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
			"    [D]    ",
			"[N] [C]    ",
			"[Z] [M] [P]",
			" 1   2   3 ",
			"",
			"move 1 from 2 to 1",
			"move 3 from 1 to 3",
			"move 2 from 2 to 1",
			"move 1 from 1 to 2"
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
		const string solution = "CMZ";
		Assert.AreEqual(solution, sampleA);
	}
    
	[TestMethod]
	public void SampleB()
	{
		const string solution = "MCD";
		Assert.AreEqual(solution, sampleB);
	}
    
	[TestMethod]
	public void SolutionA()
	{
		const string solution = "QNHWJVJZW";
		Assert.AreEqual(solution, realA);
	}
    
	[TestMethod]
	public void SolutionB()
	{
		const string solution = "BPCZJLFJW";
		Assert.AreEqual(solution, realB);
	}
}