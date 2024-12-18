using System.Reflection;

namespace advent2024.ChronospatialComputer;

internal interface IOpcode
{
    public void Operate(Computer computer);
}

internal abstract class OpcodeBase : IOpcode
{
    public string Mnemonic { get; init; } = string.Empty;
    public int Opcode { get; init; } = -1;
    public string Description { get; init; } = string.Empty;

    public abstract void Operate(Computer computer);

    public static OpcodeBase CreateOpcode(int opcode)
    {
        var opcodeTypes = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.BaseType == typeof(OpcodeBase));

        foreach (var opcodeType in opcodeTypes)
        {
            var inst = Activator.CreateInstance(opcodeType) as OpcodeBase ??
                       throw new InvalidOperationException($"cannot create instance of {opcodeType}");
            
            if (inst.Opcode == opcode)
                return inst;
        }
        
        throw new InvalidOperationException($"unable to find opcode {opcode}");
    }

    protected static int GetComboOperand(Computer computer, int value) => value switch
        {
            0 or 1 or 2 or 3 => value,
            4 => computer.A,
            5 => computer.B,
            6 => computer.C,
            7 => throw new InvalidOperationException(
                "Combo operand 7 is reserved and will not appear in valid programs."),
            _ => throw new ArgumentOutOfRangeException(nameof(value))
        };
}

internal class InvalidInstruction : OpcodeBase
{
    public InvalidInstruction()
    {
        Mnemonic = string.Empty;
        Opcode = -2;
        Description = "An invalid instruction.";
    }
    
    public override void Operate(Computer computer) => throw new InvalidOperationException();
}

internal class AdvInstruction : OpcodeBase
{
    public AdvInstruction()
    {
        Mnemonic = "adv";
        Opcode = 0;
        Description = "The adv instruction (opcode 0) performs division. The numerator is the value in the A register. The denominator is found by raising 2 to the power of the instruction's combo operand. (So, an operand of 2 would divide A by 4 (2^2); an operand of 5 would divide A by 2^B.) The result of the division operation is truncated to an integer and then written to the A register.";
    }

    public override void Operate(Computer computer)
    {
        var numerator = computer.A;
        var denominator = Math.Pow(2, GetComboOperand(computer, computer.CurrentInstruction.Operand));
        
        var result = numerator / denominator;
        computer.A = (int)result;
    }
}

internal class BxlInstruction : OpcodeBase
{
    public BxlInstruction()
    {
        Mnemonic = "bxl";
        Opcode = 1;
        Description = "The bxl instruction (opcode 1) calculates the bitwise XOR of register B and the instruction's literal operand, then stores the result in register B.";
    }

    public override void Operate(Computer computer)
    {
        computer.B ^= computer.CurrentInstruction.Operand;
    }
}

internal class BstInstruction : OpcodeBase
{
    public BstInstruction()
    {
        Mnemonic = "bst";
        Opcode = 2;
        Description = "The bst instruction (opcode 2) calculates the value of its combo operand modulo 8 (thereby keeping only its lowest 3 bits), then writes that value to the B register.";
    }

    public override void Operate(Computer computer)
    {
        computer.B = computer.CurrentInstruction.Operand % 8;
    }
}

internal class JnzInstruction : OpcodeBase
{
    public JnzInstruction()
    {
        Mnemonic = "jnz";
        Opcode = 3;
        Description = "The jnz instruction (opcode 3) does nothing if the A register is 0. However, if the A register is not zero, it jumps by setting the instruction pointer to the value of its literal operand; if this instruction jumps, the instruction pointer is not increased by 2 after this instruction.";
    }

    public override void Operate(Computer computer)
    {
        if (computer.A == 0) return;

        // Move 2 earlier so we don't have to do anything special here to not advance.
        computer.InstructionPtr = computer.CurrentInstruction.Operand - 2;
    }
}

internal class BxcInstruction : OpcodeBase
{
    public BxcInstruction()
    {
        Mnemonic = "bxc";
        Opcode = 4;
        Description = "The bxc instruction (opcode 4) calculates the bitwise XOR of register B and register C, then stores the result in register B. (For legacy reasons, this instruction reads an operand but ignores it.)";
    }

    public override void Operate(Computer computer)
    {
        computer.B ^= computer.C;
    }
}

internal class OutInstruction : OpcodeBase
{
    public OutInstruction()
    {
        Mnemonic = "out";
        Opcode = 5;
        Description = "The out instruction (opcode 5) calculates the value of its combo operand modulo 8, then outputs that value. (If a program outputs multiple values, they are separated by commas.)";
    }

    public override void Operate(Computer computer)
    {
        computer.AddOutput(computer.CurrentInstruction.Operand % 8);
    }
}

internal class BdvInstruction : OpcodeBase
{
    public BdvInstruction()
    {
        Mnemonic = "bdv";
        Opcode = 6;
        Description = "The bdv instruction (opcode 6) works exactly like the adv instruction except that the result is stored in the B register. (The numerator is still read from the A register.)";
    }

    public override void Operate(Computer computer)
    {
        var numerator = computer.A;
        var denominator = Math.Pow(2, GetComboOperand(computer, computer.CurrentInstruction.Operand));
        
        var result = numerator / denominator;
        computer.B = (int)result;
    }
}

internal class CdvInstruction : OpcodeBase
{
    public CdvInstruction()
    {
        Mnemonic = "cdv";
        Opcode = 7;
        Description = "The cdv instruction (opcode 7) works exactly like the adv instruction except that the result is stored in the C register. (The numerator is still read from the A register.)";
    }

    public override void Operate(Computer computer)
    {
        var numerator = computer.A;
        var denominator = Math.Pow(2, GetComboOperand(computer, computer.CurrentInstruction.Operand));
        
        var result = numerator / denominator;
        computer.C = (int)result;
    }
}