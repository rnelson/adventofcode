using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Collections;
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
                    map,
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
                var aNodes = AntennaLocation.FindAntinodes2(
                    map,
                    AntennaLocation.Create(one),
                    AntennaLocation.Create(two),
                    frequency);

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

            var o = other!;
            
            var row = Row.CompareTo(o.Row);
            return row != 0 ? Column.CompareTo(o.Column) : row;
        }

        public static IEnumerable<AntennaLocation> FindAntinodes(Matrix<char> map, AntennaLocation one, AntennaLocation two, char frequency = '.')
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
    
        public static IEnumerable<AntennaLocation> FindAntinodes2(Matrix<char> map, AntennaLocation one, AntennaLocation two, char frequency = '.')
        {
            var antinodes = new List<AntennaLocation>();
            
            var rowDelta = one.Row - two.Row;
            var colDelta = one.Column - two.Column;

            if (rowDelta == 0 && colDelta == 0)
                throw new InvalidOperationException("identical points not allowed");

            var node1 = new AntennaLocation { Row = one.Row + rowDelta, Column = one.Column + colDelta };
            //var node2 = new AntennaLocation { Row = one.Row + rowDelta, Column = one.Column - colDelta };
            //var node3 = new AntennaLocation { Row = one.Row - rowDelta, Column = one.Column + colDelta };
            //var node4 = new AntennaLocation { Row = one.Row - rowDelta, Column = one.Column - colDelta };
            //var node5 = new AntennaLocation { Row = two.Row + rowDelta, Column = two.Column + colDelta };
            //var node6 = new AntennaLocation { Row = two.Row + rowDelta, Column = two.Column - colDelta };
            //var node7 = new AntennaLocation { Row = two.Row - rowDelta, Column = two.Column + colDelta };
            var node8 = new AntennaLocation { Row = two.Row - rowDelta, Column = two.Column - colDelta };

            while (map.ContainsPoint(node1.Row, node1.Column)
                   //|| map.ContainsPoint(node2.Row, node2.Column)
                   //|| map.ContainsPoint(node3.Row, node3.Column)
                   //|| map.ContainsPoint(node4.Row, node4.Column)
                   //|| map.ContainsPoint(node5.Row, node5.Column)
                   //|| map.ContainsPoint(node6.Row, node6.Column)
                   //|| map.ContainsPoint(node7.Row, node7.Column)
                   || map.ContainsPoint(node8.Row, node8.Column))
            {
                if (map.ContainsPoint(node1.Row, node1.Column) && !node1.Equals(one) && !node1.Equals(two))
                    antinodes.Add(node1);
                //if (map.ContainsPoint(node2.Row, node2.Column) && !node2.Equals(one) && !node2.Equals(two))
                //    antinodes.Add(node2);
                //if (map.ContainsPoint(node3.Row, node3.Column) && !node3.Equals(one) && !node3.Equals(two))
                //    antinodes.Add(node3);
                //if (map.ContainsPoint(node4.Row, node4.Column) && !node4.Equals(one) && !node4.Equals(two))
                //    antinodes.Add(node4);
                //if (map.ContainsPoint(node5.Row, node5.Column) && !node5.Equals(one) && !node5.Equals(two))
                //    antinodes.Add(node5);
                //if (map.ContainsPoint(node6.Row, node6.Column) && !node6.Equals(one) && !node6.Equals(two))
                //    antinodes.Add(node6);
                //if (map.ContainsPoint(node7.Row, node7.Column) && !node7.Equals(one) || node7.Equals(two))
                //    antinodes.Add(node7);
                if (map.ContainsPoint(node8.Row, node8.Column) && !node8.Equals(one)&& !node8.Equals(two))
                    antinodes.Add(node8);
                
                node1 = new() { Row = node1.Row + rowDelta, Column = node1.Column + colDelta };
                //node2 = new() { Row = node2.Row + rowDelta, Column = node2.Column - colDelta };
                //node3 = new() { Row = node3.Row - rowDelta, Column = node3.Column + colDelta };
                //node4 = new() { Row = node4.Row - rowDelta, Column = node4.Column - colDelta };
                //node5 = new() { Row = node5.Row + rowDelta, Column = node5.Column + colDelta };
                //node6 = new() { Row = node6.Row + rowDelta, Column = node6.Column - colDelta };
                //node7 = new() { Row = node7.Row - rowDelta, Column = node7.Column + colDelta };
                node8 = new() { Row = node8.Row - rowDelta, Column = node8.Column - colDelta };
            }
            
            return antinodes;
        }
    }
}