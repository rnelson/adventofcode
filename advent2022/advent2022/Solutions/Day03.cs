using advent2022.Share;

namespace advent2022.Solutions;

public class Day03 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve() => (
		Input!.Sum(line => line.Halve().ToTuple().IntersectStringTuple().ScorePriority()),
		Input!.Chunk(3).Sum(lines => lines.IntersectStrings().ScorePriority())
	);
}

internal static class Day3Extensions
{
	public static string IntersectStrings(this string[] arr)
	{
		switch (arr.Length)
		{
			case 0:
				return string.Empty;
			case 1:
				return arr[0];
			default:
				var intersection = arr[0];

				for (var i = 1; i < arr.Length; i++)
					intersection = intersection.Intersection(arr[i]);

				return intersection;
		}
	}

	public static string IntersectStringTuple(this Tuple<string, string> t) => t.Item1.Intersection(t.Item2);
	
	public static int ScorePriority(this string s) => s.ToCharArray().Sum(c => c.ScorePriority());
	
	private static int ScorePriority(this char c) => c - (char.IsUpper(c) ? 38 : 96);
}