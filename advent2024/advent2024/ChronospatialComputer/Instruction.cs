namespace advent2024.ChronospatialComputer;

internal class Instruction(OpcodeBase opcode, int operand)
{
    public OpcodeBase Opcode { get; init; } = opcode;
    public int Operand { get; init; } = operand;

    public static Instruction DefaultInstruction { get; } = new(new InvalidInstruction(), -1);
}