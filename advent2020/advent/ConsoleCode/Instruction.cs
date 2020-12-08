using System;
using System.Diagnostics.CodeAnalysis;
using advent.Exceptions;

namespace advent.ConsoleCode
{
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    [SuppressMessage("ReSharper", "CA1307")]
    [SuppressMessage("ReSharper", "HeapView.ClosureAllocation")]
    internal class Instruction : ICloneable
    {
        public int Line { get; private set; }
        public InstructionType Type { get; set; } = InstructionType.Unknown;
        public int Argument { get; private set; }

        private Instruction() { }

        public static Instruction Parse(string input, int line)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("bad input", nameof(input));

            var instruction = new Instruction();
            var bits = input.Split(" ");

            instruction.Type = bits[0] switch
            {
                "acc" => InstructionType.Accumulator,
                "jmp" => InstructionType.Jump,
                "nop" => InstructionType.NoOp,
                _ => throw new BadDataException($"unexpected instruction: \"{bits[0]}\"")
            };

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

    [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local")]
    public class InstructionType
    {
        public static readonly InstructionType Unknown = new InstructionType(string.Empty, "<UNKNOWN>");
        public static readonly InstructionType Accumulator = new InstructionType("acc", "Accumulator");
        public static readonly InstructionType Jump = new InstructionType("jmp", "Jump");
        public static readonly InstructionType NoOp = new InstructionType("nop", "No Operation");
        
        private string Operation { get; }
        private string Description { get; }
        
        private InstructionType(string operation, string description)
        {
            Operation = operation;
            Description = description;
        }

        public override string ToString() => Operation;
    }
}