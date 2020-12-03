using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    internal class Day3 : Day
    {
        public Day3() : base(3)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            return false;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = Solve(Data, true);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int Solve(IEnumerable<string> inputs, bool positional = false)
        {
            return 0;
        }
        #endregion Private Methods
    }
}