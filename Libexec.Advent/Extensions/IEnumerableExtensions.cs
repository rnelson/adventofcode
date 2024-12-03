using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Libexec.Advent.Extensions;

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

    public static bool IsAscending<T>(this IEnumerable<T> list) where T : INumber<T>
    {
        var array = list.ToArray();
        
        var sorted = array.Order();
        return array.IsEqualTo(sorted);
    }

    public static bool IsDescending<T>(this IEnumerable<T> list) where T : INumber<T>
    {
        var array = list.ToArray();
        
        var sorted = array.OrderDescending();
        return array.IsEqualTo(sorted);
    }

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