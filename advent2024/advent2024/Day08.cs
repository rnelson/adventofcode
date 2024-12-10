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
        var antennae = map.Where(c => _antennaCharacters.Contains(c)).ToArray();

        foreach (var frequency in _antennaCharacters)
        {
            var matchingAntennae = map.Where(c => c == frequency).ToArray();
            var permutations = matchingAntennae.GetPermutations(2);

            var options = (
                from list in permutations
                from antenna in list
                select AntennaLocation.Create(antenna)
                ).ToArray();

            /*
            var allOptionsWithDupes = permutations
                .SelectMany(o => o.ToArray())
                .Select(AntennaLocation.Create)
                .ToArray();
            Array.Sort(allOptionsWithDupes);

            var optionsList = new List<AntennaLocation>();
            foreach (var o in allOptionsWithDupes)
            {
                if (!optionsList.Any(i => i.Row == o.Row && i.Column == o.Column))
                    optionsList.Add(o);
            }

            var options = optionsList.ToArray();
            */
        }
        
        return "";
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
            new AntennaLocation { Row = location.row, Column = location.column };

        public int CompareTo(AntennaLocation? other)
        {
            if (other == null)
                return -1;

            var o = other!;
            
            var row = Row.CompareTo(o.Row);
            return row != 0 ? Column.CompareTo(o.Column) : row;
        }
    }
}