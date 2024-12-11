using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 11.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day11(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 11, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA() => Blink(25);

    /// <inheritdoc/>
    public override object PartB() => "";

    private int Blink(int blinkCount)
    {
        var initialStones = Input.First().Split(" ").GetNumbers<ulong>().ToArray();
        var blinkResult = initialStones;

        for (var i = 0; i < blinkCount; i++)
        {
            var newStones = new List<ulong>();
            
            foreach (var stone in blinkResult)
            {
                newStones.AddRange(ApplyRule(stone));
            }
            
            blinkResult = newStones.ToArray();
        }
        
        return blinkResult.Length;
    }

    private IEnumerable<ulong> ApplyRule(ulong input)
    {
        if (input == 0)
            return [1];

        if (input.ToString().Length % 2 != 0)
            return [input * 2024];
        
        var s = input.ToString();
        var bits = new[] { s[..(s.Length / 2)], s[(s.Length / 2)..] };
        var halves = bits.Select(ulong.Parse).ToArray();

        return [halves[0], halves[1]];

        var r1 = ApplyRule(halves[0]).ToArray();
        var r2 = ApplyRule(halves[1]).ToArray();
        var l = new List<ulong>();
        l.AddRange(r1);
        l.AddRange(r2);

        return l;
    }
}