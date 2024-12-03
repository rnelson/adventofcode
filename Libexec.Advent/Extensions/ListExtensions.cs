using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class ListExtensions
{
    public static List<Tuple<T, T>> Pairs<T>(this List<T> list)
    {
        var result = new List<Tuple<T, T>>();
		
        for (var i = 0; i < list.Count; i++)
        for (var j = i + 1; j < list.Count; j++)
            result.Add(new Tuple<T, T>(list.ElementAt(i), list.ElementAt(j)));

        return result;
    }
}