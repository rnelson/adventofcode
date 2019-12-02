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

        private const int Output = 19690720;

        public Day2()
        {
            DayNumber = 2;
            LoadCommaSeparatedInput();
        }

        #region IDay Members
        protected override ICollection<string> DoPart1()
        {
            var code = Data.Select(int.Parse).ToArray();
            code[1] = 12;
            code[2] = 2;

            Interpret(ref code);

            return new List<string> {code[0].ToString(Culture.NumberFormat)};
        }

        protected override ICollection<string> DoPart2()
        {
            var nouns = Enumerable.Range(0, 100).ToArray();
            var verbs = Enumerable.Range(0, 100).ToArray();

            foreach (var noun in nouns)
            {
                foreach (var verb in verbs)
                {
                    var code = Data.Select(int.Parse).ToArray();
                    code[1] = noun;
                    code[2] = verb;

                    Interpret(ref code);
                    if (code[0] != Output)
                        continue;

                    var answer = 100 * code[1] + code[2];
                    return new List<string> { answer.ToString(Culture.NumberFormat) };
                }
            }

            return new List<string> {"Unable to find answer"};
        }
        #endregion IDay Members

        #region Private Methods
        private static void Interpret(ref int[] code)
        {
            var ip = 0;

            while (code[ip] != (int)Opcode.Halt)
            {
                switch (code[ip])
                {
                    case (int)Opcode.Add:
                        code[code[ip + 3]] = code[code[ip + 1]] + code[code[ip + 2]];
                        ip += 4;

                        break;
                    case (int)Opcode.Multiply:
                        code[code[ip + 3]] = code[code[ip + 1]] * code[code[ip + 2]];
                        ip += 4;

                        break;
                    case (int)Opcode.Halt:
                        break;
                }
            }
        }
        #endregion Private Methods
    }
}