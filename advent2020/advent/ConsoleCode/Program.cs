using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.ConsoleCode
{
    public class Program
    {
        private Instruction[] Instructions { get; set; }
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
                
                if (oldType.ToString().Equals(statement.Type.ToString(), StringComparison.Ordinal))
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