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
public class Day04(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(4, output, isTest, fileSuffix)
{
    private readonly ITestOutputHelper _output = output;

    /// <inheritdoc/>
    public override object PartA() => WordSearch(Input.ToMatrix(), "XMAS");

    /// <inheritdoc/>
    public override object PartB() => string.Empty;

    private string WordSearch(Matrix<char> m, string searchWord)
    {
        var answers = FindWord(m, searchWord);
        
        _output.WriteLine(string.Empty);
        _output.WriteLine(string.Empty);
        _output.WriteLine(string.Empty);
        _output.WriteLine(string.Empty);
        _output.WriteLine(string.Empty);
        foreach (var answer in answers)
            _output.WriteLine($"Found '{searchWord}' at ({answer.Item1}, {answer.Item2}) going {answer.Item3}");

        return answers.Count.ToString();
    }

    private List<(int, int, Direction)> FindWord(Matrix<char> m, string word)
    {
        var result = new List<(int, int, Direction)>();
        var directions = Enum.GetValues<Direction>().ToArray();
        
        var (rows, columns) = m.Size;
        var firstLetter = word[0];
        
        for (var r = 0; r < rows; r++)
        for (var c = 0; c < columns; c++)
        {
            if (m[r, c] != firstLetter)
                continue;
            
            _output.WriteLine($"Found {firstLetter} at ({r}, {c})");
            foreach (var direction in directions)
            {
                var success = CheckDirection(m, r, c, word, direction);
                if (success)
                    result.Add((r, c, direction));
                _output.WriteLine($"   Checking direction {direction}...: {success}");
            }
            
            // result.AddRange(
            //         from direction in directions
            //         where CheckDirection(m, x, y, word, direction)
            //         select (x, y, direction)
            //     );
        }

        return result;
    }

    private bool CheckDirection(Matrix<char> m, int startRow, int startColumn, string word, Direction direction)
    {
        var search = word.ToCharArray();
        int endRow, endColumn, rowDelta /* change in x-coord */, columnDelta /* change in y-coord */;

        switch (direction)
        {
            case Direction.Up:
                endRow = startRow - search.Length + 1;
                endColumn = startColumn;
                rowDelta = -1;
                columnDelta = 0;
                break;
            case Direction.Down:
                endRow = startRow + search.Length - 1;
                endColumn = startColumn;
                rowDelta = 1;
                columnDelta = 0;
                break;
            case Direction.Left:
                endRow = startRow;
                endColumn = startColumn - search.Length + 1;
                rowDelta = 0;
                columnDelta = -1;
                break;
            case Direction.Right:
                endRow = startRow;
                endColumn = startColumn + search.Length - 1;
                rowDelta = 0;
                columnDelta = 1;
                break;
            case Direction.UpLeft:
                endRow = startRow - search.Length + 1;
                endColumn = startColumn - search.Length + 1;
                rowDelta = -1;
                columnDelta = -1;
                break;
            case Direction.UpRight:
                endRow = startRow - (search.Length - 1);
                endColumn = startColumn + search.Length - 1;
                rowDelta = -1;
                columnDelta = 1;
                break;
            case Direction.DownLeft:
                endRow = startRow + search.Length - 1;
                endColumn = startColumn - search.Length + 1;
                rowDelta = 1;
                columnDelta = -1;
                break;
            case Direction.DownRight:
                endRow = startRow + search.Length - 1;
                endColumn = startColumn - search.Length + 1;
                rowDelta = 1;
                columnDelta = 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

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

    private enum Direction
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
}