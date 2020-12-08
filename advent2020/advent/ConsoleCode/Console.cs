using System;
using System.Collections.Generic;
using System.Linq;

namespace advent.ConsoleCode
{
    public class Console
    {
        public bool StopOnReexecute { get; set; }
        public bool StoppedOnReexecute { get; set; }
        
        public int Accumulator { get; private set; } = 0;
        public int Pointer { get; private set; } = 0;
        public int LastAccumulator { get; private set; } = 0;
        public int LastPointer { get; private set; } = 0;
        private int Lines { get; set; } = 0;

        private IDictionary<int, (Instruction, bool)> Program { get; set; }

        public Console(IEnumerable<Instruction> program)
        {
            if (program is null)
                throw new ArgumentException("missing program");

            var instructions = program.ToArray();
            if (instructions is null || !instructions.Any())
                throw new ArgumentException("missing program");
            
            Program = LoadProgram(instructions);
            Lines = Program.Keys.Count;
        }

        public Console(Program program)
        {
            if (program?.Lines is null)
                throw new ArgumentException("missing program");

            var instructions = program.Lines.ToList();
            if (instructions is null || !instructions.Any())
                throw new ArgumentException("missing program");
            
            Program = LoadProgram(instructions);
            Lines = Program.Keys.Count;
        }

        public void Run()
        {
            Accumulator = 0;
            Pointer = 0;
            LastAccumulator = 0;
            LastPointer = 0;
            var steps = Program.Values.OrderBy(t => t.Item1.Line).ToArray();

            while (Pointer < steps.Length)
            {
                // Save our old values
                LastPointer = Pointer;
                LastAccumulator = Accumulator;
                
                // Process the next instruction
                var (instruction, visited) = steps[Pointer];

                if (visited && StopOnReexecute)
                {
                    StoppedOnReexecute = true;
                    break;
                }

                steps[Pointer].Item2 = true; // visited
                
                switch (instruction.Type.ToString())
                {
                    case "acc":
                        Accumulator += instruction.Argument;
                        Pointer++;
                        break;
                    case "jmp":
                        Pointer += instruction.Argument;
                        break;
                    case "nop":
                        Pointer++;
                        break;
                }
            }
        }

        private IDictionary<int, (Instruction, bool)> LoadProgram(IEnumerable<Instruction> program)
        {
            var result = new Dictionary<int, (Instruction, bool)>();

            foreach (var line in program)
            {
                result[line.Line] = (line, false);
            }

            return result;
        }

    }
}