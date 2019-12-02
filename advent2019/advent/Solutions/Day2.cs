using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;

namespace advent.Solutions
{
    [UsedImplicitly]
    internal class Day2 : Day
    {
        #region Opcodes
        private enum Opcode
        {
            Add = 1,
            Multiply = 2,
            Halt = 99
        }
        #endregion Opcodes

        private int[] Intcode;

        public Day2()
        {
            DayNumber = 2;
            LoadCommaSeparatedInput();

            Intcode = Data.Select(int.Parse).ToArray();
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            Intcode[1] = 12;
            Intcode[2] = 2;
            var ip = 0;

            while (Intcode[ip] != (int)Opcode.Halt)
            {
                switch (Intcode[ip])
                {
                    case (int)Opcode.Add:
                        Add(ref ip);
                        break;
                    case (int)Opcode.Multiply:
                        Multiply(ref ip);
                        break;
                    case (int)Opcode.Halt:
                        break;
                }
            }

            return new List<string> {Intcode[0].ToString()};
        }

        protected override ICollection<string> DoPart2()
        {
            return new List<string>();
        }
        #endregion IDay Members

        #region Private Methods
        private void Add(ref int ip)
        {
            Intcode[Intcode[ip + 3]] = Intcode[Intcode[ip + 1]] + Intcode[Intcode[ip + 2]];
            ip += 4;
        }

        private void Multiply(ref int ip)
        {
            Intcode[Intcode[ip + 3]] = Intcode[Intcode[ip + 1]] * Intcode[Intcode[ip + 2]];
            ip += 4;
        }
        #endregion Private Methods
    }
}