namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day06 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day06() { }

        /// <inheritdoc/>
        public Day06(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var school = Data.First().Split(',').Select(long.Parse).ToList();
            return Solve(school, 80);
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            var school = Data.First().Split(',').Select(long.Parse).ToList();
            return Solve(school, 256);
        }
        #endregion Day Members

        #region Private Methods
        private long Solve(IEnumerable<long> school, int days)
        {
            var schoolList = school.ToList();
            var fishes = new Dictionary<int, long>();

            // Figure out how many fish are at each timer value
            for (var i = 0; i < 9; i++)
                fishes[i] = schoolList.Count(n => n == i);

            for (var i = 0; i < days; i++)
            {
                // Figure out how many new fishies there are
                var babies = fishes[0];

                // Decrease the timers
                for (var j = 0; j < 8; j++)
                    fishes[j] = fishes[j + 1];

                // All new parents are reset to 6
                fishes[6] += babies;

                // All babies are set to 8
                fishes[8] = babies;
            }

            return fishes.Sum(kvp => kvp.Value);
        }
        #endregion Private Methods

        #region Classes
        #endregion Classes
    }
}