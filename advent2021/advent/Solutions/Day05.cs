namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day05 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day05() { }

        /// <inheritdoc/>
        public Day05(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            (var vents, _, var max) = ReadInput(Data);
            var lines = vents.Where(v => v.IsHorizontal || v.IsVertical).ToList();
            var floor = new int[max+1, max+1];

            foreach (var line in lines)
            {
                var minX = line.X1 < line.X2 ? line.X1 : line.X2;
                var maxX = line.X1 > line.X2 ? line.X1 : line.X2;
                var minY = line.Y1 < line.Y2 ? line.Y1 : line.Y2;
                var maxY = line.Y1 > line.Y2 ? line.Y1 : line.Y2;

                for (var i = minX; i <= maxX; i++)
                    for (var j = minY; j <= maxY; j++)
                        floor[i, j]++;
            }

            return floor.Count(n => n > 1);
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            (var vents, _, var max) = ReadInput(Data);
            var lines = vents.Select(s => new Line(s)).ToList();
            var floor = new int[max + 1, max + 1];

            foreach (var line in lines)
            {
                foreach ((var x, var y) in line.GetPoints())
                    floor[x, y]++;
            }

            return floor.Count(n => n > 1);
        }
        #endregion Day Members

        #region Private Methods
        private Tuple<IEnumerable<Vent>, int, int> ReadInput(IEnumerable<string> inputs)
        {
            const string rex = @"(-?\d+),(-?\d+) -> (-?\d+),(-?\d+)";

            var vents = new List<Vent>();
            int min = 0, max = 0;

            foreach (var input in inputs)
            {
                if (string.IsNullOrWhiteSpace(input)) continue;

                var r = new Regex(rex);
                var match = r.Match(input);

                if (!match.Success)
                    throw new ArgumentException($"input \"{input}\" does not match expression \"{rex}\"", nameof(inputs));

                var x1 = int.Parse(match.Groups[1].Value);
                var y1 = int.Parse(match.Groups[2].Value);
                var x2 = int.Parse(match.Groups[3].Value);
                var y2 = int.Parse(match.Groups[4].Value);

                if (x1 < min) min = x1; if (x1 > max) max = x1;
                if (y1 < min) min = y1; if (y1 > max) max = y1;
                if (x2 < min) min = x2; if (x2 > max) max = x2;
                if (y2 < min) min = y2; if (y2 > max) max = y2;

                vents.Add(new Vent(x1, y1, x2, y2, x1 == x2, y1 == y2));
            }

            return new Tuple<IEnumerable<Vent>, int, int>(vents, min, max);
        }
        #endregion Private Methods

        #region Classes
        private record Vent(int X1, int Y1, int X2, int Y2, bool IsHorizontal, bool IsVertical);

        private class Line
        {
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }

            public bool IsHorizontal => X1 == X2;
            public bool IsVertical => Y1 == Y2;
            public bool IsDiagonal => !IsHorizontal && !IsVertical;

            private int Slope => Y1 < Y2 ? 1 : -1;

            public Line(Vent v)
            {
                X1 = v.X1;
                X2 = v.X2;
                Y1 = v.Y1;
                Y2 = v.Y2;
            }

            public IEnumerable<(int, int)> GetPoints()
            {
                var points = new List<(int, int)>();

                if (IsHorizontal || IsVertical)
                {
                    var minX = X1 < X2 ? X1 : X2;
                    var maxX = X1 > X2 ? X1 : X2;
                    var minY = Y1 < Y2 ? Y1 : Y2;
                    var maxY = Y1 > Y2 ? Y1 : Y2;

                    for (var i = minX; i <= maxX; i++)
                        for (var j = minY; j <= maxY; j++)
                            points.Add((i, j));
                }
                else
                {
                    var startX = X1 < X2 ? X1 : X2;
                    var endX = X1 > X2 ? X1 : X2;
                    var startY = X1 < X2 ? Y1 : Y2;
                    var endY = X1 > X2 ? Y1 : Y2;

                    var slope = startY < endY ? 1 : -1;

                    var xPoints = Enumerable.Range(startX, endX - startX + 1);
                    var y = startY;

                    foreach (var x in xPoints)
                    {
                        points.Add((x, y));
                        y += slope;
                    }
                }

                return points;
            }
        }
        #endregion Classes
    }
}