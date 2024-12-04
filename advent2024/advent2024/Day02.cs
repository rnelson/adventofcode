using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 2.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day02(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var safe = 0;

        foreach (var line in Input)
        {
            var values = line.ParseMany<int>().ToList();
            var isSafe = true;

            if (!values.IsAscending() && !values.IsDescending())
                continue;
            
            for (var i = 0; i < values.Count - 1; i++)
            {
                var diff = Math.Abs(values[i] - values[i + 1]);
                if (diff != 1 && diff != 2 && diff != 3)
                    isSafe = false;
            }

            if (isSafe)
                safe++;
        }

        return safe.ToString();
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var safe = 0;

        foreach (var line in Input)
        {
            var values = line.ParseMany<int>().ToList();

            var candidates = GetCandidates(values);
            
            foreach (var candidate in candidates)
            {
                var isSafe = true;
                var candidateArray = candidate.ToArray();
                
                if (!candidateArray.IsAscending() && !candidateArray.IsDescending())
                    continue;
                
                for (var i = 0; i < candidateArray.Length - 1; i++)
                {
                    var diff = Math.Abs(candidateArray[i] - candidateArray[i + 1]);
                    if (diff != 1 && diff != 2 && diff != 3)
                        isSafe = false;
                }

                if (!isSafe)
                    continue;

                safe++;
                break;
            }
        }

        return safe.ToString();
    }

    [SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
    private static IEnumerable<IEnumerable<int>> GetCandidates(IEnumerable<int> values)
    {
        var v = values.ToArray();
        var candidates = new List<IEnumerable<int>>
        {
            // The original list
            v,
            
            // First digit removed
            v[1..].ToList(),
            
            // Last digit removed
            v[..^1].ToList()
        };

        for (var i = 1; i < v.Length - 1; i++)
        {
            var newCandidate = new List<int>();
            newCandidate.AddRange(v[..i]);
            newCandidate.AddRange(v[(i+1)..]);
            
            candidates.Add(newCandidate);
        }

        return candidates;
    }
}