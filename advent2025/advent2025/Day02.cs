using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2025 day 2.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class Day02(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 2, output, isTest, fileSuffix)
{
    private static readonly char[] CommaSeparator = [','];
    private static readonly char[] HyphenSeparator = ['-'];
    
    /// <inheritdoc/>
    public override object PartA()
    {
        var results = ParseInput();
        
        var invalids = new List<long>();
        foreach (var result in results)
        {
            invalids.AddRange(result.Item3);
        }

        return invalids.Sum();
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var results = ParseInput(false);
        
        var invalids = new List<long>();
        foreach (var result in results)
        {
            invalids.AddRange(result.Item3);
        }

        return invalids.Sum();
    }

    private IEnumerable<Tuple<long, long, IEnumerable<long>>> ParseInput(bool onlyHalf = true)
    {
        var result = new List<Tuple<long, long, IEnumerable<long>>>();
        var line = string.Join(string.Empty, Input);
        var ranges = line.Split(CommaSeparator);

        foreach (var range in ranges)
        {
            if (string.IsNullOrWhiteSpace(range))
                continue;
            
            var bits = range.Split(HyphenSeparator).Select(long.Parse).ToArray();
            var invalid = new List<long>();

            for (var n = bits[0]; n <= bits[1]; n++)
            {
                var nStr = n.ToString();
                var mid = nStr.Length / 2;

                if (onlyHalf)
                {
                    var l = nStr[..mid];
                    var r = nStr[mid..];

                    if (l.Equals(r))
                        invalid.Add(n);
                }
                else
                {
                    for (var size = 1; size <= mid; size++)
                    {
                        if (nStr.Length % size != 0)
                            continue;

                        var partial = nStr[..size];
                        var count = nStr.Length / size;
                        var suspect = string.Concat(Enumerable.Repeat(partial, count));

                        if (suspect.Equals(nStr) && !invalid.Contains(n))
                            invalid.Add(n);
                    }
                }
            }
            
            result.Add(new(bits[0], bits[1], invalid));
        }

        return result;
    }
}