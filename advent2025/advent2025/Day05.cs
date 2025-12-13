using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2025 day 5.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class Day05(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 5, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var data = ParseInput();
        var count = 0;

        foreach (var ingredient in data.Item2)
        {
            var validRanges = data
                .Item1
                .Where(r => ingredient >= r.Item1 && ingredient <= r.Item2)
                .ToArray();

            if (validRanges.Any())
                count++;
        }

        return count;
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var data = ParseInput();
        var freshIds = new List<long>();
        var threads = new List<Task>();
        var ranges = data.Item1.OrderBy(r => r.Item1).ToArray();

        foreach (var range in data.Item1)
            for (var i = range.Item1; i <= range.Item2; i++)
                freshIds.Add(i);
            //threads.Add(Task.Factory.StartNew(() => AddFreshIds(ref freshIds, range.Item1, range.Item2)));
        
        return freshIds.Distinct().Count();
        
        //Task.WaitAll(threads);

        //return freshIds.Count;
    }

    private void AddFreshIds(ref ConcurrentBag<long> ids, long start, long end)
    {
        for (var i = start; i <= end; i++)
            if (!ids.Contains(i))
                ids.Add(i);
    }

    private (IEnumerable<Tuple<long, long>>, IEnumerable<long>) ParseInput()
    {
        var ranges = new List<Tuple<long, long>>();
        var ingredients = new List<long>();

        foreach (var line in Input)
        {
            if (string.IsNullOrWhiteSpace(line))
                continue;

            if (long.TryParse(line, out var number))
            {
                ingredients.Add(number);
                continue;
            }

            if (line.Contains('-'))
            {
                var endpoints = line.Split('-').Select(long.Parse).ToArray();
                ranges.Add(new(endpoints[0], endpoints[1]));
                continue;
            }
            
            throw new InvalidDataException();
        }

        return (ranges, ingredients);
    }
}