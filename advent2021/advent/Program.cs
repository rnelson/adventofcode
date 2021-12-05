using System.Globalization;
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

            day.LoadInput();
            day.Header();

            AnsiConsole.MarkupLine("Part A:");
            AnsiConsole.MarkupLine($"\t[bold yellow]{day.PartA()}[/]");

            AnsiConsole.MarkupLine("Part B:");
            AnsiConsole.MarkupLine($"\t[bold yellow]{day.PartB()}[/]");
        }

        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
        private static Day? CreateDay(int dayNumber)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().FirstOrDefault(t => t.Name == $"Day{dayNumber.ToString().PadLeft(2, '0')}");

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
