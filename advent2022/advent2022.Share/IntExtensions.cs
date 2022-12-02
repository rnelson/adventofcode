namespace advent2022.Share;

public static class IntExtensions
{
	public static int Increment(this int value, int minimum, int maximum) => value + 1 <= maximum ? value + 1 : minimum;
	
	public static int Decrement(this int value, int minimum, int maximum) => value - 1 >= minimum ? value - 1 : maximum;
}