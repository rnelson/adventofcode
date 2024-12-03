using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class StackExtensions
{
    public static void MoveInOrder<T>(this Stack<T> s1, Stack<T> s2, int count)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, s1.Count);

        var pops = new List<T>();
        for (var i = 0; i < count; i++)
            pops.Add(s1.Pop());
        pops.Reverse();

        foreach (var t in pops)
            s2.Push(t);
    }
}