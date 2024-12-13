using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2024;

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
        return "";
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return "";
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
        int buttonACost = 3,
        int buttonBCost = 1)
    {
        public Button A { get; private init; } = new(aMovement.Item1, aMovement.Item2);
        public int ButtonACost { get; private init; } = buttonACost;
        
        public Button B { get; private init; } = new(bMovement.Item1, bMovement.Item2);
        public int ButtonBCost { get; private init; } = buttonBCost;
        
        public Location Prize { get; private init; } = new(prizeLocation.Item1, prizeLocation.Item2);

        public ulong Solve()
        {
            //A=80,B=40
            //80*94 + 40*22 = 8400
            //80*34 + 40*67 = 5400
            
            // aCount * A.X + bCount * B.X = Prize.X
            // aCount * A.Y + bCount & B.Y = Prize.Y
            
            // px = A*ax + B*bx
            // py = A*ay + B*by
            
            // px = A*ax + B*bx
            // A*ax = px - B*bx
            // A = (px - B*bx) / ax
            
            // py = A*ay + B*by
            // B = (py - A*ay) / by
            
            // A = (px - ((py - A*ay) / by)*bx) / ax
            // A = px - 
            
            // B = (py - ((px - B*bx) / ax)*ay) / by
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