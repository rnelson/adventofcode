using JetBrains.Annotations;

namespace advent
{
    internal interface IDay
    {
        [UsedImplicitly]
        public int DayNumber { get; set; }

        [UsedImplicitly]
        public void Header();
        [UsedImplicitly]
        public void Part1();
        [UsedImplicitly]
        public void Part2();
    }
}
