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
            
            var sumA = SolveA(text);
            var sumB = SolveB(text);

            return sumA == 11 && sumB == 6;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = SolveA(Data);
            return new List<string> {$"[bold yellow]{answer}[/]"};
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
        private static int SolveA(IEnumerable<string> batch)
        {
            var blocks = Text.Chunk(batch);
            
            // Combine each block of strings into a single
            var uglyLines = blocks.Select(block => block.Aggregate("", (s1, s2) => $"{s1}{s2}")).ToList();
            
            // Get only distinct answers from each
            var lines = uglyLines.Select(line => new string(line.Distinct().ToArray())).ToList();

            // Combine all of those answers
            var answers = lines.Aggregate(string.Empty, (s1, s2) => $"{s1}{s2}");
            return answers.Length;
        }
        
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int SolveB(IEnumerable<string> batch)
        {
            var blocks = Text.Chunk(batch);

            var distinctByBlock = blocks
                .Select(block => block.Aggregate((a, b) => new string(a.Intersect(b).ToArray()))).ToList();
            var combined = distinctByBlock.Aggregate(string.Empty, (a, b) => $"{a}{b}");
            return combined.Length;
        }
        #endregion Private Methods
    }
}