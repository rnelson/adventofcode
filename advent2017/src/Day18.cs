using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2017
{
    class Day18
    {
        static void Main(string[] args)
        {
            var inputFile = @"../../../aoc-inputs/2017/day18.txt";
            var part1 = 0;
            var part2 = 0;

            var commands = File.ReadAllLines(inputFile).ToList();
            var card = new SoundCard(commands);

            part1 = card.ExecutePart1();

            Console.WriteLine($"Part 1: {part1}\nPart 2: {part2}");
        }
    }

    class SoundCard
    {
        private Dictionary<string, int> Registers;
        private List<string> Commands;
        private string LastRegisterPlayed;
        private int LastValuePlayed;
        private int Instruction;

        private SoundCard() { }

        public SoundCard(List<string> commands)
        {
            Instruction = 0;

            Registers = new Dictionary<string, int>();
            Commands = commands;

            foreach (var register in Enumerable.Range('a', 'z' - 'a' + 1).Select(c => (string)Char.ConvertFromUtf32(c)).ToList())
            {
                Registers.Add(register, 0);
            }
        }

        public int ExecutePart1()
        {
            object result = null;

            while (null == result)
            {
                Console.WriteLine("Running: " + Commands.ElementAt(Instruction));
                result = RunCommand(Commands.ElementAt(Instruction));
            }

            return (int)result;
        }

        private object RunCommand(string command)
        {
            object result = null;
            var bits = command.Split(' ');
            var increment = true;

            switch (bits[0])
            {
                case "snd":
                    LastRegisterPlayed = bits[1];
                    LastValuePlayed = Registers[bits[1]];
                    break;
                case "set":
                    Registers[bits[1]] = GetValue(bits[2]);
                    break;
                case "add":
                    Registers[bits[1]] += GetValue(bits[2]);
                    break;
                case "mul":
                    Registers[bits[1]] *= GetValue(bits[2]);
                    break;
                case "mod":
                    Registers[bits[1]] %= GetValue(bits[2]);
                    break;
                case "rcv":
                    if (Registers[bits[1]] != 0)
                    {
                        result = LastValuePlayed;
                    }
                    break;
                case "jgz":
                    if (Registers[bits[1]] > 0)
                    {
                        Instruction += GetValue(bits[2]);
                        increment = false;
                    }
                    break;
                default:
                    break;
            }

            if (increment)
            {
                Instruction++;
            }

            return result;
        }

        private int GetValue(string key)
        {
            int value = 0;
            bool numeric = Int32.TryParse(key, out value);

            if (numeric)
            {
                return value;
            }
            else
            {
                return Registers[key];
            }
        }
    }
}
