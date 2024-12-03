using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;

namespace advent2024;

/// <summary>
/// 2024 day 1.
/// </summary>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day01(bool isTest = false, string fileSuffix = "") : Day(1, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var values = ParseInput();
        return values.Sum(value => Math.Abs(value.Item1 - value.Item2));
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var values = ParseInput();
        return values.Sum(l => l.Item1 * values.Count(r => r.Item2 == l.Item1));
    }

    private List<Tuple<int, int>> ParseInput()
    {
        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in Input)
        {
            var bits = line.ParseMany<int>().ToArray();
            left.Add(bits[0]);
            right.Add(bits[1]);
        }
        
        var sortedLeft = left.ToImmutableArray().Sort();
        var sortedRight = right.ToImmutableArray().Sort();

        return sortedLeft.Select((value, index) => new Tuple<int, int>(value, sortedRight[index])).ToList();
    }
}