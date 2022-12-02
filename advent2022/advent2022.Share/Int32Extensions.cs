namespace advent2022.Share;

public static class Int32Extensions
{
	public static int Increment(this Int32 value, int minimum, int maximum) => value + 1 <= maximum ? value + 1 : minimum;
	
	public static int Decrement(this Int32 value, int minimum, int maximum) => value - 1 >= minimum ? value - 1 : maximum;
}