namespace advent
{
    internal interface IDay
    {
        int DayNumber { get; set; }

        void Header();
        void Part1();
        void Part2();
    }
}
