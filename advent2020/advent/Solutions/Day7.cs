using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using advent.Exceptions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    [SuppressMessage("ReSharper", "CA1307")]
    [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
    internal class Day7 : Day
    {
        private const string bagExpression = @"(.+) bags contain (no other bags\.|((\d+) (.+) bags?, )*(\d+) (.+) bags?\.)";
        private const string miniExpression = @"(\d+) (.+) bags?";
        
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
            var textA = new List<string>
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
            var textB = new List<string>
            {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags."
            };
            #endregion Test data

            var containingA = SolveA(textA);
            var insideA = SolveB(textA);
            var insideB = SolveB(textB);

            return containingA == 4 && insideA == 32 && insideB == 126;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = SolveA(Data);
            return new List<string> {$"[bold yellow]{answer}[/] bag colors contain ast least one [gold3_1]shiny gold[/] bag"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = SolveB(Data);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int SolveA(IEnumerable<string> data, string requestedChild = "shiny gold")
        {
            var entries = new Dictionary<string, IEnumerable<(int, string)>>();

            foreach (var line in data)
            {
                var (key, value) = Parse(line);
                entries[key] = value;
            }

            var parents = FindChild(entries, requestedChild);
            return parents.Count();
        }
        
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int SolveB(IEnumerable<string> data, string initialBag = "shiny gold")
        {
            var entries = new Dictionary<string, IEnumerable<(int, string)>>();

            foreach (var line in data)
            {
                var (key, value) = Parse(line);
                entries[key] = value;
            }

            var c = Count(entries, initialBag);
            return c;
        }

        private static int Count(IDictionary<string, IEnumerable<(int, string)>> data, string bag, int multiplier = 1)
        {
            var contentsEnumerable = data[bag];
            var contents = contentsEnumerable as (int, string)[] ?? contentsEnumerable.ToArray();
            
            if (!contents.Any())
                return 0;

            var sum = 0;

            foreach (var (count, type) in contents)
            {
                var children = Count(data, type, count);
                sum = sum + count + count * children;
            }

            return sum;
        }

        private static IEnumerable<string> FindChild(IDictionary<string, IEnumerable<(int, string)>> data, string search)
        {
            return data.Count < 1 ? new List<string>() : data.Keys.Where(dKey => ContainsChild(data, search, dKey)).ToList();
        }

        private static bool ContainsChild(IDictionary<string, IEnumerable<(int, string)>> data, string child,
            string parent)
        {
            #region Validation
            if (data is null || data.Count < 1)
                throw new BadDataException("empty dictionary");
            if (string.IsNullOrWhiteSpace(parent))
                throw new ArgumentException("need a value", nameof(parent));
            if (string.IsNullOrWhiteSpace(child))
                throw new ArgumentException("need a value", nameof(child));
            
            if (!data.ContainsKey(parent))
                throw new BadDataException($"dictionary does not contain key \"{parent}\"");
            #endregion Validation

            var result = false;
            var contents = data[parent];

            foreach (var (_, type) in contents)
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

            if ("no other bags.".Equals(m.Groups[2].Value, StringComparison.Ordinal))
            {
                // That's it
                return (bagType, childTypes);
            }
            
            // 6 and 7 always contain the last numbered value
            childTypes.Add((int.Parse(m.Groups[6].Value), m.Groups[7].Value));
            
            // But sometimes we have more than 1 numbered child.
            var childR = new Regex(miniExpression);
            if (m.Groups[2].Value.Contains(","))
            {
                var otherChildren = m.Groups[3].Value.Split(", ", StringSplitOptions.RemoveEmptyEntries);
                childTypes.AddRange(from child in otherChildren select childR.Match(child) into childM where childM.Success select (int.Parse(childM.Groups[1].Value), childM.Groups[2].Value));
            }

            var result = (bagType, childTypes);
            return result;
        }
        #endregion Private Methods
    }
}