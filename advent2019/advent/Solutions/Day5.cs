using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day5 : Day
    {
        #region Enums
        private enum Opcode
        {
            Add = 1,
            Multiply = 2,
            ReadLine = 3,
            WriteLine = 4,
            Halt = 99
        }

        private enum Mode
        {
            Position = 0,
            Immediate = 1,
            Unknown = 3000
        }
        #endregion Enums

        public Day5()
        {
            DayNumber = 5;
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            LoadCommaSeparatedInput();
            var code = Data.Select(int.Parse).ToArray();

            Interpret(ref code);

            return new List<string> {""};
        }

        protected override ICollection<string> DoPart2()
        {
            LoadCommaSeparatedInput();
            return new List<string> { "Not yet completed" };
        }
        #endregion IDay Members

        #region Private Methods
        private void Interpret(ref int[] code)
        {
            var ip = 0;

            while (code[ip] != (int)Opcode.Halt)
            {
                var instruction = Instruction.Parse(code, ip, Culture);

                int destination, argument1, argument2;

                switch (instruction.Opcode)
                {
                    case Opcode.Add:
                        argument1 = GetValue(instruction.Argument1Mode, code, ip, 1);
                        argument2 = GetValue(instruction.Argument2Mode, code, ip, 2);
                        destination = GetLocation(instruction.Argument3Mode, code, ip, 3);

                        code[destination] = argument1 + argument2;
                        ip += 4;

                        break;
                    case Opcode.Multiply:
                        argument1 = GetValue(instruction.Argument1Mode, code, ip, 1);
                        argument2 = GetValue(instruction.Argument2Mode, code, ip, 2);
                        destination = GetLocation(instruction.Argument3Mode, code, ip, 3);

                        code[destination] = argument1 * argument2;
                        ip += 4;

                        break;
                    case Opcode.ReadLine:
                        Console.Write(Resources.GetString("Day5.Input", Culture));

                        string s = null;
                        while (string.IsNullOrWhiteSpace(s))
                        {
                            s = Console.ReadLine();
                        }

                        destination = GetLocation(instruction.Argument1Mode, code, ip, 1);
                        code[destination] = int.Parse(s, Culture.NumberFormat);
                        ip += 2;

                        break;
                    case Opcode.WriteLine:
                        var output = code[GetLocation(instruction.Argument1Mode, code, ip, 1)].ToString(Culture);
                        Console.WriteLine(output);
                        ip += 2;

                        break;
                    case Opcode.Halt:
                        break;
                }
            }
        }

        private int GetValue(Mode mode, int[] code, int ip, int offset)
        {
            var value = 0;

            switch (mode)
            {
                case Mode.Position:
                    return code[code[ip + offset]];
                    break;

                case Mode.Immediate:
                    value = code[ip + offset];
                    break;
            }

            return value;
        }

        private int GetLocation(Mode mode, int[] code, int ip, int offset)
        {
            var location = -1;

            switch (mode)
            {
                case Mode.Position:
                    location = code[ip + offset];
                    break;
                case Mode.Immediate:
                    location = offset;
                    break;
            }

            return location;
        }
        #endregion Private Methods

        #region Instruction
        private class Instruction
        {
            #region Properties
            internal Opcode Opcode { get; set; }

            internal int? Argument1 { get; set; }
            internal Mode Argument1Mode { get; set; } = Mode.Unknown;

            internal int? Argument2 { get; set; }
            internal Mode Argument2Mode { get; set; } = Mode.Unknown;

            internal int? Argument3 { get; set; }
            internal Mode Argument3Mode { get; set; } = Mode.Unknown;
            #endregion Properties

            #region Public Methods
            internal static Instruction Parse(int[] code, int ip, IFormatProvider culture = null)
            {
                var instruction = new Instruction();
                var input = code[ip];
                var s = input.ToString("D5", culture);

                switch (s.Substring(s.Length - 2))
                {
                    case "01":
                        instruction.Opcode = Opcode.Add;
                        (instruction.Argument1, instruction.Argument1Mode) = DetermineArgument(code, ip, 1);
                        (instruction.Argument2, instruction.Argument2Mode) = DetermineArgument(code, ip, 2);
                        (instruction.Argument3, instruction.Argument3Mode) = DetermineArgument(code, ip, 3);
                        break;
                    case "02":
                        instruction.Opcode = Opcode.Multiply;
                        (instruction.Argument1, instruction.Argument1Mode) = DetermineArgument(code, ip, 1);
                        (instruction.Argument2, instruction.Argument2Mode) = DetermineArgument(code, ip, 2);
                        (instruction.Argument3, instruction.Argument3Mode) = DetermineArgument(code, ip, 3);
                        break;
                    case "03":
                        instruction.Opcode = Opcode.ReadLine;
                        (instruction.Argument1, instruction.Argument1Mode) = DetermineArgument(code, ip, 1);
                        break;
                    case "04":
                        instruction.Opcode = Opcode.WriteLine;
                        (instruction.Argument1, instruction.Argument1Mode) = DetermineArgument(code, ip, 1);
                        
                        break;
                    case "99":
                        instruction.Opcode = Opcode.Halt;
                        (instruction.Argument1, instruction.Argument1Mode) = DetermineArgument(code, ip, 1);
                        break;
                }

                return instruction;
            }
            #endregion Public Methods

            #region Private Methods
            private static (int, Mode) DetermineArgument(int[] code, int ip, int argNumber)
            {
                var value = 0;
                var mode = Mode.Position;

                // Get the instruction as a full five digit string
                var input = code[ip];
                var s = input.ToString("D5");

                // Determine mode
                var modeIndex = 3 - argNumber;
                if ('1'.Equals(s[modeIndex]))
                    mode = Mode.Immediate;

                // Grab the value/location
                value = code[ip + argNumber];

                return (value, mode);
            }
            #endregion Private Method
        }
        #endregion Instruction
    }
}