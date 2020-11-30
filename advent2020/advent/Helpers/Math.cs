using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace advent.Helpers
{
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    internal static class Math
    {
        internal static int Gcd(int a, int b)
        {
            while (true)
            {
                if (a == 0) return b;
                if (b == 0) return a;

                if (a > b)
                {
                    a %= b;
                    continue;
                }

                var a1 = a;
                b %= a1;
            }
        }

        internal static long Gcd(long a, long b)
        {
            while (true)
            {
                if (a == 0) return b;
                if (b == 0) return a;

                if (a > b)
                {
                    a %= b;
                    continue;
                }

                var a1 = a;
                b %= a1;
            }
        }

        internal static int Lcm(int a, int b)
        {
            return a * b / Gcd(a, b);
        }

        internal static long Lcm(long a, long b)
        {
            return a * b / Gcd(a, b);
        }

        internal static int Lcm(int[] numbers)
        {
            if (numbers.Length >= 2)
                return numbers.Aggregate(Lcm);
            
            throw new ArgumentOutOfRangeException(nameof(numbers), $"{nameof(numbers)} must contain at least 2 values");

        }

        internal static long Lcm(long[] numbers)
        {
            if (numbers.Length >= 2)
                return numbers.Aggregate(Lcm);
            
            throw new ArgumentOutOfRangeException(nameof(numbers), $"{nameof(numbers)} must contain at least 2 values");

        }
    }
}
