using System;
using advent.Exceptions;

namespace advent.ConsoleCode
{
    public class Instruction : ICloneable
    {
        public int Line { get; set; }
        public InstructionType Type { get; set; }
        public int Argument { get; set; }

        private Instruction() { }

        public static Instruction Parse(string input, int line)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("bad input", nameof(input));

            var instruction = new Instruction();
            var bits = input.Split(" ");

            switch (bits[0])
            {
                case "acc":
                    instruction.Type = InstructionType.Accumulator;
                    break;
                case "jmp":
                    instruction.Type = InstructionType.Jump;
                    break;
                case "nop":
                    instruction.Type = InstructionType.NoOp;
                    break;
                default:
                    throw new BadDataException($"unexpected instruction: \"{bits[0]}\"");
            }

            instruction.Argument = int.Parse(bits[1]);
            instruction.Line = line;

            return instruction;
        }

        public override string ToString()
        {
            return $"{Type} {Argument}  ; line {Line}";
        }

        public object Clone()
        {
            return new Instruction {Type = Type, Argument = Argument, Line = Line};
        }
    }

    public class InstructionType
    {
        public static readonly InstructionType Accumulator = new InstructionType("acc", "Accumulator");
        public static readonly InstructionType Jump = new InstructionType("jmp", "Jump");
        public static readonly InstructionType NoOp = new InstructionType("nop", "No Operation");
        
        private string Operation { get; set; }
        private string Description { get; set; }
        
        private InstructionType(string operation, string description)
        {
            Operation = operation;
            Description = description;
        }

        public override string ToString() => Operation;
    }
}