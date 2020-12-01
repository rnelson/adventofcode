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
        public bool Test();
        [UsedImplicitly]
        public void PartA();
        [UsedImplicitly]
        public void PartB();
    }
}
