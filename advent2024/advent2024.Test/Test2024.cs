using Libexec.Advent;

namespace advent2024.Test;

public class Test2024
{
    [Theory]
    [InlineData(typeof(Day01), "11", "31", true)]
    [InlineData(typeof(Day01), "2378066", "18934359", false)]
    [InlineData(typeof(Day02), "2", "4", true)]
    [InlineData(typeof(Day02), "510", "553", false)]
    public void RunTests(Type dayType, string expectedA, string expectedB, bool isTest)
    {
        var day = GetDay(dayType, isTest);
        var (actualA, actualB) = day.Solve();
        
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