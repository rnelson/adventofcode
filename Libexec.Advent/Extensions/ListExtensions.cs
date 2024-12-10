using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

/// <summary>
/// Extensions to <see cref="List{T}"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class ListExtensions
{
    /// <summary>
    /// Returns a subset of all pairs of data in the list.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <typeparam name="T">The type of data in the list.</typeparam>
    /// <returns>A weird set of pairs.</returns>
    /// <remarks>
    /// This was written for a specific puzzle. You probably don't want to use this.
    /// </remarks>
    public static List<Tuple<T, T>> Pairs<T>(this List<T> list)
    {
        var result = new List<Tuple<T, T>>();
		
        for (var i = 0; i < list.Count; i++)
        for (var j = i + 1; j < list.Count; j++)
            result.Add(new(list.ElementAt(i), list.ElementAt(j)));

        return result;
    }
}