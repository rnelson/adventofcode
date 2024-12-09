using System.Diagnostics;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2024.Test;

/// <summary>
/// 2024 test file.
/// </summary>
/// <param name="testOutputHelper"><see cref="ITestOutputHelper"/> to provide output.</param>
public partial class Test2024(ITestOutputHelper testOutputHelper)
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
    [InlineData(typeof(Day01), "11", "31", true)]
    [InlineData(typeof(Day01), "2378066", "18934359")]
    [InlineData(typeof(Day02), "2", "4", true)]
    [InlineData(typeof(Day02), "510", "553")]
    [InlineData(typeof(Day03), "161", "48", true, true)]
    [InlineData(typeof(Day03), "156388521", "75920122")]
    [InlineData(typeof(Day04), "18", "9", true)]
    [InlineData(typeof(Day04), "2583", "1978")]
    [InlineData(typeof(Day05), "143", "123", true)]
    [InlineData(typeof(Day05), "4959", "4655")]
    [InlineData(typeof(Day06), "41", "6", true)]
    [InlineData(typeof(Day06), "4602", "1703")]
    [InlineData(typeof(Day07), "3749", "11387", true)]
    [InlineData(typeof(Day07), "1260333054159", "")]
    [InlineData(typeof(Day08), "14", "", true)]
    [InlineData(typeof(Day08), "", "")]
    [InlineData(typeof(Day09), "1928", "2858", true)]
    [InlineData(typeof(Day09), "6330095022244", "6359491814941")]
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