using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;

namespace advent
{
    internal abstract class Day : IDay
    {
        #region Fields
        private readonly CultureInfo culture = CultureInfo.GetCultureInfo("en-US");
        private readonly ResourceManager resources = Strings.ResourceManager;
        private int? day;
        #endregion Fields

        #region Properties
        public int DayNumber
        {
            get => day ?? -1;
            set
            {
                if (value < 1 || value > 31)
                    throw new ArgumentOutOfRangeException(nameof(value), resources.GetString("Help.InvalidDay", culture));

                day = value;
                LoadInput();
            }
        }

        protected ICollection<string> Data { get; set; } = new List<string>();

        protected IList<int> DataAsInts => Data.Select(int.Parse).ToList();
        #endregion Properties

        #region Public Methods
        public void Header()
        {
            var text = resources
                .GetString("Day.Header", culture)
                .Replace("\\n", "\n", StringComparison.InvariantCulture);
            Console.WriteLine(text);
        }

        public void Part1()
        {
            Console.WriteLine(string.Format(culture, resources.GetString("Day.Part", culture), "1"));
            
            var output = DoPart1();
            foreach (var line in output)
            {
                Console.WriteLine(string.Format(culture, resources.GetString("Day.Part.Line", culture), line));
            }
        }

        public void Part2()
        {
            Console.WriteLine();
            Console.WriteLine(string.Format(culture, resources.GetString("Day.Part", culture), "2"));

            var output = DoPart2();
            foreach (var line in output)
            {
                Console.WriteLine(string.Format(culture, resources.GetString("Day.Part.Line", culture), line));
            }
        }
        #endregion Public Methods

        #region Protected Methods
        protected void LoadInput()
        {
            if (!day.HasValue)
                throw new InvalidOperationException(resources.GetString("Help.SpecifyDay", culture));

			var pwd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var filename = Path.Combine(pwd, $"../../../Inputs/input{DayNumber}.txt");
            if (!File.Exists(filename))
                throw new InvalidOperationException($"cannot read {filename}");

            Data = File.ReadAllLines(filename).ToList();
        }

        protected abstract ICollection<string> DoPart1();
        protected abstract ICollection<string> DoPart2();
        #endregion Protected Methods
    }
}
