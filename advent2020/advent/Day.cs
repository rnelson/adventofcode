using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;

namespace advent
{
    [SuppressMessage("ReSharper", "HeapView.ObjectAllocation.Possible")]
    internal abstract class Day : IDay
    {
        #region Fields
        private int? day;
        #endregion Fields

        #region Properties
        public int DayNumber
        {
            get => day ?? -1;
            set
            {
                if (value < 1 || value > 31)
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be between 1 and 31");

                day = value;
            }
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        protected ICollection<string> Data { get; set; } = new List<string>();

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        protected IEnumerable<int> DataAsInts => Data.Select(int.Parse).ToList();
        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        protected IList<int> CommaSeparatedDataAsInts => Data.Select(int.Parse).ToList();
        #endregion Properties
        
        #region Constructors
        [UsedImplicitly]
        private Day() { }

        protected Day(int dayNumber)
        {
            DayNumber = dayNumber;
        }
        #endregion Constructors

        #region Public Methods
        public void Header()
        {
            Console.WriteLine("Advent of Code 2020 - C#\n======================================\n");
        }

        public bool Test() => true;

        public void Part1()
        {
            Console.WriteLine("Part 1:");
            
            var output = DoPart1();
            foreach (var line in output)
            {
                Console.WriteLine($"  {line}");
            }
        }

        public void Part2()
        {
            Console.WriteLine("Part 2:");

            var output = DoPart2();
            foreach (var line in output)
            {
                Console.WriteLine($"  {line}");
            }
        }
        #endregion Public Methods

        #region Protected Methods
        [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
        protected void LoadInput()
        {
            if (!day.HasValue)
                throw new InvalidOperationException("must specify day before loading input file");

			var pwd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(pwd))
                throw new InvalidOperationException("unable to determine current directory");
            
            var filename = Path.Combine(pwd, $"../../../Inputs/input{DayNumber}.txt");
            if (!File.Exists(filename))
                throw new InvalidOperationException($"cannot read {filename}");

            Data = File.ReadAllLines(filename).ToList();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        protected void LoadCommaSeparatedInput()
        {
            LoadInput();
            var s = Data.First();

            Data = s.Split(new[] { ',' });
        }

        protected abstract IEnumerable<string> DoPart1();
        protected abstract IEnumerable<string> DoPart2();
        #endregion Protected Methods
    }
}
