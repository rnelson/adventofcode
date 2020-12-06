using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.Helpers;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal class Day6 : Day
    {
        public Day6() : base(6)
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
                "abc",
                "",
                "a",
                "b",
                "c",
                "",
                "ab",
                "ac",
                "",
                "a",
                "a",
                "a",
                "a",
                "",
                "b"
            };
            #endregion Test data
            
            var sumA = Solve(text);

            return sumA == 11;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = 0; //Solve(Data).Count(p => p.SuperValid);
            return new List<string> {$"[bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int Solve(IEnumerable<string> batch)
        {
            var blocks = Text.Chunk(batch);
            
            // Combine each block of strings into a single
            var uglyLines = blocks.Select(block => block.Aggregate("", (s1, s2) => $"{s1}{s2}")).ToList();
            
            // Get only distinct answers from each
            var lines = uglyLines.Select(line => new string(line.Distinct().ToArray())).ToList();

            // Combine all of those answers
            var answers = lines.Aggregate(string.Empty, (s1, s2) => $"{s1}{s2}"); // lines.Select(line => line).ToList();
            return answers.Length;
        }
        #endregion Private Methods
    }
}