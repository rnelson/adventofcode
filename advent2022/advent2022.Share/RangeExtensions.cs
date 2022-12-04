namespace advent2022.Share;

public static class RangeExtensions
{
	public static bool Contains(this Range r1, Range r2) => r1.Start.Value <= r2.Start.Value && r1.End.Value >= r2.End.Value;
}