using System.Diagnostics.CodeAnalysis;
using advent.Util.Exceptions;
using Combinatorics.Collections;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    public class Day01 : Day
    {
        #region Constructors
        /// <inheritdoc/>
        public Day01() : base() { }

        /// <inheritdoc/>
        public Day01(IEnumerable<string> data) : base(data) { }
        #endregion Constructors

        #region Day Members
        /// <inheritdoc/>
        public override object PartA()
        {
            var product = Solve(DataAsInts);
            return product;
        }

        /// <inheritdoc/>
        public override object PartB()
        {
            var product = Solve(DataAsInts, 3);
            return product;
        }
        #endregion Day Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        private static long Solve(IEnumerable<int> input, int count = 2, long sum = 2020)
        {
            var combinations = new Combinations<int>(input.ToList(), count);

            foreach (var c in combinations)
            {
                if (c.Sum() == sum)
                {
                    return c.Aggregate(1, (x, y) => x * y);
                }
            }
            
            throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}
