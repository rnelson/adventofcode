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

        #region Special Positions
        private enum Position
        {
            Noun = 1,
            Verb = 2
        }
        #endregion Special Positions

        private int[] Intcode;
        private const int Output = 19690720;

        public Day2()
        {
            DayNumber = 2;
            LoadCommaSeparatedInput();
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            Intcode = Data.Select(int.Parse).ToArray();
            Intcode[(int)Position.Noun] = 12;
            Intcode[(int)Position.Verb] = 2;

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

            return new List<string> {Intcode[0].ToString(Culture.NumberFormat)};
        }

        protected override ICollection<string> DoPart2()
        {
            var nouns = Enumerable.Range(0, 100);
            var verbs = Enumerable.Range(0, 100);

            foreach (var noun in nouns)
            {
                foreach (var verb in verbs)
                {
                    Intcode = Data.Select(int.Parse).ToArray();
                    Intcode[(int)Position.Noun] = noun;
                    Intcode[(int)Position.Verb] = verb;

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

                    if (Intcode[0] == Output)
                    {
                        var answer = 100 * Intcode[(int)Position.Noun] + Intcode[(int)Position.Verb];
                        return new List<string> { answer.ToString(Culture.NumberFormat) };
                    }
                }
            }

            return new List<string> {"Unable to find answer"};
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