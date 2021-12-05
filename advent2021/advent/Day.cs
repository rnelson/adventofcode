using System.Reflection;
using Spectre.Console;

namespace advent
{
    [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
    [SuppressMessage("ReSharper", "PublicConstructorInAbstractClass")]
    public abstract class Day
    {
        #region Fields
        private int? day;
        #endregion Fields

        #region Properties
        [UsedImplicitly]
        public int DayNumber
        {
            get => day ?? -1;
            set
            {
                if (value is < 1 or > 31)
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be between 1 and 31");

                day = value;
            }
        }

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        [SuppressMessage("Usage", "CA2227:Collection properties should be read only")]
        protected ICollection<string> Data { get; set; } = new List<string>();

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        protected IEnumerable<int> DataAsInts => Data.Select(int.Parse).ToList();

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        protected IList<int> CommaSeparatedDataAsInts => Data.Select(int.Parse).ToList();
        #endregion Properties

        #region Constructors
        /// <summary>
        /// Initializes a new instance of this <see cref="Day"/> class.
        /// </summary>
        [SuppressMessage("Globalization", "CA1307:Specify StringComparison")]
        public Day()
        {
            var className = GetType().Name;
            if (string.IsNullOrWhiteSpace(className) || className == "Day" || !className.StartsWith("Day"))
                return;
            
            if (int.TryParse(className[3..], out var number))
                DayNumber = number;
        }

        /// <summary>
        /// Initializes a new instance of this <see cref="Day"/> class, using <paramref name="data"/> as its data instead of the default source file.
        /// </summary>
        /// <param name="data">The data to use.</param>
        public Day(IEnumerable<string> data)
        {
            Data = (ICollection<string>)(data ?? throw new ArgumentNullException(nameof(data)));
        }
        #endregion Constructors

        #region Public Methods
        [UsedImplicitly]
        public void Header()
        {
            AnsiConsole.MarkupLine($"[underline deepskyblue3]Advent of Code 2021[/] (Day {DayNumber})\n");
        }
        
        [UsedImplicitly]
        public abstract object PartA();

        [UsedImplicitly]
        public abstract object PartB();

        [SuppressMessage("ReSharper", "HeapView.BoxingAllocation")]
        public void LoadInput()
        {
            if (!day.HasValue)
                throw new InvalidOperationException("must specify day before loading input file");

            var pwd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            if (string.IsNullOrWhiteSpace(pwd))
                throw new InvalidOperationException("unable to determine current directory");

            var filename = Path.Combine(pwd, $"../../../Inputs/{DayNumber.ToString().PadLeft(2, '0')}.txt");
            if (!File.Exists(filename))
                throw new InvalidOperationException($"cannot read {filename}");

            Data = File.ReadAllLines(filename).ToList();
        }

        [SuppressMessage("ReSharper", "UnusedMember.Global")]
        public void LoadCommaSeparatedInput()
        {
            LoadInput();
            var s = Data.First();

            Data = s.Split(new[] { ',' });
        }
        #endregion Public Methods
    }
}
