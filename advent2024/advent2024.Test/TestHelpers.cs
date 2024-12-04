using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2024.Test;

/// <summary>
/// Helper bits for testing.
/// </summary>
/// <param name="testOutputHelper"><see cref="ITestOutputHelper"/> to provide output</param>
public partial class Test2024
{
    /// <summary>
    /// Gets the <see cref="Day"/> instance for <paramref name="dayType"/>
    /// </summary>
    /// <param name="dayType">The <see cref="Type"/> of day to acquire.</param>
    /// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
    /// <param name="isTest"><c>true</c> if test/sample input should be used, else <c>false</c>.</param>
    /// <param name="fileSuffix">File suffix for input file (e.g., "a" or "b" for separate test inputs).</param>
    /// <returns>The <paramref name="dayType"/> instance.</returns>
    /// <exception cref="Exception">Instantiating a <paramref name="dayType"/> object failed.</exception>
    private static Day GetDay(Type dayType, ITestOutputHelper output, bool isTest, string fileSuffix = "")
    {
        if (Activator.CreateInstance(dayType, output, isTest, fileSuffix) is not Day day)
            throw new Exception($"unable to instantiate type {dayType.FullName}");

        return day;
    }

    /// <summary>
    /// Runs SolveA() on the specified <see cref="Day"/>.
    /// </summary>
    /// <param name="dayType">The <see cref="Type"/> of day to acquire.</param>
    /// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
    /// <param name="isTest"><c>true</c> if test/sample input should be used, else <c>false</c>.</param>
    /// <param name="fileSuffix">File suffix for input file (e.g., "a" or "b" for separate test inputs).</param>
    /// <returns>The output of SolveA().</returns>
    private static string SolveA(Type dayType, ITestOutputHelper output, bool isTest, string fileSuffix = "a") =>
        GetDay(dayType, output, isTest, fileSuffix).PartA().ToString()!;

    /// <summary>
    /// Runs SolveB() on the specified <see cref="Day"/>.
    /// </summary>
    /// <param name="dayType">The <see cref="Type"/> of day to acquire.</param>
    /// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
    /// <param name="isTest"><c>true</c> if test/sample input should be used, else <c>false</c>.</param>
    /// <param name="fileSuffix">File suffix for input file (e.g., "a" or "b" for separate test inputs).</param>
    /// <returns>The output of SolveB().</returns>
    private static string SolveB(Type dayType, ITestOutputHelper output, bool isTest, string fileSuffix = "b") =>
        GetDay(dayType, output, isTest, fileSuffix).PartB().ToString()!;
}