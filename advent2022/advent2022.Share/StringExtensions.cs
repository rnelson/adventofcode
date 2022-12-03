namespace advent2022.Share;

public static class StringExtensions
{
	public static string Intersection(this string s1, string s2)
	{
		var s1Chars = s1.Distinct().Select(c => c.ToString()).ToArray();
		var s2Chars = s2.Distinct().Select(c => c.ToString()).ToArray();

		var intersection = s1Chars.Intersect(s2Chars, StringComparer.Ordinal);
		return string.Join(string.Empty, intersection);
	}

	public static (string, string) Halve(this string s) => (s[..(s.Length / 2)], s[^(s.Length / 2)..]);
}