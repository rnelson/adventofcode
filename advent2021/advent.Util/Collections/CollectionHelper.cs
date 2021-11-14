namespace advent.Util.Collections
{
    public static class CollectionHelper
    {
        public static ICollection<string> ToStringCollection(this int[] numbers)
        {
            if (numbers == null) return new List<string>();

            var result = new List<string>();
            foreach (var item in numbers)
            {
                result.Add(item.ToString());
            }

            return result;
        }
    }
}
