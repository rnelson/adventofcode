using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace advent.ConsoleCode
{
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    [SuppressMessage("ReSharper", "CA1307")]
    [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
    internal class Program
    {
        private Instruction[] Instructions { get; }
        public IEnumerable<Instruction>? Lines => new List<Instruction>(Instructions).ToArray().Clone() as Instruction[];

        public Program(IEnumerable<Instruction> instructions)
        {
            Instructions = instructions.ToArray().Clone() as Instruction[] ?? Array.Empty<Instruction>();
        }
        
        public void ModifyNthInstruction(InstructionType oldType, InstructionType newType, int n)
        {
            var seen = 0;
            var changed = false;
            
            for (var line = 0; line < Instructions.Length; line++)
            {
                var statement = Instructions[line].Clone() as Instruction;
                
                if (oldType.ToString().Equals(statement!.Type.ToString(), StringComparison.Ordinal))
                {
                    if (!changed && seen == n)
                    {
                        statement.Type = newType;
                        changed = true;
                    }
                    
                    seen++;
                }

                Instructions[line] = statement;
            }
        }
    }
}