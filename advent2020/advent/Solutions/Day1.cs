using System.Collections.Generic;
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
            return true;
        }
        
        protected override IEnumerable<string> DoPart1()
        {
            return new List<string> {"Part 1"};
        }

        protected override IEnumerable<string> DoPart2()
        {
            return new List<string> {"Part 2"};
        }
        #endregion IDay Members

        #region Private Methods
        #endregion Private Methods
    }
}
