using advent2022.Share;

namespace advent2022.Solutions;

public class Day03 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		var partA = 0;

		foreach (var line in Input!)
		{
			var (c1, c2) = line.Halve();
			var intersection = c1.Intersection(c2);
			partA += intersection.ScorePriority();
		}

		var partB = Input!.Chunk(3).Sum(lines => lines.IntersectStrings().ScorePriority());

		return (partA, partB);
	}
}

internal static class Day3ArrayExtensions
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
}

internal static class Day3StringExtensions
{
	public static int ScorePriority(this string s) => s.ToCharArray().Sum(c => c.ScorePriority());
}

internal static class Day3CharExtensions
{
	public static int ScorePriority(this char c) => c - (char.IsUpper(c) ? 38 : 96);
}