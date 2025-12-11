using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2025 day 4.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class Day04(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 4, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var result = FindAccessibleLocations();
        return "";
    }

    /// <inheritdoc/>
    public override object PartB() => "";

    private List<Tuple<int, int>> FindAccessibleLocations(int maxAdjacent = 4)
    {
        var grid = Input.ToMatrix();
        var (rows, columns) = grid.Size;
        var targets = new List<Tuple<int, int>>();
        
        for (var i = 0; i <= rows; i++)
        for (var j = 0; j <= columns; j++)
        {
            var coords = new List<(int, int)>
            {
                // above
                (i - 1, j - 1),
                (i - 1, j),
                (i - 1, j + 1),
                
                // sides
                (i, j - 1),
                (i, j + 1),
                
                // below
                (i + 1, j - 1),
                (i + 1, j),
                (i + 1, j + 1),
            };
            
            //var adjacents = coords
            //    .Where(coord => grid.ContainsPoint(coord.Item1, coord.Item2))
            //    .Count(coord => grid[coord.Item1, coord.Item2] == '@');

            var adjacents = 0;
            foreach (var coord in coords)
            {
                if (!grid.ContainsPoint(coord.Item1, coord.Item2))
                    continue;

                if ('@' == grid[coord.Item1, coord.Item2])
                    adjacents++;
            }
            
            if (adjacents <= maxAdjacent)
                targets.Add(new(i, j));
        }

        return targets;
    }
}