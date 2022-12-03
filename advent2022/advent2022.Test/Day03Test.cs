using System.Diagnostics.CodeAnalysis;
using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
[SuppressMessage("ReSharper", "StringLiteralTypo")]
public class Day03Test
{
	private readonly IDay sample = new Day03(); 
	private readonly IDay real = new Day03();
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
			"vJrwpWtwJgWrhcsFMMfFFhFp",
			"jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL",
			"PmmdzqPrVvPwwTWBwg",
			"wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn",
			"ttgJtRGJQctTZtZT",
			"CrZsJsPPZsGzwwsLwLmpwMDw"
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
		const int solution = 157;
		Assert.AreEqual(solution, sampleA);
	}
    
	[TestMethod]
	public void SampleB()
	{
		const int solution = 70;
		Assert.AreEqual(solution, sampleB);
	}
    
	[TestMethod]
	public void SolutionA()
	{
		const int solution = 8252;
		Assert.AreEqual(solution, realA);
	}
    
	[TestMethod]
	public void SolutionB()
	{
		const int solution = 2828;
		Assert.AreEqual(solution, realB);
	}
}