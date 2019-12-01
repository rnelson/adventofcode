using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day1 : Day
    {
        private readonly CultureInfo culture = CultureInfo.GetCultureInfo("en-US");

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            var modules = Data.Select(int.Parse).ToList();
            var total = modules.Sum(DetermineFuel);

            return new List<string>() { $"Total fuel: {total}" };
        }

        protected override ICollection<string> DoPart2()
        {
            var modules = Data.Select(int.Parse).ToList();
            var total = modules.Sum(DetermineFuelRecursive);

            return new List<string>() {$"Total fuel: {total}"};
        }
        #endregion IDay Members

        #region Private Methods
        private int DetermineFuel(int mass)
        {
            var a = mass / 3.0;
            var b = Math.Floor(a);
            var c = int.Parse(b.ToString(CultureInfo.CurrentCulture), culture.NumberFormat);
            var d = c - 2;

            return int.Parse(d.ToString(culture), culture.NumberFormat);
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
