using System.Diagnostics.CodeAnalysis;

namespace advent2022.Share;

public interface IDay
{
    /// <summary>
    /// Gets or sets the puzzle input.
    /// </summary>
    [SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
    public IEnumerable<string>? Input { get; set; }
    
    /// <summary>
    /// Solves both part A and B of the puzzle.
    /// </summary>
    /// <returns>The solutions to parts A and B, respectively.</returns>
    public (object, object) Solve();

    /// <summary>
    /// Loads the input file and returns its values as a list of strings.
    /// </summary>
    /// <returns>The contents of the input file.</returns>
    public IEnumerable<string> LoadInput();
}