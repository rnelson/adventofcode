using System.Diagnostics.CodeAnalysis;
using Xunit.Abstractions;
#pragma warning disable CS9113 // Parameter is unread.

namespace Libexec.Advent;

/// <summary>
/// Base class for each day's code.
/// </summary>
/// <param name="year">The year this puzzle belongs to.</param>
/// <param name="dayNumber">The number of the day for this puzzle.</param>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "UnusedMemberInSuper.Global")]
[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
[SuppressMessage("ReSharper", "VirtualMemberNeverOverridden.Global")]
public abstract class Day(int year, int dayNumber, ITestOutputHelper output, bool isTest = false, string testSuffix = "")
{
    /// <summary>
    /// The year this code belongs to.
    /// </summary>
    public int Year { get; } = year;
    
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
    /// <remarks>
    /// After removing my inputs from the repo, I had two options: do this right, or do it fast. And
    /// this is Advent of Code, dang it! Going lazy.
    /// </remarks>
    protected string InputFilename => Path.Combine(
        IsWindows ? $"C:/dev/aoc-inputs/{Year}" : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), $"dev/aoc-inputs/{Year}"),
        IsTest ? "test" : "real",
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
    
    /// <summary>
    /// Tells us whether we're running on Windows.
    /// </summary>
    /// <returns><c>true</c> if Windows, else <c>false</c>.</returns>
    private static bool IsWindows => Environment.OSVersion.Platform == PlatformID.Win32S
        || Environment.OSVersion.Platform == PlatformID.Win32Windows
        || Environment.OSVersion.Platform == PlatformID.Win32NT
        || Environment.OSVersion.Platform == PlatformID.WinCE;
}