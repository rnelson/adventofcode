using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Text;
using Libexec.Advent;
using MathEvaluation.Extensions;
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
            var equations = GetEquationPossibilities(item.Item2, _partASymbols, addParens: true);
            foreach (var equation in equations)
            {
                var math = (ulong)equation.Evaluate();
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
            var equations = AddParens(GetEquationPossibilities(item.Item2, _partBSymbols), _partBSymbols).ToArray();
            foreach (var equation in equations)
            {
                var math = (ulong)equation.Evaluate();
                if (math != item.Item1)
                    continue;
                
                sum += math;
                break;
            }
        }
        
        return sum;
    }

    private IEnumerable<string> GetEquationPossibilities<T>(IEnumerable<T> components, char[] symbols, bool addParens = false)
        where T : INumber<T>
    {
        var bits = components.Select(b => b.ToString()!).ToArray();
        var s = string.Join(" ", bits);

        return AddSymbols(s, symbols, addParens);
    }

    private static IEnumerable<string> AddSymbols(string s, char[] symbols, bool addParens = false)
    {
        if (!s.Contains(' '))
        {
            var sb = new StringBuilder();
            
            if (addParens)
                for (var i = 0; i < s.Count(c => c == ')') ; i++)
                    sb.Append('(');
            
            sb.Append(s);
            
            yield return sb.ToString();
        }
        
        var idx = s.IndexOf(' ');

        if (idx >= 0)
            foreach (var symbol in symbols)
            {
                var sb = new StringBuilder();

                if (symbol == '|')
                {
                    sb.Append(s);
                    //sb.Append(s[..idx]);
                    //sb.Append(s[(idx + 1)..]);
                    
                    if (addParens) sb.Append(") ");
                }
                else
                {
                    var nextSpace = s.IndexOf(' ', idx + 1);
                    sb.Append(s[..idx]);
                    sb.Append(symbol);

                    if (nextSpace >= 0)
                    {
                        sb.Append(s[(idx + 1)..nextSpace]);
                        if (addParens) sb.Append(") ");
                        sb.Append(s[(nextSpace + 1)..]);
                    }
                    else
                    {
                        sb.Append(s[(idx + 1)..]);
                    }

                    if (addParens) sb.Append(')');
                }

                foreach (var u in AddSymbols(sb.ToString().Trim(), symbols, addParens))
                    yield return u;
            }
    }

    private IEnumerable<string> AddParens(IEnumerable<string> items, char[] symbols)
    {
        var modified = new List<string>();

        foreach (var item in items)
        {
            if (item.Count(c => !char.IsDigit(c)) < 2)
            {
                modified.Add(item);
                continue;
            }

            var sb = new StringBuilder();
            
            // Find the first symbol. We're ignoring this one intentionally.
            var left = item.IndexOfAny(symbols);
            var lastLeft = 0;

            while (true)
            {
                // Find the next symbol.
                left = item.IndexOfAny(symbols, left + 1);

                // If we're all done, add the rest of the string and bail.
                if (left < 0)
                {
                    sb.Append(item[lastLeft..]);
                    break;
                }
                
                // Add everything in that range to our result.
                sb.Append(item[lastLeft..left]);
                
                // Add a close paren
                sb.Append(')');
                
                // Update our last left position.
                lastLeft = left;
            }
            
            // Balance parentheses.
            for (var i = 0; i < sb.ToString().Count(c => c == ')'); i++)
                sb.Insert(0, '(');
            
            modified.Add(sb.ToString());
        }

        return modified;
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