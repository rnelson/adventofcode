using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Libexec.Advent.Extensions;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class StringExtensions
{
    public static (string, string) Halve(this string s) => (s[..(s.Length / 2)], s[^(s.Length / 2)..]);
    
    public static string Intersection(this string s1, string s2)
    {
        var s1Chars = s1.Distinct().Select(c => c.ToString()).ToArray();
        var s2Chars = s2.Distinct().Select(c => c.ToString()).ToArray();

        var intersection = s1Chars.Intersect(s2Chars, StringComparer.Ordinal);
        return string.Join(string.Empty, intersection);
    }

    public static IEnumerable<T> ParseMany<T>(this string s, char delimiter = ' ') where T : INumber<T> => s
        .Split(delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(b => T.Parse(b, CultureInfo.CurrentCulture));

    public static IEnumerable<string> SlidingWindows(this string s, int size)
    {
        if (s.Length < size)
            yield return s;
        else
        {
            for (var i = 0; i < s.Length - size; i++)
                yield return s.Substring(i, size);
        } 
    }
}