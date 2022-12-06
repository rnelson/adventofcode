using advent2022.Share;

namespace advent2022.Solutions;

public class Day06 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		var partA = new List<int>();
		var partB = new List<int>();

		foreach (var line in Input!)
		{
			var index = 1;
			//var seen = new List<char>();
			
			foreach (var window in line.SlidingWindows(4))
			{
				// The first few can never be it
				if (index < 4)
				{
					index++;
					continue;
				}

				var possibleMarker = line.Substring(index - 4, 4);
				if (possibleMarker.Distinct().Count() == 4)
				{
					partA.Add(index);
					break;
				}

				index++;
				
				/*
				if (window.Distinct().Count() == window.Length &&
				    !window.Intersect(seen).Any())
				{
					partA.Add(index);
					break;
				}
				
				//seen.AddRange(window.ToCharArray().Distinct());
				seen.Add(window[0]);
				index++;
				*/
			}
		}
		
		return (partA, partB);
	}
}