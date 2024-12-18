namespace advent2024.ChronospatialComputer;

internal class Computer
{
    public int A { get; set; } = 0;
    public int B { get; set; } = 0;
    public int C { get; set; } = 0;

    public int InstructionPtr { get; set; } = 0;

    public Instruction CurrentInstruction { get; private set; } = Instruction.DefaultInstruction;

    private readonly List<Instruction> _program = [];
    private Instruction[] _runningProgram = [];
    private readonly List<string> _output = [];

    public void AddInstruction(int opcode, int operand)
    {
        var code = OpcodeBase.CreateOpcode(opcode);
        var instruction = new Instruction(code, operand);
        
        AddInstruction(instruction);
    }

    public void AddInstruction(Instruction instruction)
    {
        _program.Add(instruction);
    }

    public void AddInstructions(IEnumerable<int> instructions)
    {
        var result = new List<Instruction>();
        var inst = instructions.ToArray();
        
        if (inst.Length == 0)
            throw new InvalidOperationException("no instructions provided");
        
        if (inst.Length % 2 != 0)
            throw new InvalidOperationException("incorrect number of arguments");

        for (var i = 0; i < inst.Length; i += 2)
        {
            var code = OpcodeBase.CreateOpcode(inst[i]);
            var instruction = new Instruction(code, inst[i + 1]);
            
            result.Add(instruction);
        }

        AddInstructions(result);
    }

    public void AddInstructions(IEnumerable<Instruction> instructions)
    {
        _program.AddRange(instructions);
    }

    public void AddOutput(object value)
    {
        _output.Add(value.ToString() ?? throw new InvalidOperationException(nameof(value)));
    }

    public void ClearInstructions()
    {
        _program.Clear();
    }

    public void Execute()
    {
        _runningProgram = _program.ToArray();
        _output.Clear();
        
        InstructionPtr = 0;

        while (InstructionPtr < _program.Count * 2)
        {
            var instruction = _runningProgram[InstructionPtr / 2];
            CurrentInstruction = instruction;
            
            instruction.Opcode.Operate(this);
            InstructionPtr += 2;
        }
    }
    
    public string GetOutput() => string.Join(",", _output);
}