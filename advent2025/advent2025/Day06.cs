using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Collections;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 6.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
[SuppressMessage("ReSharper", "PropertyCanBeMadeInitOnly.Local")]
public class Day06(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 6, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA() =>
        TraverseRoom(new())
            .Map
            .Where(c => c == 'X')
            .ToArray()
            .Length;

    /// <inheritdoc/>
    public override object PartB()
    {
        var referenceMap = Input.ToMatrix();
        var workingObstacles = 0;
        
        for (var obstacleRow = 0; obstacleRow < referenceMap.Size.Item1; obstacleRow++)
        for (var obstacleColumn = 0; obstacleColumn < referenceMap.Size.Item2; obstacleColumn++)
        {
            var request = new TraversalRequest
            {
                CycleWatch = true,
                ObstacleRow = obstacleRow,
                ObstacleColumn = obstacleColumn
            };
            
            var response = TraverseRoom(request);
            if (response.CycleDetected ?? false)
                workingObstacles++;
        }
        
        return workingObstacles;
    }

    private TraversalResponse TraverseRoom(TraversalRequest request)
    {
        var response = new TraversalResponse(Input.ToMatrix());

        // If we're checking for cycles (part B), add the requested obstacle.
        if (request.CycleWatch ?? false)
        {
            // Everything in the request and response records are nullable because lazy, but make sure that we have
            // the obstacle coordinates if the caller is requesting we look for cycles.
            var obstacleRow = request.ObstacleRow
                              ?? throw new ArgumentNullException(nameof(request), "request.ObstacleRow cannot be null if performing cycle watch.");
            var obstacleColumn = request.ObstacleColumn
                                 ?? throw new ArgumentNullException(nameof(request), "request.ObstacleColumn cannot be null if performing cycle watch.");

            // If there's something there, or if it's an invalid coordinate, err out.
            if (!response.Map.ContainsPoint(obstacleRow, obstacleColumn)
                || response.Map[obstacleRow, obstacleColumn] == 'X'
                || response.Map[obstacleRow, obstacleColumn] == '^')
            {
                response.CycleError = true;
                return response;
            }

            // Add the obstacle.
            response.Map[obstacleRow, obstacleColumn] = 'O';
        }
        
        var visited = new Dictionary<(int x, int y, GuardDirection), int>();
        var (guardRow, guardColumn) = response.Map.First('^');
        var direction = GuardDirection.Up;
        var delta = (-1, 0);

        while (response.Map.ContainsPoint(guardRow, guardColumn))
        {
            // Mark that the guard has been to this cell.
            response.Map[guardRow, guardColumn] = 'X';

            // If we've found a cycle, and we're watching for them, bail out.
            if ((request.CycleWatch ?? false) && visited.ContainsKey((guardRow, guardColumn, direction)))
            {
                visited[(guardRow, guardColumn, direction)]++;
                response.CycleDetected = true;
                return response;
            }

            // Whether or not we're specifically looking for cycles, mark this cell.
            visited[(guardRow, guardColumn, direction)] = 1;

            // Check to see if the next cell is part of the map. If not, we found our
            // exit.
            var next = (guardRow + delta.Item1, guardColumn + delta.Item2);
            if (!response.Map.ContainsPoint(next.Item1, next.Item2))
            {
                response.ExitCell = (guardRow, guardColumn);
                return response;
            }
            
            // If we found some sort of obstacle, default or added by the caller,
            // start rotating and moving until we aren't stuck at an obstacle.
            var cell = response.Map[next.Item1, next.Item2];
            while (cell.Equals('#') || cell.Equals('O'))
            {
                direction = direction switch
                {
                    GuardDirection.Up => GuardDirection.Right,
                    GuardDirection.Down => GuardDirection.Left,
                    GuardDirection.Left => GuardDirection.Up,
                    GuardDirection.Right => GuardDirection.Down,
                    _ => throw new InvalidEnumArgumentException(nameof(direction))
                };
                
                delta = direction switch
                {
                    GuardDirection.Up => (-1, 0),
                    GuardDirection.Down => (1, 0),
                    GuardDirection.Left => (0, -1),
                    GuardDirection.Right => (0, 1),
                    _ => throw new InvalidEnumArgumentException(nameof(direction))
                };

                next = (guardRow + delta.Item1, guardColumn + delta.Item2);
                cell = response.Map[next.Item1, next.Item2];
            }

            // Move the guard to this new cell.
            (guardRow, guardColumn) = next;
            if (response.Map.ContainsPoint(guardRow, guardColumn))
                response.Map[guardRow, guardColumn] = '^';
        }

        return response;
    }

    private class TraversalRequest
    {
        public int? ObstacleRow { get; set; }
        public int? ObstacleColumn { get; set; }
        public bool? CycleWatch { get; set; }
    }

    private class TraversalResponse(Matrix<char> map)
    {
        public (int, int)? ExitCell { get; set; }
        public bool? CycleDetected { get; set; }
        public bool? CycleError { get; set; }
        public Matrix<char> Map { get; } = map;
    }
    
    private enum GuardDirection
    {
        Up,
        Down,
        Left,
        Right
    }
}