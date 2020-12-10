using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Exceptions;
using advent.Helpers;
using Combinatorics.Collections;
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
            var values = Solve(Data);
            var answer = values.Item1 * values.Item2;
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = 0;
            return new List<string> {$"[bold yellow]{answer}[/]"};
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

            var largest = newData.Max(s => int.Parse(s));
            newData.Add((largest + 3).ToString());

            var integers = Text.StringsToLongs(newData).ToArray();
            var sorted = integers.OrderBy(n => n).ToArray();
            var pairs = sorted.Skip(1).Zip(sorted, (y, x) => new[] {x, y}).ToArray();

            var jolt1 = pairs.Count(jolts => Math.Abs(jolts[1] - jolts[0]) == 1);
            var jolt3 = pairs.Count(jolts => Math.Abs(jolts[1] - jolts[0]) == 3);

            return (jolt1, jolt3, FindValidChainCount(sorted, largest + 3));
        }
        
        private static long FindValidChainCount(long[] sortedAdapters, long deviceJolts)
        {
            var adapters = new Span<long>(sortedAdapters);
            var data = new List<(long, List<long>)>();

            for (var i = 0; i < adapters.Length - 1; i++)
            {
                var jolts = adapters[i];
                var remaining = Math.Min(3, adapters.Length - 1 - i);
                var next = adapters.Slice(i + 1, remaining);

                var options = new List<long>();
                foreach (var possibility in next)
                {
                    if (possibility - jolts <= 3)
                        options.Add(possibility);
                }

                data.Add((jolts, options));
            }

            var cnt = 1;
            for (var i = data.Count - 1; i > 0; i--)
            {
                if (data.ElementAt(i).Item2.Count > 1)
                    cnt += data.ElementAt(i).Item2.Count;
            }

            return cnt;

            /*
            var combinations = data.Last().Item2.Count;
            for (var i = data.Count - 1; i > 0; i--)
            {
                combinations *= data.ElementAt(i).Item2.Count;
            }

            return combinations;
            */
        }
        #endregion Private Methods
    }
}