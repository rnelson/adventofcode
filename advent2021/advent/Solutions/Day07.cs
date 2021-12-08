namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day07 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day07() { }

        /// <inheritdoc/>
        public Day07(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var positions = Data.First().Split(',').Select(int.Parse).ToList();
            var options = positions.Distinct().ToArray();
            var weights = new Dictionary<int, int>();

            foreach (var option in options)
            {
                var weight = 0;
                foreach (var position in positions)
                    weight += Math.Abs(position - option);

                weights.Add(option, weight);
            }

            return (long)weights.Min(kvp => kvp.Value);
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            var positions = Data.First().Split(',').Select(int.Parse).ToList();
            var options = positions.Distinct().ToArray();
            var min = options.Min();
            var max = options.Max();
            var range = Enumerable.Range(min, max - min + 1).ToArray();

            int Diff(int a, int b) => Math.Abs(a - b);
            long Sum(int n) => n * (n + 1) / 2L;

            return range.Min(i => positions.Sum(j => Sum(Diff(i, j))));
        }
        #endregion Day Members
    }
}