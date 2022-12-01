using advent2022.Share;

namespace advent2022.Solutions;

public class Day01 : DayBase
{
    /// <inheritdoc />
    public override (object, object) Solve()
    {
        var groups = Input!.GroupByDivider<string>(string.Empty);
        var perElfCalorieCounts = groups.Select(GetNumericInputs<int>).ToList();
        var sums = perElfCalorieCounts.OrderByDescending(n => n.Sum()).ToArray();

        return (sums.First().Sum(), sums.Take(3).Sum(n => n.Sum()));
    }
}