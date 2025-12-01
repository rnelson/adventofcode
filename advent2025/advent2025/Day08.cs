using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Collections;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

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
                    map,
                    AntennaLocation.Create(one),
                    AntennaLocation.Create(two),
                    maxIterations: 1);

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
                    map,
                    AntennaLocation.Create(one),
                    AntennaLocation.Create(two));

                foreach (var aNode in aNodes)
                {
                    // Skip past any impossible points.
                    if (!map.ContainsPoint(aNode.Row, aNode.Column))
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

    private class AntennaLocation : IComparable<AntennaLocation>
    {
        public int Row { get; private init; }
        public int Column { get; private init; }

        public static AntennaLocation Create((int row, int column) location) =>
            new() { Row = location.row, Column = location.column };

        public override bool Equals(object? obj)
        {
            if (obj is not AntennaLocation antennaLocation)
                return false;
            
            return Row == antennaLocation.Row && Column == antennaLocation.Column;
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        public bool Equals(AntennaLocation other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Column);
        }

        public int CompareTo(AntennaLocation? other)
        {
            if (other == null)
                return -1;
            
            var row = Row.CompareTo(other.Row);
            return row != 0 ? Column.CompareTo(other.Column) : row;
        }

        public static implicit operator (int, int)(AntennaLocation instance) => instance.ToMatrixCoords();
        
        public static AntennaLocation operator +(AntennaLocation instance, (int row, int column) delta) =>
            new() { Row = instance.Row + delta.row, Column = instance.Column + delta.column };
        
        public static AntennaLocation operator -(AntennaLocation instance, (int row, int column) delta) =>
            new() { Row = instance.Row - delta.row, Column = instance.Column - delta.column };

        public static List<AntennaLocation> FindAntinodes(Matrix<char> map, AntennaLocation one, AntennaLocation two, int maxIterations = int.MaxValue - 1)
        {
            var antinodes = new List<AntennaLocation>();
            
            var rowDelta = one.Row - two.Row;
            var colDelta = one.Column - two.Column;

            var node1 = new AntennaLocation { Row = one.Row, Column = one.Column };
            var node2 = new AntennaLocation { Row = two.Row, Column = two.Column };
            
            if (rowDelta < 0)
            {
                if (colDelta < 0)
                {
                    for (var i = 1; i <= maxIterations; i++)
                    {
                        node1 += (rowDelta * i, colDelta * i);
                        node2 -= (rowDelta * i, colDelta * i);
                    
                        if (map.ContainsPoint(node1))
                            antinodes.Add(node1);
                        if (map.ContainsPoint(node2))
                            antinodes.Add(node2);

                        if (!map.ContainsPoint(node1) && !map.ContainsPoint(node2))
                            break;
                    }
                }
                else
                {
                    for (var i = 1; i <= maxIterations; i++)
                    {
                        node1 -= (rowDelta * i, colDelta * i);
                        node2 += (rowDelta * i, colDelta * i);
                    
                        if (map.ContainsPoint(node1))
                            antinodes.Add(node1);
                        if (map.ContainsPoint(node2))
                            antinodes.Add(node2);

                        if (!map.ContainsPoint(node1) && !map.ContainsPoint(node2))
                            break;
                    }
                }
            }
            else
            {
                for (var i = 1; i <= maxIterations; i++)
                {
                    node1 += (rowDelta * i, colDelta * i);
                    node2 -= (rowDelta * i, colDelta * i);
                    
                    if (map.ContainsPoint(node1))
                        antinodes.Add(node1);
                    if (map.ContainsPoint(node2))
                        antinodes.Add(node2);

                    if (!map.ContainsPoint(node1) && !map.ContainsPoint(node2))
                        break;
                }
            }
            
            return antinodes;
        }

        private (int, int) ToMatrixCoords() => (Row, Column);
    }
}