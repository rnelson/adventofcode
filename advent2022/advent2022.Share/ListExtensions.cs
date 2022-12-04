namespace advent2022.Share;

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