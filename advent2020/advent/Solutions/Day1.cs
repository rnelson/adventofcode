using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Exceptions;
using Combinatorics.Collections;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day1 : Day
    {
        public Day1() : base(1)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        public override bool Test()
        {
            const long answerA = 514579;
            const long answerB = 241861950;
            
            var input = new[] {1721, 979, 366, 299, 675, 1456};
            
            var a = Solve(input) == answerA;
            var b = Solve(input, 3) == answerB;

            return a && b;
        }
        
        protected override IEnumerable<string> DoPart1()
        {
            var product = Solve(DataAsInts);
            return new List<string> {product.ToString()};
        }

        protected override IEnumerable<string> DoPart2()
        {
            var product = Solve(DataAsInts, 3);
            return new List<string> {product.ToString()};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        private static long Solve(IEnumerable<int> input, int count = 2)
        {
            var combinations = new Combinations<int>(input.ToList(), count);

            foreach (var c in combinations)
            {
                if (c.Sum() == 2020)
                {
                    return c.Aggregate(1, (x, y) => x * y);
                }
            }
            
            throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}
