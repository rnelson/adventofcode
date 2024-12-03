using System.Diagnostics.CodeAnalysis;

namespace Libexec.Advent.Extensions;

[SuppressMessage("ReSharper", "InconsistentNaming")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class IntExtensions
{
    public static int Increment(this int value, int minimum, int maximum) => value + 1 <= maximum ? value + 1 : minimum;
	
    public static int Decrement(this int value, int minimum, int maximum) => value - 1 >= minimum ? value - 1 : maximum;
}