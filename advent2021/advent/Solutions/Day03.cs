using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using advent.Util.Exceptions;
using advent.Util.Extensions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day03 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day03() { }

        /// <inheritdoc/>
        public Day03(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var list = Data.ToList(); 
            var gamma = string.Empty;
            
            for (var i = 0; i < list[0].Length; i++)
            {
                gamma += list.Count(num => num[i] == '0') > list.Count(num => num[i] == '1') ? "0" : "1";
            }

            var epsilon = gamma.Replace("1", "2").Replace("0", "1").Replace("2", "0");

            return Convert.ToInt64(gamma, 2) * Convert.ToInt64(epsilon, 2);
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            var list = Data.ToList();

            var o2Candidates = new List<string>(list);
            var co2Candidates = new List<string>(list);

            for (var i = 0; i < list[0].Length; i++)
            {
                var countZero = o2Candidates.Count(num => num[i] == '0');
                var countOne = o2Candidates.Count(num => num[i] == '1');

                if (countZero > countOne)
                {
                    // 1s are less common, remove them
                    if (o2Candidates.Count > 1)
                        foreach (var bin in list.Where(num => num[i] == '1'))
                            o2Candidates.Remove(bin);
                }
                else if (countZero < countOne)
                {
                    // 0s are less common, remove them
                    if (o2Candidates.Count > 1)
                        foreach (var bin in list.Where(num => num[i] == '0'))
                            o2Candidates.Remove(bin);
                }
                else
                {
                    // Equal, keep 1s
                    if (o2Candidates.Count > 1)
                        foreach (var bin in list.Where(num => num[i] == '0'))
                            o2Candidates.Remove(bin);
                }

                countZero = co2Candidates.Count(num => num[i] == '0');
                countOne = co2Candidates.Count(num => num[i] == '1');

                if (countZero > countOne)
                {
                    // 0s are less common, keep them
                    if (co2Candidates.Count > 1)
                        foreach (var bin in list.Where(num => num[i] == '0'))
                            co2Candidates.Remove(bin);
                }
                else if (countZero < countOne)
                {
                    // 1s are less common, keep them
                    if (co2Candidates.Count > 1)
                        foreach (var bin in list.Where(num => num[i] == '1'))
                            co2Candidates.Remove(bin);
                }
                else
                {
                    // Equal, keep 0s
                    if (co2Candidates.Count > 1)
                        foreach (var bin in list.Where(num => num[i] == '1'))
                            co2Candidates.Remove(bin);
                }
            }

            if (o2Candidates.Count != 1)
                throw new BadDataException($"expected 1 O2 candidate, have {o2Candidates.Count}");
            if (co2Candidates.Count != 1)
                throw new BadDataException($"expected 1 CO2 candidate, have {co2Candidates.Count}");

            return Convert.ToInt64(o2Candidates[0], 2) * Convert.ToInt64(co2Candidates[0], 2);
        }
        #endregion Day Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        private static long Solve(IEnumerable<int> input)
        {
            throw new AnswerNotFoundException();
        }

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