using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 8.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day08(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 8, output, isTest, fileSuffix)
{
    private readonly char[] _antennaCharacters = [
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
        '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
    ];
    
    /// <inheritdoc/>
    public override object PartA()
    {
        var map = Input.ToMatrix();
        var antinodes = new Dictionary<(int, int), List<char>>();

        foreach (var frequency in _antennaCharacters)
        {
            var matchingAntennae = map.Where(c => c == frequency).ToArray();
            var permutations = matchingAntennae.GetPermutations(2).ToArray();

            foreach (var permutation in permutations)
            {
                var pArray = permutation.ToArray();
                var one = pArray.First();
                var two = pArray.Last();
                
                // Find all potential antinodes.
                var aNodes = AntennaLocation.FindAntinodes(
                    AntennaLocation.Create(one),
                    AntennaLocation.Create(two));

                foreach (var aNode in aNodes)
                {
                    // Skip past any impossible points.
                    if (!map.ContainsPoint(aNode.Row, aNode.Column))
                        continue;
                    
                    // Skip this antinode if it's one of the nodes for this frequency.
                    if (map[aNode.Row, aNode.Column] == frequency)
                        continue;
                    
                    // Add the antinode to the list.
                    if (antinodes.TryGetValue((aNode.Row, aNode.Column), out var frequenciesThere))
                        frequenciesThere.Add(frequency);
                    else
                        antinodes.Add((aNode.Row, aNode.Column), [frequency]);
                }
            }
        }
        
        return antinodes.Keys.Count;
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return "";
    }

    private class AntennaLocation : IComparable<AntennaLocation>
    {
        public int Row { get; set; }
        public int Column { get; set; }

        public static AntennaLocation Create((int row, int column) location) =>
            new() { Row = location.row, Column = location.column };

        public int CompareTo(AntennaLocation? other)
        {
            if (other == null)
                return -1;

            var o = other!;
            
            var row = Row.CompareTo(o.Row);
            return row != 0 ? Column.CompareTo(o.Column) : row;
        }

        public static IEnumerable<AntennaLocation> FindAntinodes(AntennaLocation one, AntennaLocation two)
        {
            var antinodes = new List<AntennaLocation>();
            
            var rowDelta = one.Row - two.Row;
            var colDelta = one.Column - two.Column;

            if (rowDelta == 0 && colDelta == 0)
                throw new InvalidOperationException("identical points not allowed");

            if (rowDelta == 0)
            {
                antinodes.Add(new() { Row = one.Row, Column = one.Column + colDelta });
                antinodes.Add(new() { Row = one.Row, Column = one.Column - colDelta });
            }
            else if (colDelta == 0)
            {
                antinodes.Add(new() { Row = one.Row + rowDelta, Column = one.Column });
                antinodes.Add(new() { Row = one.Row - rowDelta, Column = one.Column });
            }
            else if (rowDelta < 0)
            {
                if (colDelta < 0)
                {
                    antinodes.Add(new() { Row = one.Row + rowDelta, Column = one.Column + colDelta });
                    antinodes.Add(new() { Row = two.Row - rowDelta, Column = two.Column - colDelta });
                }
                else
                {
                    antinodes.Add(new() { Row = one.Row - rowDelta, Column = one.Column - colDelta });
                    antinodes.Add(new() { Row = two.Row + rowDelta, Column = two.Column + colDelta });
                }
            }
            else
            {
                antinodes.Add(new() { Row = one.Row + rowDelta, Column = one.Column + colDelta });
                antinodes.Add(new() { Row = two.Row - rowDelta, Column = two.Column - colDelta });
            }
            
            return antinodes;
        }
    }
}