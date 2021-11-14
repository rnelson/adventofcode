namespace advent.Util.Collections
{
    public static class CollectionHelper
    {
        public static IEnumerable<string> ToStringCollection(this int[] numbers)
        {
            var result = new List<string>();
            foreach (var item in numbers)
            {
                result.Add(item.ToString());
            }

            return result;
        }
    }
}
