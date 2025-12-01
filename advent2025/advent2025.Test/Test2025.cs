using System.Diagnostics;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025.Test;

/// <summary>
/// 2024 test file.
/// </summary>
/// <param name="testOutputHelper"><see cref="ITestOutputHelper"/> to provide output.</param>
public partial class Test2025(ITestOutputHelper testOutputHelper)
{
    /// <summary>
    /// Run all tests for <see cref="dayType"/>.
    /// </summary>
    /// <param name="dayType">the <see cref="Type"/> for the <see cref="Day"/> to test.</param>
    /// <param name="expectedA">expected answer for part A.</param>
    /// <param name="expectedB">expected answer for part B.</param>
    /// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
    /// <param name="twoPartTest"><c>true</c> for separate "a" and "b" test input files, otherwise <c>false</c>.</param>
    [Theory]
    [InlineData(typeof(Day01), "3", "", true)]
    [InlineData(typeof(Day01), "", "")]
    public void RunTests(Type dayType, string expectedA, string expectedB, bool isTest = false, bool twoPartTest = false)
    {
        string actualA, actualB;
        var watch = Stopwatch.StartNew();

        if (!twoPartTest)
            (actualA, actualB) = GetDay(dayType, testOutputHelper, isTest).Solve();
        else
        {
            actualA = SolveA(dayType, testOutputHelper, isTest);
            actualB = SolveB(dayType, testOutputHelper, isTest);
        }
        
        watch.Stop();
        var testText = isTest ? " test" : string.Empty;
        testOutputHelper.WriteLine($"{dayType.FullName}{testText} runtime: {watch.ElapsedMilliseconds}ms");
        
        Assert.Equal(expectedA, actualA);
        Assert.Equal(expectedB, actualB);
    }
}