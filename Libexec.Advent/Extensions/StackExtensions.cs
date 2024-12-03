using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

/// <summary>
/// Extensions to <see cref="Stack{T}"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class StackExtensions
{
    /// <summary>
    /// Moves <paramref name="count"/> items from this stack into <paramref name="other"/>.
    /// </summary>
    /// <param name="s">The stack.</param>
    /// <param name="other">The other stack.</param>
    /// <param name="count">The number of items to move from this stack into <paramref name="other"/>.</param>
    /// <typeparam name="T">The type of data in the two stacks.</typeparam>
    public static void MoveInOrder<T>(this Stack<T> s, Stack<T> other, int count)
    {
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, s.Count);

        var pops = new List<T>();
        for (var i = 0; i < count; i++)
            pops.Add(s.Pop());
        pops.Reverse();

        foreach (var t in pops)
            other.Push(t);
    }
}