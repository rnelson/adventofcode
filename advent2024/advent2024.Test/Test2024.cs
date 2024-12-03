using Libexec.Advent;

namespace advent2024.Test;

/// <summary>
/// 2024 test file
/// </summary>
public class Test2024
{
    /// <summary>
    /// Run all tests
    /// </summary>
    /// <param name="dayType">the <see cref="Type"/> for the <see cref="Day"/> to test</param>
    /// <param name="expectedA">expected answer for part A</param>
    /// <param name="expectedB">expected answer for part B</param>
    /// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data</param>
    [Theory]
    [InlineData(typeof(Day01), "11", "31", true)]
    [InlineData(typeof(Day01), "2378066", "18934359", false)]
    [InlineData(typeof(Day02), "2", "4", true)]
    [InlineData(typeof(Day02), "510", "553", false)]
    [InlineData(typeof(Day03), "", "", true)]
    [InlineData(typeof(Day03), "", "", false)]
    public void RunTests(Type dayType, string expectedA, string expectedB, bool isTest)
    {
        var (actualA, actualB) = (GetDay(dayType, isTest)).Solve();
        
        Assert.Equal(expectedA, actualA);
        Assert.Equal(expectedB, actualB);
    }

    private static Day GetDay(Type dayType, bool isTest)
    {
        if (Activator.CreateInstance(dayType, [isTest]) is not Day day)
            throw new Exception($"unable to instantiate type {dayType.FullName}");

        return day;
    }
}