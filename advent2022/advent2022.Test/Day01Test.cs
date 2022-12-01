using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day01Test
{
    private readonly IDay sample = new Day01(); 
    private readonly IDay real = new Day01();
    private object? sampleA;
    private object? sampleB;
    private object? realA;
    private object? realB;

    [TestInitialize]
    public void Setup()
    {
        #region Load sample and real input data
        var sampleInput = new[]
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
        sample.Input = sampleInput;

        var realInput = real.LoadInput();
        real.Input = realInput;
        #endregion Load sample and real input data

        // Solve all four problems at once
        (sampleA, sampleB) = sample.Solve();
        (realA, realB) = real.Solve();
    }
    
    [TestMethod]
    public void SampleA()
    {
        const int solution = 24000;
        Assert.AreEqual(solution, sampleA);
    }
    
    [TestMethod]
    public void SampleB()
    {
        const int solution = 45000;
        Assert.AreEqual(solution, sampleB);
    }
    
    [TestMethod]
    public void SolutionA()
    {
        const int solution = 64929;
        Assert.AreEqual(solution, realA);
    }
    
    [TestMethod]
    public void SolutionB()
    {
        const int solution = 193697;
        Assert.AreEqual(solution, realB);
    }
}