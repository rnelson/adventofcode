using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2025 day 3.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class Day03(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 3, output, isTest, fileSuffix)
{
    private static readonly char[] CommaSeparator = [','];
    private static readonly char[] HyphenSeparator = ['-'];
    
    /// <inheritdoc/>
    public override object PartA()
    {
        _ = FindLargestJoltage(
            "6645513635553536456434465554555532563634624843245574253462583453246844857313433341876266423526473852");
        
        var winners = new List<Tuple<long, IEnumerable<int>>>();
        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;
            
            winners.Add(FindLargestJoltage(line.Trim()));
        }

        return winners.Sum(t => t.Item1);
        
        // not 16020
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return string.Empty;
    }

    private Tuple<long, IEnumerable<int>> FindLargestJoltage(string numberString, int count = 2)
    {
        var numbers = numberString.ToCharArray().Select(c => c.ToString()).Select(int.Parse).ToArray();
        var permutations = numbers.GetPermutations2(count);
        var valids = new List<IEnumerable<int>>();

        foreach (var perm in permutations)
        {
            var valid = true;
            var p = perm.ToArray();
            var lastIndex = numbers.IndexOf(p.First());

            foreach (var n in p.Skip(1))
            {
                var remaining = numbers.Skip(lastIndex).ToArray();
                var index = numbers.IndexOf(n);
                if (lastIndex > index)
                    valid = false;
            }

            if (valid)
                valids.Add(p);
        }

        var largest = -1L;
        int[] largestPerm = [];
        foreach (var perm in valids)
        {
            var p = perm.ToArray();
            var v = long.Parse(string.Join(string.Empty, p));

            if (v <= largest)
                continue;
            
            largest = v;
            largestPerm = p;
        }
        
        return new(largest, largestPerm);
    }
}