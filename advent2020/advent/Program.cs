using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using Spectre.Console;

[assembly: NeutralResourcesLanguage("en")]
namespace advent
{
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var culture = CultureInfo.GetCultureInfo("en-US");

            if (args is null || args.Length != 1)
                throw new InvalidOperationException("usage: advent <day>");

            var number = int.Parse(args[0], culture.NumberFormat);
            var day = CreateDay(number);

            if (day is null)
                throw new InvalidOperationException($"unable to run day {number}");

            day!.Header();
            if (!day!.Test())
            {
                AnsiConsole.MarkupLine($"[bold red]error[/]: test(s) for day {number} failed");
                return;
            }

            day!.PartA();
            day!.PartB();
        }

        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
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
