using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using advent.Exceptions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day10 : Day
    {
        public Day10() : base(10)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            #region Test data
            var text = new List<string>
            {
            };
            #endregion Test data

            return false;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = 0;
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = 0;
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static long Solve(IEnumerable<string> data)
        {
            throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}