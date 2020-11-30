﻿using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;

[assembly: NeutralResourcesLanguage("en")]
namespace advent
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var culture = CultureInfo.GetCultureInfo("en-US");

            if (args is null || args.Length != 1)
                throw new InvalidOperationException("usage: advent <day>");

            var number = int.Parse(args[0], culture.NumberFormat);
            var day = CreateDay(number);

            day?.Header();
            day?.Part1();
            day?.Part2();
        }

        private static Day? CreateDay(int dayNumber)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == $"Day{dayNumber}");

            if (type is null)
            {
                Console.Error.WriteLine($"error: unknown day {dayNumber}");
                return null;
            }

            var day = (Day) Activator.CreateInstance(type)!;
            day.DayNumber = dayNumber;

            return day;
        }
    }
}
