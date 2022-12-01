namespace advent2022.Share;

public interface IDay
{
    /// <summary>
    /// Gets or sets the puzzle input.
    /// </summary>
    public IEnumerable<string>? Input { get; set; }
    
    /// <summary>
    /// Solves both part A and B of the puzzle.
    /// </summary>
    /// <returns>The solutions to parts A and B, respectively.</returns>
    public (object, object) Solve();
    
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