using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2025;

/// <summary>
/// 2024 day 13.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public partial class Day13(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 13, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var machines = GetMachines();
        var answer = 0UL;

        foreach (var machine in machines)
        {
            var counts = machine.Solve();
            
            if (counts.Item1 != ulong.MaxValue && counts.Item2 != ulong.MaxValue)
                answer += counts.Item1 * machine.ButtonACost + counts.Item2 * machine.ButtonBCost;
        }
        
        return answer;
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        const long offset = 10000000000000L;
        
        var machines = GetMachines();
        var answer = 0UL;

        foreach (var machine in machines)
        {
            var newMachine = new ClawMachine((machine.Prize.X + offset, machine.Prize.Y + offset),
                (machine.A.X, machine.A.Y),
                (machine.B.X, machine.B.Y));
            var counts = newMachine.Solve();
            
            if (counts.Item1 != ulong.MaxValue && counts.Item2 != ulong.MaxValue)
                answer += counts.Item1 * machine.ButtonACost + counts.Item2 * machine.ButtonBCost;
        }
        
        return answer;
    }

    private ClawMachine[] GetMachines()
    {
        var machines = new List<ClawMachine>();
        var strings = Input.ToArray();
        
        for (var i = 0; i < strings.Length; i += 4)
            machines.Add(ClawMachine.Create(strings[i], strings[i + 1], strings[i + 2]));
        
        return machines.ToArray();
    }
    
    private record Button(long X, long Y);

    private record Location(long X, long Y);

    private partial class ClawMachine((long, long) prizeLocation,
        (long, long) aMovement,
        (long, long) bMovement,
        ulong buttonACost = 3,
        ulong buttonBCost = 1)
    {
        public Button A { get; } = new(aMovement.Item1, aMovement.Item2);
        public ulong ButtonACost { get; } = buttonACost;
        
        public Button B { get; } = new(bMovement.Item1, bMovement.Item2);
        public ulong ButtonBCost { get; } = buttonBCost;
        
        public Location Prize { get; } = new(prizeLocation.Item1, prizeLocation.Item2);

        public (ulong, ulong) Solve()
        {
            // px = aCount * ax + bCount * bx
            // py = aCount * ay + bCount * by
            // 
            // Some math goes here! I'm far too removed from my math classes and
            // used the internet to sort these out
            // 
            // aCount = (bx * py - by * px) / (bx * ay - by * ax)
            // bCount = (ax * py - ay * px) / (ax * by - ay * bx)

            var aCountN = B.X * Prize.Y - B.Y * Prize.X;
            var aCountD = B.X * A.Y - B.Y * A.X;
            var bCountN = A.X * Prize.Y - A.Y * Prize.X;
            var bCountD = A.X * B.Y - A.Y * B.X;

            var a = (ulong)Math.DivRem(aCountN, aCountD, out var aCountRemainder);
            var b = (ulong)Math.DivRem(bCountN, bCountD, out var bCountRemainder);
            
            if (aCountRemainder != 0) a = ulong.MaxValue;
            if (bCountRemainder != 0) b = ulong.MaxValue;

            return (a, b);
        }
        
        public static ClawMachine Create(string buttonA, string buttonB, string prize)
        {
            var aButtonRegex = ButtonRegex();
            var bButtonRegex = ButtonRegex();
            var prizeRegex = PrizeRegex();

            if (!aButtonRegex.IsMatch(buttonA) ||
                !bButtonRegex.IsMatch(buttonB) ||
                !prizeRegex.IsMatch(prize))
                throw new("unable to parse arguments");
            
            var aMatches = aButtonRegex.Matches(buttonA);
            var bMatches = bButtonRegex.Matches(buttonB);
            var prizeMatches = prizeRegex.Matches(prize);

            var aMovement = (
                long.Parse(aMatches[0].Groups[1].Value),
                long.Parse(aMatches[0].Groups[2].Value));
            var bMovement = (
                long.Parse(bMatches[0].Groups[1].Value),
                long.Parse(bMatches[0].Groups[2].Value));
            var prizeLocation = (
                long.Parse(prizeMatches[0].Groups[1].Value),
                long.Parse(prizeMatches[0].Groups[2].Value));
            
            return new(prizeLocation, aMovement, bMovement);
        }

        [GeneratedRegex(@"Button [AB]: X([+-]\d+), Y([+-]\d+)")]
        private static partial Regex ButtonRegex();

        [GeneratedRegex(@"Prize: X=(\d+), Y=(\d+)")]
        private static partial Regex PrizeRegex();
    }
}