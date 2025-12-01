using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 1.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day01(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 1, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        return 0;
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return 0;
    }
}