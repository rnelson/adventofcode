using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 6.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day06(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(6, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var map = Input.ToMatrix();
        var (guardRow, guardColumn) = map.First('^');
        var direction = GuardDirection.Up;

        while (map.ContainsPoint(guardRow, guardColumn))
        {
            map[guardRow, guardColumn] = 'X';
            
            var delta = direction switch
            {
                GuardDirection.Up => (-1, 0),
                GuardDirection.Down => (1, 0),
                GuardDirection.Left => (0, -1),
                GuardDirection.Right => (0, 1),
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };
            
            var next = (guardRow + delta.Item1, guardColumn + delta.Item2);
            if (!map.ContainsPoint(next.Item1, next.Item2))
                break;
            var cell = map[next.Item1, next.Item2];

            while (cell.Equals('#'))
            {
                direction = direction switch
                {
                    GuardDirection.Up => GuardDirection.Right,
                    GuardDirection.Down => GuardDirection.Left,
                    GuardDirection.Left => GuardDirection.Up,
                    GuardDirection.Right => GuardDirection.Down,
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };
                delta = direction switch
                {
                    GuardDirection.Up => (-1, 0),
                    GuardDirection.Down => (1, 0),
                    GuardDirection.Left => (0, -1),
                    GuardDirection.Right => (0, 1),
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };
                
                next = (guardRow + delta.Item1, guardColumn + delta.Item2);
                cell = map[next.Item1, next.Item2];
            }

            (guardRow, guardColumn) = next;
            if (map.ContainsPoint(guardRow, guardColumn))
                map[guardRow, guardColumn] = '^';
        }

        return map.Where(c => c == 'X').ToArray().Length.ToString();
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var works = 0;
        var referenceMap = Input.ToMatrix();
        
        for (var obstacleRow = 0; obstacleRow < referenceMap.Size.Item1; obstacleRow++)
        for (var obstacleColumn = 0; obstacleColumn < referenceMap.Size.Item2; obstacleColumn++)
        {
            var map = Input.ToMatrix();

            // We can't place an obstacle in a cell that already has an object.
            if (map[obstacleRow, obstacleColumn] == 'X' || map[obstacleRow, obstacleColumn] == '^')
                continue;
            
            map[obstacleRow, obstacleColumn] = 'O';
            
            var (guardRow, guardColumn) = map.First('^');
            var direction = GuardDirection.Up;
            var visited = new Dictionary<(int x, int y, GuardDirection), int>();

            while (map.ContainsPoint(guardRow, guardColumn))
            {
                map[guardRow, guardColumn] = 'X';

                if (visited.ContainsKey((guardRow, guardColumn, direction)))
                {
                    visited[(guardRow, guardColumn, direction)]++;
                    works++;
                    break;
                }

                visited[(guardRow, guardColumn, direction)] = 1;

                var delta = direction switch
                {
                    GuardDirection.Up => (-1, 0),
                    GuardDirection.Down => (1, 0),
                    GuardDirection.Left => (0, -1),
                    GuardDirection.Right => (0, 1),
                    _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                };

                var next = (guardRow + delta.Item1, guardColumn + delta.Item2);
                if (!map.ContainsPoint(next.Item1, next.Item2))
                    break;
                var cell = map[next.Item1, next.Item2];

                while (cell.Equals('#') || cell.Equals('O'))
                {
                    direction = direction switch
                    {
                        GuardDirection.Up => GuardDirection.Right,
                        GuardDirection.Down => GuardDirection.Left,
                        GuardDirection.Left => GuardDirection.Up,
                        GuardDirection.Right => GuardDirection.Down,
                        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                    };
                    delta = direction switch
                    {
                        GuardDirection.Up => (-1, 0),
                        GuardDirection.Down => (1, 0),
                        GuardDirection.Left => (0, -1),
                        GuardDirection.Right => (0, 1),
                        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                    };

                    next = (guardRow + delta.Item1, guardColumn + delta.Item2);
                    cell = map[next.Item1, next.Item2];
                }

                (guardRow, guardColumn) = next;
                if (map.ContainsPoint(guardRow, guardColumn))
                    map[guardRow, guardColumn] = '^';
            }
        }

        return works.ToString();
    }
}

internal enum GuardDirection
{
    Up,
    Down,
    Left,
    Right
}