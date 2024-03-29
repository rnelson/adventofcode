﻿using advent2022.Share;

namespace advent2022.Solutions;

public class Day01 : DayBase
{
	/// <inheritdoc />
	public override (object, object) Solve()
	{
		var sums = Input!
			.GroupByDivider<string>("")       // group by elf
			.Select(GetNumbers<int>)          // str -> int
			.OrderByDescending(n => n.Sum())  // sum each elf's pile
			.ToArray();                       // avoid multiple enumerations

		return (sums.First().Sum(), sums.Take(3).Sum(n => n.Sum()));
	}
}