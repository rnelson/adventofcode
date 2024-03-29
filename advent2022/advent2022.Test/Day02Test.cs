﻿using advent2022.Share;
using advent2022.Solutions;

namespace advent2022.Test;

[TestClass]
public class Day02Test
{
    private readonly IDay sample = new Day02(); 
    private readonly IDay real = new Day02();
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
            "A Y",
            "B X",
            "C Z"
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
        const int solution = 15;
        Assert.AreEqual(solution, sampleA);
    }
    
    [TestMethod]
    public void SampleB()
    {
        const int solution = 12;
        Assert.AreEqual(solution, sampleB);
    }
    
    [TestMethod]
    public void SolutionA()
    {
        const int solution = 15632;
        Assert.AreEqual(solution, realA);
    }
    
    [TestMethod]
    public void SolutionB()
    {
        const int solution = 14416;
        Assert.AreEqual(solution, realB);
    }
}