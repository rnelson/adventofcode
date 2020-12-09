﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Exceptions;
using advent.Helpers;
using Combinatorics.Collections;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day9 : Day
    {
        public Day9() : base(9)
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
                "35",
                "20",
                "15",
                "25",
                "47",
                "40",
                "62",
                "55",
                "65",
                "95",
                "102",
                "117",
                "150",
                "182",
                "127",
                "219",
                "299",
                "277",
                "309",
                "576"
            };
            #endregion Test data

            return Solve(text, 5) == 127;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data);
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
        private static long Solve(IEnumerable<string> data, int preambleLength = 25, int combinationSize = 2)
        {
            var array = Text.StringsToLongs(data).ToArray();
            var numbers = new Span<long>(array);

            for (var start = preambleLength; start < numbers.Length; start++)
            {
                var preamble = numbers.Slice(start - preambleLength, preambleLength).ToArray();
                var combinations = new Combinations<long>(preamble, combinationSize);

                var found = combinations.Any(c => c.Sum() == array[start]);

                if (!found)
                    return array[start];
            }
            
            throw new AnswerNotFoundException();
        }
        #endregion Private Methods
    }
}