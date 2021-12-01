using System.Diagnostics.CodeAnalysis;
using advent.Util.Exceptions;
using JetBrains.Annotations;
using MoreLinq;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day01 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day01() { }

        /// <inheritdoc/>
        public Day01(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var list = DataAsInts.ToList();
            var pairs = list.Zip(list.Skip(1), Tuple.Create);

            return pairs.Count(p => p.Item1 < p.Item2);
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            const int windowSize = 3;

            var list = DataAsInts.ToList();
            var windows = list.Window(windowSize).Select(w => w.Sum());
            var pairs = windows.Zip(windows.Skip(1), Tuple.Create);

            return pairs.Count(p => p.Item1 < p.Item2);
        }
        #endregion Day Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        private static long Solve(IEnumerable<int> input, int windowSize = 1)
        {
            throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}
