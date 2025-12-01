using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 3.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public partial class Day03(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 3, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var sum = (
                from line in Input
                let r = MulRegex()
                from Match match in r.Matches(line)
                let n1 = int.Parse(match.Groups[2].Value)
                let n2 = int.Parse(match.Groups[3].Value)
                select n1 * n2)
            .Sum();

        return sum.ToString();
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var shouldDo = true;
        var sum = 0;
        
        foreach (var line in Input)
        {
            var r = DoDontMulRegex();
            var matches = r.Matches(line);

            foreach (Match match in matches)
            {
                switch (match.Groups[0].Value)
                {
                    case "do()":
                        shouldDo = true;
                        break;
                    case "don't()":
                        shouldDo = false;
                        break;
                    default:
                        if (!shouldDo)
                            continue;

                        var n1 = int.Parse(match.Groups[2].Value);
                        var n2 = int.Parse(match.Groups[3].Value);

                        sum += n1 * n2;
                        break;
                }
            }
        }
        
        return sum.ToString();
    }

    [GeneratedRegex(@"(mul\((\d{1,3}),(\d{1,3})\))")]
    private static partial Regex MulRegex();

    [GeneratedRegex(@"(mul\((\d{1,3}),(\d{1,3})\)|don't\(\)|do\(\))")]
    private static partial Regex DoDontMulRegex();
}