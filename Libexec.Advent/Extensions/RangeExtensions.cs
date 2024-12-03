using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

/// <summary>
/// Extensions to <see cref="Range"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class RangeExtensions
{
    /// <summary>
    /// Determines if this range contains (inclusive) <paramref name="other"/>.
    /// </summary>
    /// <param name="r">This range.</param>
    /// <param name="other">The other range.</param>
    /// <returns><c>true</c> if <paramref name="r"/> contains <paramref name="other"/>, otherwise <c>false</c>.</returns>
    public static bool Contains(this Range r, Range other) => r.Start.Value <= other.Start.Value && r.End.Value >= other.End.Value;

    /// <summary>
    /// Determines if this range overlaps <paramref name="other"/>.
    /// </summary>
    /// <param name="r">This range.</param>
    /// <param name="other">The other range.</param>
    /// <returns><c>true</c> if <paramref name="r"/> overlaps <paramref name="other"/>, otherwise <c>false</c>.</returns>
    public static bool Overlaps(this Range r, Range other) =>
        r.Start.Value <= other.End.Value && r.End.Value >= other.Start.Value &&
        other.Start.Value <= r.End.Value && other.End.Value >= r.Start.Value;
}