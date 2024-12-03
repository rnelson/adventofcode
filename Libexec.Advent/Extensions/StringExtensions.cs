using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;

namespace Libexec.Advent.Extensions;

/// <summary>
/// Extensions to <see cref="string"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class StringExtensions
{
    /// <summary>
    /// Splits the string in half.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <returns>A tuple containing each half of the string.</returns>
    public static (string, string) Halve(this string s) => (s[..(s.Length / 2)], s[^(s.Length / 2)..]);
    
    /// <summary>
    /// Determines the common characters between this string and another.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <param name="other">The other string.</param>
    /// <returns>A string containing the common characters between the two strings.</returns>
    public static string Intersection(this string s, string other)
    {
        var s1Chars = s.Distinct().Select(c => c.ToString()).ToArray();
        var s2Chars = other.Distinct().Select(c => c.ToString()).ToArray();

        var intersection = s1Chars.Intersect(s2Chars, StringComparer.Ordinal);
        return string.Join(string.Empty, intersection);
    }

    /// <summary>
    /// Parses many <typeparamref name="T"/>s out of this string.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <param name="delimiter">The delimiter (by default a space) between the <typeparamref name="T"/>s.</param>
    /// <typeparam name="T">The <see cref="INumber{TSelf}"/> to parse out of the string.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> of <typeparamref name="T"/>s.</returns>
    public static IEnumerable<T> ParseMany<T>(this string s, char delimiter = ' ') where T : INumber<T> => s
        .Split(delimiter, StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
        .Select(b => T.Parse(b, CultureInfo.CurrentCulture));

    /// <summary>
    /// Returns substrings of the string of up to <paramref name="size"/> characters.
    /// </summary>
    /// <param name="s">The string.</param>
    /// <param name="size">Maximum window size.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of substrings.</returns>
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