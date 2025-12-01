using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 1.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class Day01(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 1, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA() => new CombinationLock(0, 99, 50)
        .CountZeroes(ParseInput());

    /// <inheritdoc/>
    public override object PartB() => new CombinationLock(0, 99, 50)
        .CountZeroes(ParseInput(), false);

    private IEnumerable<Tuple<CombinationLock.Direction, int>> ParseInput()
    {
        return (
            from line in Input
            where !string.IsNullOrWhiteSpace(line)
            let direction = line.StartsWith("L", StringComparison.CurrentCultureIgnoreCase) ? CombinationLock.Direction.Left : CombinationLock.Direction.Right
            let count = int.Parse(line[1..])
            select new Tuple<CombinationLock.Direction, int>(direction, count)
        ).ToList();
    }

    private class CombinationLock(int start, int end, int initial)
    {
        private int Start { get; } = start;
        private int End { get; } = end;
        private int _location = initial;

        internal enum Direction { Left, Right }

        public int CountZeroes(IEnumerable<Tuple<Direction, int>> rotations, bool stopsOnly = true)
        {
            if (stopsOnly)
                return rotations.Count(r => Rotate(r.Item1, r.Item2).ToArray().Last() == 0);

            return rotations.Select(r => Rotate(r.Item1, r.Item2).ToArray())
                .Select(ticks => ticks.Count(i => i == 0))
                .Sum();
        }

        private IEnumerable<int> Rotate(Direction direction, int count)
        {
            var moved = 0;
            var delta = direction == Direction.Right ? 1 : -1;
            
            while (moved < count)
            {
                _location += delta;

                if (_location > End)
                    _location = Start;
                if (_location < Start)
                    _location = End;
                
                yield return _location;
                moved++;
            }
        }
    }
}