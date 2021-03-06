﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace advent.Helpers
{
    internal static class Math
    {
        public static int Gcd(int a, int b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            return a > b ? Gcd(a % b, b) : Gcd(a, b % a);
        }

        public static long Gcd(long a, long b)
        {
            if (a == 0) return b;
            if (b == 0) return a;

            return a > b ? Gcd(a % b, b) : Gcd(a, b % a);
        }

        public static int Lcm(int a, int b)
        {
            return a * b / Gcd(a, b);
        }

        public static long Lcm(long a, long b)
        {
            return a * b / Gcd(a, b);
        }

        public static int Lcm(int[] numbers)
        {
            if (numbers is null || numbers.Length < 2)
            {
                var culture = CultureInfo.CurrentUICulture;
                var error = string.Format(
                    culture,
                    Strings.Helpers_Math_NeedTwoValues,
                    nameof(numbers));
                throw new ArgumentOutOfRangeException(nameof(numbers), error);
            }

            return numbers.Aggregate(Lcm);
        }

        public static long Lcm(long[] numbers)
        {
            if (numbers is null || numbers.Length < 2)
            {
                var culture = CultureInfo.CurrentUICulture;
                var error = string.Format(
                    culture,
                    Strings.Helpers_Math_NeedTwoValues,
                    nameof(numbers));
                throw new ArgumentOutOfRangeException(nameof(numbers), error);
            }

            return numbers.Aggregate(Lcm);
        }
    }
}
