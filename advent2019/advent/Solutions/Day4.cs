using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day4 : Day
    {
        public Day4()
        {
            DayNumber = 4;
            LoadInput();
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            var range = Data.First().Split('-').Select(int.Parse).ToArray();
            var lower = range[0];
            var upper = range[1];
            var values = Enumerable.Range(lower, (upper - lower) + 1).ToArray();
            var good = 0;

            bool IsValid(string password)
            {
                #region It is a six-digit number
                var result = password.Length == 6;
                #endregion It is a six-digit number

                #region The value is within the range given in your puzzle input
                result = result && (int.Parse(password, Culture.NumberFormat) >= lower) && (int.Parse(password, Culture.NumberFormat) <= upper);
                #endregion The value is within the range given in your puzzle input

                #region Two adjacent digits are the same
                var hasDouble = false;

                for (var i = 1; i < password.Length; i++)
                {
                    var left = password[i - 1].ToString(Culture);
                    var right = password[i].ToString(Culture);

                    if (left != right)
                        continue;

                    hasDouble = true;
                    break;
                }

                result = result && hasDouble;
                #endregion Two adjacent digits are the same

                #region Going from left to right, the digits never decrease; they only ever increase or stay the same
                for (var i = 1; i < password.Length; i++)
                {
                    var left = password[i - 1].ToString(Culture);
                    var right = password[i].ToString(Culture);

                    if (int.Parse(left, Culture.NumberFormat) <= int.Parse(right, Culture.NumberFormat))
                        continue;
                    
                    result = false;
                    break;
                }
                #endregion Going from left to right, the digits never decrease; they only ever increase or stay the same

                return result;
            }

            foreach (var n in values)
            {
                if (IsValid(n.ToString(Culture)))
                    good++;
            }

            return new List<string> { $"{good}" };
        }

        protected override ICollection<string> DoPart2()
        {
            return new List<string> { "Not completed" };
        }
        #endregion IDay Members

        #region Private Methods
        #endregion Private Methods
    }
}