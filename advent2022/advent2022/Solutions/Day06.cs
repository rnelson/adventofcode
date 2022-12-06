using advent2022.Share;

namespace advent2022.Solutions;

public class Day06 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve() => (Solve(4), Solve(14));

	/// <summary>
	/// Finds the marker positions.
	/// </summary>
	/// <remarks>
	/// 1. Loop through all 
	/// </remarks>
	/// <param name="windowSize">The width of the marker.</param>
	/// <returns>A list of marker positions.</returns>
	private List<int> Solve(int windowSize) =>
		Input!.Select(line => Enumerable        // For every line in the input...
				.Range(0, line.Length)          // For every position in the line...
				.First(i => line                //  Find the first index where...
					.Substring(i, windowSize)   // A `windowSize` substring...
					.Distinct()                 // Has only unique characters (relative to itself)...
					.Count() == windowSize)     // Totaling `windowSize`.
		                      + windowSize)     // Then add `windowSize` since we need the spot AFTER the marker.
			.ToList();

}