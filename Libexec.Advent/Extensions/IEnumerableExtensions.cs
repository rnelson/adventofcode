using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Libexec.Advent.Extensions;

/// <summary>
/// Extensions to <see cref="IEnumerable{T}"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public static class IEnumerableExtensions
{
    /// <summary>
    /// Gets <paramref name="input"/> as a list of <typeparamref name="T"/>s.
    /// </summary>
    /// <param name="input">The enumerable string list to convert to a list of <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The numeric type to parse input as.</typeparam>
    /// <returns>The parsed inputs.</returns>
    public static IList<T> GetNumbers<T>(this IEnumerable<string> input)
        where T: INumber<T> =>
        input.Select(s => T.Parse(s, CultureInfo.CurrentCulture)).ToList();
    
    /// <summary>
    /// Groups this collection of data, separating the lists each time <paramref name="divider"/> is found.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="divider">The delimiter.</param>
    /// <typeparam name="T">The type of data in the list.</typeparam>
    /// <returns>A series of sub-lists of this list, grouped by <paramref name="divider"/>.</returns>
    public static IEnumerable<IEnumerable<T>> GroupByDivider<T>(this IEnumerable<T> list, T divider)
    {
        var results = new List<T>();

        foreach (var item in list)
        {
            if (item?.Equals(divider) ?? false)
            {
                yield return results;
                results = [];
            }
            else
                results.Add(item);
        }

        yield return results;
    }

    /// <summary>
    /// Determines whether this list only contains ascending <typeparamref name="T"/>s.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <typeparam name="T">The type of <see cref="INumber{TSelf}"/>s in the list.</typeparam>
    /// <returns><c>true</c> if the list is ascending, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// Two equivalent values are considered ascending if next to each other.
    /// </remarks>
    public static bool IsAscending<T>(this IEnumerable<T> list) where T : INumber<T>
    {
        var array = list.ToArray();
        
        var sorted = array.Order();
        return array.IsEqualTo(sorted);
    }

    /// <summary>
    /// Determines whether this list only contains descending <typeparamref name="T"/>s.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <typeparam name="T">The type of <see cref="INumber{TSelf}"/>s in the list.</typeparam>
    /// <returns><c>true</c> if the list is descending, otherwise <c>false</c>.</returns>
    /// <remarks>
    /// Two equivalent values are considered descending if next to each other.
    /// </remarks>
    public static bool IsDescending<T>(this IEnumerable<T> list) where T : INumber<T>
    {
        var array = list.ToArray();
        
        var sorted = array.OrderDescending();
        return array.IsEqualTo(sorted);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="other">The other list.</param>
    /// <typeparam name="T">The type of data in the lists.</typeparam>
    /// <returns><c>true</c> if the elements in the two lists are the same as determined by <see cref="Object.Equals(Object)"/>.</returns>
    public static bool IsEqualTo<T>(this IEnumerable<T> list, IEnumerable<T> other) where T : INumber<T>
    {
        try
        {
            var array1 = list.ToArray();
            var array2 = other.ToArray();

            return !array1.Where((value, index) => !value.Equals(array2[index])).Any();
        }
        catch
        {
            return false;
        }
    }
}