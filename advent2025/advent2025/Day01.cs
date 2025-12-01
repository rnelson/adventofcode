using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 1.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day01(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 1, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var cLock = new CombinationLock(0, 99, 50);
        var turns = ParseInput();
        
        foreach (var turn in turns)
            cLock.Rotate(turn.Item1, turn.Item2);
        
        return cLock.Zeroes;
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return 0;
    }

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
        private int Start { get; init; } = start;
        private int End { get; init; } = end;
        private int Initial { get; init; } = initial;
        public int Zeroes { get; private set;  } = 0;
        private int _location = initial;

        internal enum Direction { Left, Right }

        public void Rotate(Direction direction, int count)
        {
            var totalSize = End - Start + 1;
            var moving = direction == Direction.Right ? count : -count;
            _location = (_location + moving) % totalSize;

            if (_location < Start)
                _location = End - Math.Abs(_location) + 1;
            if (_location > End)
                _location = Start + _location;

            if (_location == 0)
                Zeroes++;
        }
    }
}