using System.Collections.Generic;
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
        protected new bool Test()
        {
            const long answerA = 514579;
            var inputA = new[] {1721, 979, 366, 299, 675, 1456};
            
            return SolveA(inputA) == answerA;
        }
        
        protected override IEnumerable<string> DoPart1()
        {
            var product = SolveA(DataAsInts);
            return new List<string> {product.ToString()};
        }

        protected override IEnumerable<string> DoPart2()
        {
            return new List<string> {"Part 2"};
        }
        #endregion IDay Members

        #region Private Methods
        private long SolveA(IEnumerable<int> input)
        {
            var combinations = new Combinations<int>(input.ToList(), 2);

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
