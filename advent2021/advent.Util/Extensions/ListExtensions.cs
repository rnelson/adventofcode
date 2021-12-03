namespace advent.Util.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<object> Duplicate(this IEnumerable<object> values) => values.Select(o => o).ToList();

        public static IList<object> DuplicateList(this IEnumerable<object> values) => values.Select(o => o).ToList();

        public static List<string> DumbCopyList(this IEnumerable<string> values) => values.ToList();
    }
}
