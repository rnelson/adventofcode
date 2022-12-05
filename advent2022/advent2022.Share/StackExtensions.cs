namespace advent2022.Share;

public static class StackExtensions
{
	public static void MoveInOrder<T>(this Stack<T> s1, Stack<T> s2, int count)
	{
		if (s1.Count < count) throw new ArgumentOutOfRangeException(nameof(count));

		var pops = new List<T>();
		for (var i = 0; i < count; i++)
			pops.Add(s1.Pop());
		pops.Reverse();

		foreach (var t in pops)
			s2.Push(t);
	}
}