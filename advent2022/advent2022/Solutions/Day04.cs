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
		var assignments = (
			from line in Input!
			select parser.Matches(line) into matches
			let one = new Range(int.Parse(matches[0].Groups[1].Value), int.Parse(matches[0].Groups[2].Value))
			let two = new Range(int.Parse(matches[0].Groups[3].Value), int.Parse(matches[0].Groups[4].Value))
			select new Tuple<Range, Range>(one, two)).ToList();
		
		return (
			assignments.Count(pair => IsStrictlyOverlapped(pair.Item1, pair.Item2)),
			assignments.Count(pair => IsOverlapped(pair.Item1, pair.Item2))
		);
	}

	private static bool IsStrictlyOverlapped(Range r1, Range r2) => r1.Contains(r2) || r2.Contains(r1);
	
	private static bool IsOverlapped(Range r1, Range r2) => r1.Overlaps(r2) || r2.Overlaps(r1);
}