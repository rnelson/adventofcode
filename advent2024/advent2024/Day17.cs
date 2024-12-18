using System.Diagnostics.CodeAnalysis;
using advent2024.ChronospatialComputer;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 17.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day17(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 17, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var computer = Parse();
        computer.Execute();
        
        return computer.GetOutput();
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return "";
    }

    private Computer Parse()
    {
        var lines = Input.Select(l => l.Trim()).ToArray();
        
        var computer = new Computer
        {
            A = int.Parse(lines[0].Split(": ")[1].Trim()),
            B = int.Parse(lines[1].Split(": ")[1].Trim()),
            C = int.Parse(lines[2].Split(": ")[1].Trim())
        };
        
        var program = lines[4].Split(": ")[1].Trim().Split(",").Select(int.Parse).ToArray();
        computer.AddInstructions(program);

        return computer;
    }
}