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
			assignments.Count(pair => pair.Item1.Contains(pair.Item2) || pair.Item2.Contains(pair.Item1)),
			assignments.Count(pair => pair.Item1.Overlaps(pair.Item2) || pair.Item2.Overlaps(pair.Item1))
		);
	}
}