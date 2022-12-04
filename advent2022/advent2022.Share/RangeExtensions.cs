namespace advent2022.Share;

public static class RangeExtensions
{
	public static bool Contains(this Range r1, Range r2) => r1.Start.Value <= r2.Start.Value && r1.End.Value >= r2.End.Value;

	public static bool Overlaps(this Range r1, Range r2)
	{
		var range1 = new List<int>();
		for (var i = r1.Start.Value; i <= r1.End.Value; i++)
			range1.Add(i);
		
		var range2 = new List<int>();
		for (var i = r2.Start.Value; i <= r2.End.Value; i++)
			range2.Add(i);

		return range1.Intersect(range2).Any();
	}
}