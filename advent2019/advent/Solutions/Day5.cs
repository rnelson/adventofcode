using System.Collections.Generic;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day5 : Day
    {
        public Day5()
        {
            DayNumber = 5;
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            LoadInput();
            return new List<string> { "Not yet completed" };
        }

        protected override ICollection<string> DoPart2()
        {
            LoadInput();
            return new List<string> { "Not yet completed" };
        }
        #endregion IDay Members

        #region Private Methods
        #endregion Private Methods
    }
}