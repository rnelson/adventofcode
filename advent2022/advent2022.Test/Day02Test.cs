using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day02Test
{
    private string[] SampleInput =
    {
        ""
    };
    
    [TestMethod]
    public void SampleA()
    {
        const int solution = 0;

        var day = new Day01();
        Assert.AreEqual(solution, day.A(SampleInput));
    }
    
    [TestMethod]
    public void SampleB()
    {
        const int solution = 0;

        var day = new Day01();
        Assert.AreEqual(solution, day.B(SampleInput));
    }
    
    [TestMethod]
    public void SolutionA()
    {
        var day = new Day02();

        var input = day.LoadInput();
        const int solution = 0;

        Assert.AreEqual(solution, day.A(input));
    }
    
    [TestMethod]
    public void SolutionB()
    {
        var day = new Day02();

        var input = day.LoadInput();
        const int solution = 0;

        Assert.AreEqual(solution, day.B(input));
    }
}