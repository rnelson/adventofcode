using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

/// <summary>
/// Extensions to <see cref="int"/>.
/// </summary>
[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class IntExtensions
{
    /// <summary>
    /// Increments this integer. If the new value exceeds <paramref name="maximum"/>, it will
    /// circle around and become <paramref name="minimum"/>.
    /// </summary>
    /// <param name="value">The integer.</param>
    /// <param name="minimum">The minimum value for the integer.</param>
    /// <param name="maximum">The maximum value for the integer.</param>
    /// <returns>The incremented integer.</returns>
    public static int CircularIncrement(this int value, int minimum, int maximum) => value + 1 <= maximum ? value + 1 : minimum;
	
    
    /// <summary>
    /// Decrements this integer. If the new value is smaller than <paramref name="minimum"/>, it will
    /// circle around and become <paramref name="maximum"/>.
    /// </summary>
    /// <param name="value">The integer.</param>
    /// <param name="minimum">The minimum value for the integer.</param>
    /// <param name="maximum">The maximum value for the integer.</param>
    /// <returns>The decremented integer.</returns>
    public static int CircularDecrement(this int value, int minimum, int maximum) => value - 1 >= minimum ? value - 1 : maximum;
}