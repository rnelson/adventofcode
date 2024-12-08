using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 8.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day08(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 8, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        return "";
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return "";
    }
}