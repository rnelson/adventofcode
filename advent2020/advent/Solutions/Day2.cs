using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using advent.Exceptions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    internal class Day2 : Day
    {
        public Day2() : base(2)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        public override bool Test()
        {
            return false;
        }
        
        protected override IEnumerable<string> DoPart1()
        {
            var answer = Solve(DataAsInts);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPart2()
        {
            var answer = Solve(DataAsInts);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        private static long Solve(IEnumerable<int> input)
        {
            throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}