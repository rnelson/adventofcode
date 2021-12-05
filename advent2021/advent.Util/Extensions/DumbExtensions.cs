namespace advent.Util.Extensions
{
    public static class DumbExtensions
    {
        public static long Count(this int[,] array, Func<int, bool> predicate)
        {
            if (predicate is null)
                throw new ArgumentNullException(nameof(predicate));

            var count = 0L;

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (predicate(array[i, j]))
                        count++;

            return count;
        }
    }
}
