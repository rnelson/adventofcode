using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day07Test
{
	private readonly IDay sample = new Day07(); 
	private readonly IDay real = new Day07();
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
			"$ cd /",
			"$ ls",
			"dir a",
			"14848514 b.txt",
			"8504156 c.dat",
			"dir d",
			"$ cd a",
			"$ ls",
			"dir e",
			"29116 f",
			"2557 g",
			"62596 h.lst",
			"$ cd e",
			"$ ls",
			"584 i",
			"$ cd ..",
			"$ cd ..",
			"$ cd d",
			"$ ls",
			"4060174 j",
			"8033020 d.log",
			"5626152 d.ext",
			"7214296 k"
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
		const long solution = 95437;
		Assert.AreEqual(solution, sampleA);
	}
    
	[TestMethod]
	public void SampleB()
	{
		const long solution = 0;
		Assert.AreEqual(solution, sampleB);
	}
    
	[TestMethod]
	public void SolutionA()
	{
		const long solution = 0;
		Assert.AreEqual(solution, realA);
	}
    
	[TestMethod]
	public void SolutionB()
	{
		const long solution = 0;
		Assert.AreEqual(solution, realB);
	}
}