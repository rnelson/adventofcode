namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day02 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day02() { }

        /// <inheritdoc/>
        public Day02(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var list = Data.ToList();
            long x = 0;
            long d = 0;

            foreach (var i in list)
            {
                var t = Parse(i);

                switch (t.Item1)
                {
                    case Direction.Down:
                        d += t.Item2;
                        break;
                    case Direction.Up:
                        d -= t.Item2;
                        break;
                    case Direction.Forward:
                        x += t.Item2;
                        break;
                }
            }

            return d * x;
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            var list = Data.ToList();
            long x = 0;
            long d = 0;
            long aim = 0;

            foreach (var i in list)
            {
                var t = Parse(i);

                switch (t.Item1)
                {
                    case Direction.Down:
                        aim += t.Item2;
                        break;
                    case Direction.Up:
                        aim -= t.Item2;
                        break;
                    case Direction.Forward:
                        x += t.Item2;
                        d += (aim * t.Item2);
                        break;
                }
            }

            return d * x;
        }
        #endregion Day Members

        #region Private Methods
        private static Tuple<Direction, int> Parse(string input)
        {
            const string rex = @"(\w+) (\d+)";

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("invalid input", nameof(input));

            var r = new Regex(rex);
            var match = r.Match(input);

            if (!match.Success)
                throw new ArgumentException($"input \"{input}\" does not match expression \"{rex}\"");

            if (!Enum.TryParse(typeof(Direction), match.Groups[1].Value.Capitalize(), out var d) || d is null)
                throw new BadDataException($"unknown direction: {match.Groups[1].Value.Capitalize()}");

            return new Tuple<Direction, int>((Direction)d, int.Parse(match.Groups[2].Value));
        }
        #endregion Private Methods

        private enum Direction { Up, Down, Forward }
    }
}