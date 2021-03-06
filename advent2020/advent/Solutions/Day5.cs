﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Exceptions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day5 : Day
    {
        public Day5() : base(5)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            var testCasesA = new Dictionary<string, (short, short, int)>
            {
                {"FBFBBFFRLR", (44, 5, 357)},
                {"BFFFBBFRRR", (70, 7, 567)},
                {"FFFBBBFRRR", (14, 7, 119)},
                {"BBFFBBFRLL", (102, 4, 820)}
            };

            var passesA = true;
            foreach (var _ in from test in testCasesA let result = Locate(test.Key) where result.Item1 != test.Value.Item1 ||
                result.Item2 != test.Value.Item2 ||
                result.Item3 != test.Value.Item3 select test)
            {
                passesA = false;
            }
            
            return passesA;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = SolveA(Data);
            return new List<string> {$"the highest seat ID is [bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = SolveB(Data);
            return new List<string> {$"your seat is [bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int SolveA(IEnumerable<string> data)
        {
            var results = data.Select(Locate).ToList();
            return results.Max(tuple => tuple.Item3);
        }
        
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        private static int SolveB(IEnumerable<string> data)
        {
            var tuples = data.Select(Locate).ToList().OrderBy(t => t.Item3);
            
            var minSeat = tuples.First().Item3;
            var maxSeat = tuples.Last().Item3;

            for (var id = minSeat; id < maxSeat; id++)
            {
                if (tuples.Any(t => t.Item3 == id - 1) &&
                    tuples.Any(t => t.Item3 == id + 1) &&
                    tuples.All(t => t.Item3 != id))
                    return id;
            }

            throw new AnswerNotFoundException();
        }

        private static (short, short, int) Locate(string input)
        {
            var rows = new Span<short>(BuildArray(128));
            var cols = new Span<short>(BuildArray(8));

            foreach (var c in input)
            {
                var rowMid = rows.Length / 2;
                var colMid = cols.Length / 2;
                
                switch (c)
                {
                    case 'F':
                        rows = rows.Slice(0, rowMid);
                        break;
                    case 'B':
                        rows = rows.Slice(rowMid);
                        break;
                    case 'R':
                        cols = cols.Slice(colMid);
                        break;
                    case 'L':
                        cols = cols.Slice(0, colMid);
                        break;
                }
            }

            var row = rows[0];
            var col = cols[0];
            var id = row * 8 + col;

            return (row, col, id);
        }

        private static short[] BuildArray(short upper)
        {
            var l = new List<short>();
            for (short i = 0; i < upper; i++)
                l.Add(i);

            return l.ToArray();
        }
        #endregion Private Methods
    }
}