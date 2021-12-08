namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day08 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day08() { }

        /// <inheritdoc/>
        public Day08(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Properties
        private Dictionary<int, string> Mapping { get; set; } = new()
        {
            { 0, "abcefg" },
            { 1, "cf" },
            { 2, "acdeg" },
            { 3, "acdfg" },
            { 4, "bcdf" },
            { 5, "abdfg" },
            { 6, "abdefg" },
            { 7, "acf" },
            { 8, "abcdefg" },
            { 9, "abcdfg" },
        };
        #endregion Properties

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var uniques = new[] { 1, 4, 7, 8 };
            var counts = new Dictionary<string, long>();

            for (var i = 0; i < 10; i++)
                counts[i.ToString()] = 0;

            foreach (var line in Data)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                var bits = line.Split(" | ");
                var input = bits[0].Split(' ');
                var output = bits[1].Split(' ');

                foreach (var number in output)
                {
                    var candidates = Mapping.Where(kvp => kvp.Value.Length == number.Length).ToList();
                    
                    if (candidates.Count() != 1) continue;
                    counts[candidates.First().Key.ToString()]++;
                }
            }

            return uniques.Sum(unique => counts[unique.ToString()]);
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            // start with 1, 4, 7, 8 and assume they are in the same order in
            // input as on the page? bea is my 3 letter (I think), which means
            // it is 7. b is top top, e is top right, a is bottom right. Use
            // those to sort out all of the locations?
            
            // oh no. "fd", "fb", "fc", "fg" -- each row has its own lettering
            // scheme I think?

            //var digits = new Digits();
            //
            //var inputs = string.Join(" ", Data.Select(s => s.Split(" | ")[0]).Distinct().ToList()).Split(' ').ToList();
            //inputs.Sort();
            //
            //Console.WriteLine(string.Join(", ", inputs));

            return string.Empty;
        }
        #endregion Day Members

        #region Private Methods
        #endregion Private Methods

        #region Classes
        private class Digits
        {
            public Dictionary<char, Segment> Segments { get; set; } = new();
        }
        #endregion Classes

        private enum Segment
        {
            Top,
            UpperLeft,
            UpperRight,
            Middle,
            LowerLeft,
            LowerRight,
            Bottom
        }
    }
}