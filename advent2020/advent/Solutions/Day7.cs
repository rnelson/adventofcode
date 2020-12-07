using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using advent.Exceptions;
using JetBrains.Annotations;
using Spectre.Console;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day7 : Day
    {
        private const string bagExpression = @"(.+) bags contain (no other bags\.|((\d+) (.+) bags?, )*(\d+) (.+) bags?\.)";
        
        public Day7() : base(7)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            #region Test data
            var text = new List<string>
            {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags."
            };
            #endregion Test data

            var countA = Solve(text);

            return countA == 4;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data);
            return new List<string> {$"[bold yellow]{answer}[/] bag colors contain ast least one [gold3_1]shiny gold[/] bag"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = 0; //Solve(Data);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int Solve(IEnumerable<string> data, string requestedChild = "shiny gold")
        {
            var entries = new Dictionary<string, IEnumerable<(int, string)>>();

            foreach (var line in data)
            {
                var (key, value) = Parse(line);
                entries[key] = value;
            }

            var parents = FindChild(entries, "shiny gold");
            return parents.Count();

            //return FindChild(entries, "shiny gold").Count();

            /*
            foreach (var (key, value) in entries)
            {
                AnsiConsole.MarkupLine($"[turquoise2 underline]{key}[/]");

                if (!value.Any())
                {
                    AnsiConsole.WriteLine("\t0 bags");
                }
                else
                {
                    foreach (var (count, type) in value)
                    {
                        AnsiConsole.WriteLine($"\t{count} {type} bag(s)");
                    }
                }
            }
            */
        }

        private static IEnumerable<string> FindChild(IDictionary<string, IEnumerable<(int, string)>> data, string search)
        {

            if (data.Count < 1)
                return new List<string>();

            var results = new List<string>();

            foreach (var (dKey, dValue) in data)
            {
                if (ContainsChild(data, search, dKey))
                    results.Add(dKey);
            }

            return results;
        }

        private static bool ContainsChild(IDictionary<string, IEnumerable<(int, string)>> data, string child,
            string parent)
        {
            #region Validation
            if (data is null || data.Count < 1)
                throw new BadDataException($"empty dictionary");
            if (string.IsNullOrWhiteSpace(parent))
                throw new ArgumentException("need a value", nameof(parent));
            if (string.IsNullOrWhiteSpace(child))
                throw new ArgumentException("need a value", nameof(child));
            
            if (!data.ContainsKey(parent))
                throw new BadDataException($"dictionary does not contain key \"{parent}\"");
            #endregion Validation

            var result = false;
            var contents = data[parent];

            foreach (var (count, type) in contents)
            {
                if (child.Equals(type, StringComparison.Ordinal))
                {
                    return true;
                }
                
                result = result || ContainsChild(data, child, type);
            }
            
            return result;
        }

        private static (string, IEnumerable<(int, string)>) Parse(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
                throw new BadDataException();
            
            var r = new Regex(bagExpression);
            var m = r.Match(line);
            
            if (!m.Success)
                throw new BadDataException(line);

            var bagType = m.Groups[1].Value;
            var childTypes = new List<(int, string)>();
            var startIndex = 6;

            if ("no other bags.".Equals(m.Groups[2].Value))
            {
                // That's it
                return (bagType, childTypes);
            }
            else if (m.Groups[2].Value.Contains(","))
            {
                // Multiple child bag types
                startIndex = 4;
            }

            for (var i = startIndex; i < m.Groups.Count; i += 2)
            {
                var count = int.Parse(m.Groups[i].Value);
                var type = m.Groups[i + 1].Value;

                childTypes.Add((count, type));
            }

            var result = (bagType, childTypes);
            return result;
        }
        #endregion Private Methods
    }
}