using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    internal class Day4 : Day
    {
        public Day4() : base(4)
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
            var answer = 0; //Solve(Process(Data), 3, 1);
            return new List<string> {$"[bold yellow]{answer}[/] valid passports"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = 0; //slopes.Aggregate(1, (a, b) => a * b);
            return new List<string> {$"[bold yellow]{answer}[/] valid passports"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int Solve()
        {
            return 0;
        }
        #endregion Private Methods
    }
}