using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent;

/// <summary>
/// Base class for each day's code.
/// </summary>
/// <param name="dayNumber">The number of the day.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
public abstract class Day(int dayNumber, bool isTest = false, string testSuffix = "")
{
    /// <summary>
    /// The numerical day this code belongs to.
    /// </summary>
    public int DayNumber { get; } = dayNumber;
    
    /// <summary>
    /// Gets or sets the puzzle input.
    /// </summary>
    public IEnumerable<string> Input => File.ReadAllLines(InputFilename);

    /// <summary>
    /// Flag indicating whether we are running the test input.
    /// </summary>
    public bool IsTest { get; } = isTest;

    /// <summary>
    /// Test file suffix, before the extension.
    /// </summary>
    /// <remarks>
    /// This is used when there are separate A and B test inputs.
    /// </remarks>
    public string TestSuffix { get; } = testSuffix;

    /// <summary>
    /// The filename for the day's input.
    /// </summary>
    protected string InputFilename => Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        "Inputs",
        IsTest ? "Test" : "Real",
        $"Day{DayNumber:00}{TestSuffix}.txt");

    /// <summary>
    /// Solves part A.
    /// </summary>
    /// <returns>The solution to part A.</returns>
    public abstract object PartA();
    
    /// <summary>
    /// Solves part B.
    /// </summary>
    /// <returns>The solution to part B.</returns>
    public abstract object PartB();

    /// <summary>
    /// Solves both part A and B of the puzzle.
    /// </summary>
    /// <returns>The solutions to parts A and B, respectively.</returns>
    public virtual (string, string) Solve()
    {
        var a = PartA().ToString() ?? "<null>";
        var b = PartB().ToString() ?? "<null>";
        return (a, b);
    }
}