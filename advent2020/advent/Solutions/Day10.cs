using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Helpers;
using JetBrains.Annotations;
using Math = System.Math;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day10 : Day
    {
        public Day10() : base(10)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        [SuppressMessage("ReSharper", "UseDeconstruction")]
        public override bool Test()
        {
            #region Test data
            var textA = new List<string>
            {
                "16",
                "10",
                "15",
                "5",
                "1",
                "11",
                "7",
                "19",
                "6",
                "12",
                "4"
            };
            var textB = new List<string>
            {
                "28",
                "33",
                "18",
                "42",
                "31",
                "14",
                "46",
                "20",
                "48",
                "47",
                "24",
                "23",
                "49",
                "45",
                "19",
                "38",
                "39",
                "11",
                "1",
                "32",
                "25",
                "35",
                "8",
                "17",
                "7",
                "9",
                "4",
                "2",
                "34",
                "10",
                "3"
            };
            #endregion Test data

            var answersA1 = Solve(textA);
            var answersA2 = Solve(textB);
            return answersA1.Item1 == 7 &&
                   answersA1.Item2 == 5 &&
                   answersA1.Item3 == 8 &&
                   answersA2.Item1 == 22 &&
                   answersA2.Item2 == 10 &&
                   answersA2.Item3 == 19208;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var (jolt1, jolt3, _) = Solve(Data);
            var answer = jolt1 * jolt3;
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = Solve(Data);
            return new List<string> {$"[bold yellow]{answer.Item3}[/] arrangements"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static (long, long, long) Solve(IEnumerable<string> data)
        {
            // Add the wall outlet
            var newData = data.ToList();
            newData.Add("0");

            var largest = newData.Max(int.Parse);
            newData.Add((largest + 3).ToString());

            var integers = Text.StringsToLongs(newData).ToArray();
            var sorted = integers.OrderBy(n => n).ToArray();
            var pairs = sorted.Skip(1).Zip(sorted, (y, x) => new[] {x, y}).ToArray();

            var jolt1 = pairs.Count(jolts => Math.Abs(jolts[1] - jolts[0]) == 1);
            var jolt3 = pairs.Count(jolts => Math.Abs(jolts[1] - jolts[0]) == 3);

            return (jolt1, jolt3, FindValidChainCount(sorted));
        }
        
        private static long FindValidChainCount(long[] sortedAdapters)
        {
            var adapters = new Span<long>(sortedAdapters);

            var ways = new long[adapters.Length];
            ways[0] = 1;
            
            foreach (var i in Enumerable.Range(1, ways.Length - 1))
            {
                ways[i] = 0;
                
                for (var j = i - 1; j >= 0; j--)
                    if (adapters[i] - adapters[j] <= 3)
                        ways[i] += ways[j];
                    else
                        break;
            }

            return ways.Last();
        }
        #endregion Private Methods
    }
}