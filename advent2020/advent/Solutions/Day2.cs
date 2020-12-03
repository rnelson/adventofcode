using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    internal class Day2 : Day
    {
        public Day2() : base(2)
        {
            LoadInput();
        }

        #region IDay Members
        [UsedImplicitly]
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public override bool Test()
        {
            var testCases = new List<string>
            {
                "1-3 a: abcde",
                "1-3 b: cdefg",
                "2-9 c: ccccccccc"
            };
            
            var validA = Solve(testCases);
            var validB = Solve(testCases, true);
            
            return validA == 2 && validB == 1;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data);
            return new List<string> {$"[bold yellow]{answer}[/] valid passwords"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = Solve(Data, true);
            return new List<string> {$"[bold yellow]{answer}[/] valid passwords"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int Solve(IEnumerable<string> inputs, bool positional = false)
        {
            const string expression = @"(\d+)-(\d+) (\w): (\w+)";
            var r = new Regex(expression, RegexOptions.Compiled);
            
            return inputs.Count(input => Solve(r, input, positional));
        }

        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static bool Solve(Regex r, string input, bool positional = false)
        {
            var m = r.Match(input);
            if (!m.Success)
                return false;
            
            var min = int.Parse(m.Groups[1].Value);
            var max = int.Parse(m.Groups[2].Value);
            var letter = m.Groups[3].Value[0];
            var password = m.Groups[4].Value;

            var count = password.Count(c => c == letter);
            var goodCount = count >= min && count <= max;

            if (!positional)
                return goodCount;

            var minMatch = password[min - 1] == letter;
            var maxMatch = password[max - 1] == letter;

            return minMatch ^ maxMatch;
        }
        #endregion Private Methods
    }
}