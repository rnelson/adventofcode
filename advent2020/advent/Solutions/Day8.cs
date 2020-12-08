using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.ConsoleCode;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    [SuppressMessage("ReSharper", "CA1307")]
    [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
    internal class Day8 : Day
    {
        public Day8() : base(8)
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
                "nop +0",
                "acc +1",
                "jmp +4",
                "acc +3",
                "jmp -3",
                "acc -99",
                "acc +1",
                "jmp -4",
                "acc +6"
            };
            #endregion Test data

            return Solve(text) == 5;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = Solve(Data);
            return new List<string> {$"acc = [bold yellow]{answer}[/]"};
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
        private static int Solve(IEnumerable<string> data)
        {
            var instructions = GetInstructions(data);
            var console = new Console(instructions) {StopOnReexecute = true};
            
            console.Run();
            return console.Accumulator;
        }

        private static IEnumerable<Instruction> GetInstructions(IEnumerable<string> data)
        {
            var lineNumber = 0;
            return data.Select(line => Instruction.Parse(line, ++lineNumber)).ToList();
        }
        #endregion Private Methods
    }
}