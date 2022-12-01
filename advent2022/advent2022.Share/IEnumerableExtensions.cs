namespace advent2022.Share;

public static class IEnumerableExtensions
{
    public static IEnumerable<IEnumerable<T>> GroupByDivider<T>(this IEnumerable<T> list, T divider)
    {
        var results = new List<T>();

        foreach (var item in list)
        {
            if (item?.Equals(divider) ?? false)
            {
                yield return results;
                results = new List<T>();
            }
            else
                results.Add(item);
        }

        yield return results;
    }
}