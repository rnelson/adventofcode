namespace advent2022.Share;

public interface IDay
{
    /// <summary>
    /// Solves part A of the puzzle.
    /// </summary>
    /// <param name="input">Puzzle input.</param>
    /// <returns>The answer to the puzzle.</returns>
    public object A(IEnumerable<string> input);
    
    /// <summary>
    /// Solves part B of the puzzle.
    /// </summary>
    /// <param name="input">Puzzle input.</param>
    /// <returns>The answer to the puzzle.</returns>
    public object B(IEnumerable<string> input);
}