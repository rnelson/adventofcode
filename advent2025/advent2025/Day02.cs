using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2025 day 2.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("Performance", "CA1859:Use concrete types when possible for improved performance")]
public class Day02(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2025, 2, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA() => string.Empty;

    /// <inheritdoc/>
    public override object PartB() => string.Empty;
}