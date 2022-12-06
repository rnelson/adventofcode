using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day06Test
{
	private readonly IDay sample = new Day06(); 
	private readonly IDay real = new Day06();
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
			"mjqjpqmgbljsphdztnvjfqwrcgsmlb",
			"bvwbjplbgvbhsrlpgdmjqwftvncz",
			"nppdvjthqldpwncqszvftbrmjlhg",
			"nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg",
			"zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw"
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
		var solutions = new[] {7, 5, 6, 10, 11};
		var sampleSolutions = sampleA as List<int>;
		
		for (var i = 0; i < solutions.Length; i++)
		{
			Assert.AreEqual(solutions[i], sampleSolutions!.ElementAt(i));
		}
	}
    
	[TestMethod]
	public void SampleB()
	{
		var solutions = new[] {19, 23, 23, 29, 26};
		var sampleSolutions = sampleB as List<int>;
		
		for (var i = 0; i < solutions.Length; i++)
		{
			Assert.AreEqual(solutions[i], sampleSolutions!.ElementAt(i));
		}
	}
    
	[TestMethod]
	public void SolutionA()
	{
		var solutions = new[] {1929};
		var realSolutions = realA as List<int>;
		
		for (var i = 0; i < realSolutions!.Count(); i++)
		{
			Assert.AreEqual(solutions[i], realSolutions!.ElementAt(i));
		}
	}
    
	[TestMethod]
	public void SolutionB()
	{
		var solutions = new[] {3298};
		var realSolutions = realB as List<int>;
		
		for (var i = 0; i < realSolutions!.Count(); i++)
		{
			Assert.AreEqual(solutions[i], realSolutions!.ElementAt(i));
		}
	}
}