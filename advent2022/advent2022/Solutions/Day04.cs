using System.Text.RegularExpressions;
using advent2022.Share;

namespace advent2022.Solutions;

public class Day04 : DayBase
{
	private const string parsePattern = @"(\d+)-(\d+),(\d+)-(\d+)";
	
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		var parser = new Regex(parsePattern);
		var assignments = (from line in Input! select parser.Matches(line) into matches let one = new Range(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value)) let two = new Range(int.Parse(matches[0].Groups[3].Value), int.Parse(matches[0].Groups[4].Value)) select new Tuple<Range, Range>(one, two)).ToList();

		foreach (var pair in assignments)
		{
			Console.WriteLine($"[{pair.Item1.Start.Value}, {pair.Item1.End.Value}] overlaps with [{pair.Item2.Start.Value}, {pair.Item2.End.Value}]: {IsOverlapped(pair.Item1, pair.Item2)}");
		}
		
		return (assignments.Count(pair => IsOverlapped(pair.Item1, pair.Item2)), 0);
	}

	private static bool IsOverlapped(Range r1, Range r2) => r1.Contains(r2) || r2.Contains(r1);
	
}

public static class RangeExtensions2
{
	public static bool DebugContains(this Range r1, Range r2)
	{
		var starts = r1.Start.Value <= r2.Start.Value;
		var ends = r1.End.Value >= r2.End.Value;
		var result = starts && ends;

		var containsString = result ? "contains" : "does not contain";
		
		Console.WriteLine($"\t> ({r1.Start.Value},{r1.End.Value}) {containsString} ({r2.Start.Value},{r2.End.Value}) <");

		return result;
	}
}