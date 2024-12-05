using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 5.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day05(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(5, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var (rules, updates) = ParseInput();
        var validUpdates = updates.Where(update => update.IsValid(rules)).ToArray();
        return validUpdates.Sum(update => update.GetMiddle()).ToString();
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var (rules, updates) = ParseInput();
        var invalidUpdates = updates.Where(update => !update.IsValid(rules)).ToArray();
        return invalidUpdates.Sum(update => Update<int>.Rearrange(update, rules).GetMiddle()).ToString();
    }

    private (IEnumerable<Rule<int>>, IEnumerable<Update<int>>) ParseInput()
    {
        var lines = Input.ToArray();
        var rules = new List<Rule<int>>();
        var updates = new List<Update<int>>();

        var lineNumber = 0;
        
        // Parse rules.
        while (!"".Equals(lines[lineNumber].Trim()))
        {
            var pages = lines[lineNumber].Trim().Split("|").Select(int.Parse).ToArray();
            rules.Add(new(pages[0], pages[1]));
            lineNumber++;
        }
        
        // Skip past the blank line.
        lineNumber++;
        
        // Parse updates.
        while (lineNumber < lines.Length)
        {
            var pages = lines[lineNumber].Trim().Split(",").Select(int.Parse).ToArray();
            updates.Add(new(pages));
            lineNumber++;
        }
        
        return (rules, updates);
    }

    private record Rule<T>([NotNull] T EarlierPage, [NotNull] T LaterPage);

    private class Update<T>(T[] pages)
    {
        private readonly T[] _pages = pages;
        
        public T GetMiddle() => _pages[(int)Math.Ceiling(_pages.Length / 2f) - 1];
        
        public bool IsValid(IEnumerable<Rule<T>> rules)
        {
            var rulesArray = rules.ToArray();
            
            foreach (var (index, page) in _pages.Enumerate())
            {
                for (var i = index + 1; i < _pages.Length; i++)
                {
                    if (rulesArray.Any(rule => rule.EarlierPage!.Equals(_pages[i]) && rule.LaterPage!.Equals(page)))
                        return false;
                }
            }
            
            return true;
        }

        public static Update<T> Rearrange(Update<T> old, IEnumerable<Rule<T>> rules)
        {
            var update = new Update<T>(old._pages.ToArray());
            Array.Sort(update._pages, SortPages(rules));
            return update;
        }

        public override string ToString()
        {
            return string.Join(",", _pages);
        }

        private static UpdateComparer<T> SortPages(IEnumerable<Rule<T>> rules) => new(rules);

        private class UpdateComparer<TCompare>(IEnumerable<Rule<TCompare>> rules) : IComparer<TCompare>
        {
            public int Compare(TCompare? x, TCompare? y)
            {
                if (x is null && y is null)
                    return 0;

                if (rules.Any(r => r.EarlierPage!.Equals(x) && r.LaterPage!.Equals(y)))
                    return -1;
                
                return rules.Any(r => r.EarlierPage!.Equals(y) && r.LaterPage!.Equals(x)) ? 1 : 0;
            }
        }
    }
}