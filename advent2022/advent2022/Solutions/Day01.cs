using advent2022.Share;

namespace advent2022.Solutions;

public class Day01 : DayBase
{
    /// <inheritdoc />
    public override (object, object) Solve()
    {
        var sums =
            Input!.GroupByDivider<string>(string.Empty)         // group by elf
            .Select(GetNumericInputs<int>).ToList()             // str -> int
            .OrderByDescending(n => n.Sum())            // sum each elf's pile
            .ToArray();                                         // avoid multiple enumerations

        return (sums.First().Sum(), sums.Take(3).Sum(n => n.Sum()));
    }
}