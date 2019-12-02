using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day1 : Day
    {
        public Day1()
        {
            DayNumber = 1;
            LoadInput();
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            var total = DataAsInts.Sum(DetermineFuel);

            return new List<string> { $"Total fuel: {total}" };
        }

        protected override ICollection<string> DoPart2()
        {
            var total = DataAsInts.Sum(DetermineFuelRecursive);

            return new List<string> {$"Total fuel: {total}"};
        }
        #endregion IDay Members

        #region Private Methods
        private int DetermineFuel(int mass)
        {
            var a = mass / 3.0;
            var b = Math.Floor(a);
            var c = int.Parse(b.ToString(Culture), Culture.NumberFormat);
            var d = c - 2;

            return int.Parse(d.ToString(Culture), Culture.NumberFormat);
        }

        private int DetermineFuelRecursive(int mass)
        {
            var fuel = DetermineFuel(mass);
            
            if (fuel < 0)
                return 0;
            
            return fuel + DetermineFuelRecursive(fuel);
        }
        #endregion Private Methods
    }
}
