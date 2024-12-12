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
    public override object PartB() => ""; //Blink(75);

    private int Blink(int blinkCount)
    {
        var initialStones = Input.First().Split(" ").GetNumbers<ulong>().ToArray();
        var blinkResult = initialStones;

        for (var i = 0; i < blinkCount; i++)
        {
            var newStones = new List<ulong>();
            
            foreach (var stone in blinkResult)
            {
                if (stone == 0)
                {
                    newStones.AddRange([1]);
                    continue;
                }

                if (stone.ToString().Length % 2 != 0)
                {
                    newStones.AddRange([stone * 2024]);
                    continue;
                }

                var s = stone.ToString();
                var bits = new[] { s[..(s.Length / 2)], s[(s.Length / 2)..] };
                var halves = bits.Select(ulong.Parse).ToArray();

                newStones.AddRange([halves[0], halves[1]]);
            }
            
            blinkResult = newStones.ToArray();
        }
        
        return blinkResult.Length;
    }
}