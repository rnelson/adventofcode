using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 7.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day07(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 7, output, isTest, fileSuffix)
{
    private readonly char[] _partASymbols = ['*', '+'];
    private readonly char[] _partBSymbols = ['|', '*', '+'];

    /// <inheritdoc/>
    public override object PartA()
    {
        var input = ParseInput<ulong>().ToArray();
        var sum = (ulong)0;
        
        foreach (var item in input)
        {
            var equations = GetEquationPossibilities(item.Item2, _partASymbols);
            foreach (var equation in equations)
            {
                var math = equation.DoMath_2024d7();
                if (math != item.Item1)
                    continue;
                
                sum += math;
                break;
            }
        }
        
        return sum;
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var input = ParseInput<ulong>().ToArray();
        var sum = (ulong)0;
        
        foreach (var item in input)
        {
            var equations = GetEquationPossibilities(item.Item2, _partBSymbols).ToArray();
            foreach (var equation in equations)
            {
                var math = equation.DoMath_2024d7();
                if (math != item.Item1)
                    continue;
                
                sum += math;
                break;
            }
        }
        
        return sum;
    }

    private IEnumerable<string> GetEquationPossibilities<T>(IEnumerable<T> components, char[] symbols)
        where T : INumber<T>
    {
        var bits = components.Select(b => b.ToString()!).ToArray();
        var s = string.Join(" ", bits);

        return AddSymbols(s, symbols);
    }

    private static IEnumerable<string> AddSymbols(string s, char[] symbols)
    {
        if (!s.Contains(' '))
            yield return s;
        
        var idx = s.IndexOf(' ');

        if (idx >= 0)
            foreach (var symbol in symbols)
            {
                var sb = new StringBuilder();

                if (symbol == '|')
                {
                    sb.Append(s[..idx]);
                    sb.Append(symbol);
                    sb.Append(s[(idx + 1)..]);
                }
                else
                {
                    var nextSpace = s.IndexOf(' ', idx + 1);
                    sb.Append(s[..idx]);
                    sb.Append(symbol);

                    if (nextSpace >= 0)
                    {
                        sb.Append(s[(idx + 1)..]);
                        //sb.Append(s[(idx + 1)..nextSpace]);
                        //sb.Append(s[(nextSpace + 1)..]);
                    }
                    else
                    {
                        sb.Append(s[(idx + 1)..]);
                    }
                }

                foreach (var u in AddSymbols(sb.ToString().Trim(), symbols))
                    yield return u;
            }
    }

    private List<(T, IEnumerable<T>)> ParseInput<T>()
        where T : INumber<T> =>
        (from line in Input
            select line.Split(": ")
            into halves
            let answer = T.Parse(halves[0], NumberStyles.Integer, CultureInfo.InvariantCulture)
            let components = Enumerable.Select(halves[1]
                .Split(" "), c => T.Parse(c, NumberStyles.Integer, CultureInfo.InvariantCulture))
            select (answer, components)).ToList();
}