using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using advent.ConsoleCode;
using advent.Exceptions;
using JetBrains.Annotations;
using Console = advent.ConsoleCode.Console;

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

            return SolveA(text) == 5 && SolveB(text) == 8;
        }
        
        protected override IEnumerable<string> DoPartA()
        {
            var answer = SolveA(Data);
            return new List<string> {$"acc = [bold yellow]{answer}[/]"};
        }

        protected override IEnumerable<string> DoPartB()
        {
            var answer = SolveB(Data);
            return new List<string> {$"acc = [bold yellow]{answer}[/]"};
        }
        #endregion IDay Members

        #region Private Methods
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int SolveA(IEnumerable<string> data)
        {
            var instructions = GetInstructions(data);
            var console = new Console(instructions) {StopOnReexecute = true};
            
            console.Run();
            return console.Accumulator;
        }
        
        [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
        [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
        private static int SolveB(IEnumerable<string> data)
        {
            var programs = new List<IEnumerable<Instruction>>();
            var instructions = GetInstructions(data).ToArray();
            
            #region Make copies of the program
            var nops = instructions.Where(i => i.Type.ToString() == "nop").ToArray();
            var nopCount = nops.Length;
            var jumps = instructions.Where(i => i.Type.ToString() == "jmp").ToArray();
            var jumpCount = jumps.Length;

            for (var n = 0; n < nopCount; n++)
            {
                var program = new ConsoleCode.Program(instructions);
                program.ModifyNthInstruction(InstructionType.NoOp, InstructionType.Jump, n);
                programs.Add(program.Lines!);
            }

            for (var n = 0; n < jumpCount; n++)
            {
                var program = new ConsoleCode.Program(instructions);
                program.ModifyNthInstruction(InstructionType.Jump, InstructionType.NoOp, n);
                programs.Add(program.Lines!);
            }
            #endregion Make copies of the program
            
            #region Run each copy
            foreach (var program in programs)
            {
                var console = new Console(program) {StopOnReexecute = true};
                console.Run();
                
                if (!console.StoppedOnReexecute)
                    return console.Accumulator;
            }
            #endregion Run each copy
            
            throw new AnswerNotFoundException();
        }

        private static IEnumerable<Instruction> GetInstructions(IEnumerable<string> data)
        {
            var lineNumber = 0;
            return data.Select(line => Instruction.Parse(line, ++lineNumber)).ToList();
        }

        /*
        private static IEnumerable<Instruction> ModifyNthInstruction(IEnumerable<Instruction> instructions,
            InstructionType oldType, InstructionType newType, int n)
        {
            var clone = new List<Instruction>();
            var program = instructions.ToArray();
            
            // Find the nth statement to change
            var seen = 0;
            var changed = false;
            
            for (var line = 0; line < program.Length; line++)
            {
                var original = program[line];
                var statement = original.Clone() as Instruction;
                
                if (!changed && oldType.ToString().Equals(statement!.Type.ToString(), StringComparison.Ordinal))
                {
                    if (seen == n)
                    {
                        statement.Type = newType;
                        changed = true;
                    }
                    
                    seen++;
                }
                
                clone.Add(statement!);
            }

            return clone;
        }
        */
        #endregion Private Methods
    }
}