using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day01Test
{
    private string[] SampleInput =
    {
        "1000",
        "2000",
        "3000",
        "",
        "4000",
        "",
        "5000",
        "6000",
        "",
        "7000",
        "8000",
        "9000", 
        "",
        "10000",
        ""
    };
    
    [TestMethod]
    public void SampleA()
    {
        const int solution = 24000;

        var day = new Day01();
        Assert.AreEqual(solution, day.A(SampleInput));
    }
    
    [TestMethod]
    public void SampleB()
    {
        const int solution = 45000;

        var day = new Day01();
        Assert.AreEqual(solution, day.B(SampleInput));
    }
    [TestMethod]
    public void SolutionA()
    {
        var day = new Day01();

        var input = day.LoadInput();
        const int solution = 64929;

        Assert.AreEqual(solution, day.A(input));
    }
    
    [TestMethod]
    public void SolutionB()
    {
        var day = new Day01();

        var input = day.LoadInput();
        const int solution = 193697;

        Assert.AreEqual(solution, day.B(input));
    }
}