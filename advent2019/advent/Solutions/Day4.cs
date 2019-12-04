using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            LoadInput();
            var range = Data.First().Split('-').Select(int.Parse).ToArray();
            var lower = range[0];
            var upper = range[1];
            var values = Enumerable.Range(lower, (upper - lower) + 1).ToArray();
            var good = 0;

            foreach (var n in values)
            {
                if (IsValid(n.ToString(Culture), lower, upper))
                    good++;
            }

            return new List<string> { $"{good}" };
        }

        protected override ICollection<string> DoPart2()
        {
            LoadInput();
            var range = Data.First().Split('-').Select(int.Parse).ToArray();
            var lower = range[0];
            var upper = range[1];
            var values = Enumerable.Range(lower, (upper - lower) + 1).ToArray();
            var good = 0;

            foreach (var n in values)
            {
                if (IsValid(n.ToString(Culture), lower, upper, true))
                    good++;
            }

            return new List<string> { $"{good}" };
        }
        #endregion IDay Members

        #region Private Methods
        private bool IsValid(string password, int lower, int upper, bool part2 = false)
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

            #region (Part 2) The two adjacent matching digits are not part of a larger group of matching digits
            if (part2)
            {
                var rex = @"(\d)\1+";
                var matches = Regex.Matches(password, rex);

                if (matches.FirstOrDefault(m => m.Groups[0].Value.Length == 2) is null)
                    result = false;
            }
            #endregion (Part 2) The two adjacent matching digits are not part of a larger group of matching digits

            return result;
        }
        #endregion Private Methods
    }
}