using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using Libexec.Advent.Collections;

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
    /// Enumerates over this collection, providing a <see cref="Tuple{T1,T2}"/> with the numeric index and the value.
    /// </summary>
    /// <param name="items">The collection to enumerate.</param>
    /// <typeparam name="T">The type of data in the collection.</typeparam>
    /// <returns>A collection of <see cref="Tuple{T1,T2}"/>s containing the index and the value.</returns>
    public static IEnumerable<Tuple<int, T>> Enumerate<T>(this IEnumerable<T> items)
    {
        var index = 0;
        foreach (var item in items)
            yield return new(index++, item);
    }
    
    /// <summary>
    /// Converts this list of strings into a character matrix.
    /// </summary>
    /// <param name="source">The list of strings.</param>
    /// <returns>A <see cref="Matrix{T}"/> of <see cref="char"/>s.</returns>
    /// <remarks>
    /// This requires all elements in <paramref name="source"/> be the same length.
    /// </remarks>
    public static Matrix<char> ToMatrix(this IEnumerable<string> source)
    {
        var data = source.ToArray();
        var width = data[0].ToCharArray().Length;
        var height = data.Length;
        var array = new char[width, height];

        for (var y = 0; y < height; y++)
        {
            var element = data[y];
            var chars = element.ToCharArray();
            
            for (var x = 0; x < width; x++)
                array[y, x] = chars[x];
        }
        
        return Matrix<char>.Create(array, width, height);
    }
    
    /// <summary>
    /// Gets <paramref name="input"/> as a list of <typeparamref name="T"/>s.
    /// </summary>
    /// <param name="input">The enumerable string list to convert to a list of <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The numeric type to parse input as.</typeparam>
    /// <returns>The parsed inputs.</returns>
    public static IList<T> GetNumbers<T>(this IEnumerable<string> input)
        where T: INumber<T> =>
        input.Select(s => T.Parse(s, CultureInfo.CurrentCulture)).ToList();
    
    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> list, int length)
    {
        var enumerated = list.ToArray();
        
        if (length == 1)
            return enumerated.Select(t => new T[] { t });

        return enumerated
            .GetPermutations(length - 1)
            .SelectMany(t => enumerated.Where(e => !t.Contains(e)),
                (t1, t2) => t1.Concat([t2]).ToArray())
            .ToArray();
    }
    
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