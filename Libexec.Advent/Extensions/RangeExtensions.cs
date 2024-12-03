using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class RangeExtensions
{
    public static bool Contains(this Range r1, Range r2) => r1.Start.Value <= r2.Start.Value && r1.End.Value >= r2.End.Value;

    public static bool Overlaps(this Range r1, Range r2) =>
        r1.Start.Value <= r2.End.Value && r1.End.Value >= r2.Start.Value &&
        r2.Start.Value <= r1.End.Value && r2.End.Value >= r1.Start.Value;
}