using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Collections;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 4.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day04(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 4, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA() => FindWord(Input.ToMatrix(), "XMAS").Count.ToString();

    /// <inheritdoc/>
    public override object PartB()
    {
        var matrix = Input.ToMatrix();
        var finds = FindWord(matrix, "MAS", true);

        var ehs = new SortedSet<(int, int)>();
        var exes = 0;
        
        foreach (var find in finds)
        {
            var delta = find.Item3.GetDeltas();
            var (aRow, aCol) = (find.Item1 + delta.Item1, find.Item2 + delta.Item2);
            
            if (!ehs.Add((aRow, aCol)))
                exes++;
        }
        
        return exes.ToString();
    }

    private static List<(int, int, Direction)> FindWord(Matrix<char> m, string word, bool diagonalsOnly = false)
    {
        var result = new List<(int, int, Direction)>();
        
        var directions = (diagonalsOnly
                ? DirectionCollections.GetDiagonals()
                : DirectionCollections.GetAll())
            .ToArray();
        
        var (rows, columns) = m.Size;
        var firstLetter = word[0];
        
        for (var r = 0; r < rows; r++)
        for (var c = 0; c < columns; c++)
        {
            if (m[r, c] != firstLetter)
                continue;
            
            result.AddRange(
                    from direction in directions
                    where CheckDirection(m, r, c, word, direction)
                    select (r, c, direction)
                );
        }

        return result;
    }

    private static bool CheckDirection(Matrix<char> m, int startRow, int startColumn, string word, Direction direction)
    {
        var (rowDelta, columnDelta) = direction.GetDeltas();
        var (endRow, endColumn) = direction.GetExpectedEndCoordinates(word, startRow, startColumn);

        // Check to see if we're going to go out of bounds in the direction we're searching. If so,
        // we can quit here because we're obviously not going to find the full string from (startX, startY)
        // in the requested direction.
        if (!m.ContainsPoint(endRow, endColumn))
            return false;

        // Start out at the specific cell initially requested, then keep going until we hit
        // what we determined to be our end cell.
        var foundString = "";
        int r = startRow, c = startColumn;
        while (true)
        {
            foundString += m[r, c].ToString(); 
            if (foundString.Equals(word))
                return true;
            
            if (r == endRow && c == endColumn)
                break;
            
            r += rowDelta;
            c += columnDelta;
            
            if (!m.ContainsPoint(r, c))
                break;
        }

        return foundString.Equals(word);
    }
}

internal enum Direction
{
    Up,
    Down,
    Left,
    Right,
    UpLeft,
    UpRight,
    DownLeft,
    DownRight
}

internal static class DirectionExtensions
{
    public static (int, int) GetDeltas(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => (-1, 0),
            Direction.Down => (1, 0),
            Direction.Left => (0, -1),
            Direction.Right => (0, 1),
            Direction.UpLeft => (-1, -1),
            Direction.UpRight => (-1, 1),
            Direction.DownLeft => (1, -1),
            Direction.DownRight => (1, 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }

    public static (int, int) GetExpectedEndCoordinates(this Direction direction, string searchWord, int row, int column)
    {
        var len = searchWord.Length;
        
        return direction switch
        {
            Direction.Up => (row - len + 1, column),
            Direction.Down => (row + len - 1, column),
            Direction.Left => (row, column - len + 1),
            Direction.Right => (row, column + len - 1),
            Direction.UpLeft => (row - len + 1, column - len + 1),
            Direction.UpRight => (row - len + 1, column + len - 1),
            Direction.DownLeft => (row + len - 1, column - len + 1),
            Direction.DownRight => (row + len - 1, column + len - 1),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
}

[SuppressMessage("ReSharper", "UnusedMember.Global")]
internal static class DirectionCollections
{
    public static IEnumerable<Direction> GetAll() => Enum.GetValues<Direction>();

    public static IEnumerable<Direction> GetCardinals() =>
    [
        Direction.Left,
        Direction.Right,
        Direction.Up,
        Direction.Down
    ];

    public static IEnumerable<Direction> GetDiagonals() =>
    [
        Direction.UpLeft,
        Direction.UpRight,
        Direction.DownLeft,
        Direction.DownRight
    ];
}