using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Exceptions;
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
        public override bool Test()
        {
            #region Test data
            var text = new List<string>
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

            var answersA = Solve(text);
            return answersA.Item1 == 22 && answersA.Item2 == 10;
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
        private static (long, long, IEnumerable<long>) Solve(IEnumerable<string> data)
        {
            // Add the wall outlet
            var newData = data.ToList();
            newData.Add("0");

            var largest = newData.Max(s => int.Parse(s));
            newData.Add((largest + 3).ToString());
            
            var integers = Text.StringsToLongs(newData).ToArray();
            var sorted = integers.OrderBy(n => n).ToArray();
            var pairs = sorted.Skip(1).Zip(sorted, (y, x) => new[] {x, y}).ToArray();

            //*
            var jolt1 = pairs.Count(jolts => Math.Abs(jolts[1] - jolts[0]) == 1);
            var jolt3 = pairs.Count(jolts => Math.Abs(jolts[1] - jolts[0]) == 3);

            return (jolt1, jolt3, new List<long>());
            //*/

            /*
            var chain = new List<long>();
            var lastAdapter = 0L;
            var jolt1 = 0L;
            var jolt3 = 0L;
            
            foreach (var adapter in sorted)
            {
                var difference = Math.Abs(adapter - lastAdapter);
                lastAdapter = adapter;
                
                if (difference == 1)
                {
                    jolt1++;
                    chain.Add(adapter);
                }
                else if (difference == 3)
                {
                    jolt3++;
                    chain.Add(adapter);
                }
            }

            return (jolt1, jolt3, chain);
            */

            //throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}