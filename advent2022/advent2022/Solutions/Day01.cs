using advent2022.Share;

namespace advent2022.Solutions;

public class Day01 : DayBase
{
    /// <inheritdoc />
    public override object A(IEnumerable<string> input)
    {
        var groups = input.GroupByDivider<string>(string.Empty);
        var elfCalorieCounts = groups.Select(GetNumericInputs<int>).ToList();
        var descendingTotals = elfCalorieCounts.OrderByDescending(n => n.Sum());

        return descendingTotals.First().Sum();
    }

    /// <inheritdoc />
    public override object B(IEnumerable<string> input)
    {
        var groups = input.GroupByDivider<string>(string.Empty);
        var elfCalorieCounts = groups.Select(GetNumericInputs<int>).ToList();
        var descendingTotals = elfCalorieCounts.OrderByDescending(n => n.Sum());

        return descendingTotals.Take(3).Sum(l => l.Sum());
    }
}