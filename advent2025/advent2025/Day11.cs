using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 11.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day11(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 11, output, isTest, fileSuffix)
{
    private readonly Dictionary<(ulong, int), ulong> _memoize = new();

    /// <inheritdoc/>
    public override object PartA() => Blink(25);

    /// <inheritdoc/>
    public override object PartB() => Blink(75);

    private ulong Blink(int blinkCount)
    {
        _memoize.Clear();
        
        return Input
            .First()
            .Split(" ")
            .GetNumbers<ulong>()
            .ToArray()
            .Aggregate(
                0UL,
                (current, stone) => current + Blink(stone, blinkCount)
            );
    }

    private ulong Blink(ulong stone, int blinkCount)
    {
        if (blinkCount == 0)
            return 1;
        
        if (_memoize.ContainsKey((stone, blinkCount)))
            return _memoize[(stone, blinkCount)];

        ulong stoneCount;
        var s = stone.ToString();

        if (stone == 0)
            stoneCount = Blink(1, blinkCount - 1);
        else if (stone.ToString().Length % 2 == 0)
            stoneCount = Blink(ulong.Parse(s[..(s.Length / 2)]), blinkCount - 1) +
                         Blink(ulong.Parse(s[(s.Length / 2)..]), blinkCount - 1);
        else
            stoneCount = Blink(stone * 2024, blinkCount - 1);
        
        _memoize[(stone, blinkCount)] = stoneCount;
        return stoneCount;
    }
}