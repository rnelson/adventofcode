using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 5.
/// </summary>
[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
public class Day05 : Day
{
    private readonly IEnumerable<Rule<int>> _rules;
    private readonly IEnumerable<Update<int>> _validUpdates;
    private readonly IEnumerable<Update<int>> _invalidUpdates;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="Day05"/> class.
    /// </summary>
    /// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
    /// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
    /// <param name="fileSuffix">test file suffix.</param>
    public Day05(ITestOutputHelper output, bool isTest = false, string fileSuffix = "")
        : base(5, output, isTest, fileSuffix)
    {
        (_rules, var allUpdates) = ParseInput();
        var updates = allUpdates.ToArray();
        
        _validUpdates = updates.Where(update => update.IsValid(_rules));
        _invalidUpdates = updates.Where(update => !update.IsValid(_rules));
    }
    
    /// <inheritdoc/>
    public override object PartA() => _validUpdates.Sum(update => update.GetMiddle()).ToString();

    /// <inheritdoc/>
    public override object PartB() => _invalidUpdates.Sum(update => Update<int>.Rearrange(update, _rules).GetMiddle()).ToString();

    private (IEnumerable<Rule<int>>, IEnumerable<Update<int>>) ParseInput()
    {
        var lines = Input.Select(i => i.Trim()).ToArray();
        var delimiter = Array.IndexOf(lines, string.Empty);
        var rulesLines = lines.Take(delimiter).ToArray();
        var updatesLines = lines.Skip(delimiter + 1).ToArray();

        var rules = rulesLines
            .Select(line => line.Split("|").Select(int.Parse).ToArray())
            .Select(pages => new Rule<int>(pages[0], pages[1]))
            .ToList();

        var updates = updatesLines
            .Select(line => line.Split(",").Select(int.Parse).ToArray())
            .Select(pages => new Update<int>(pages))
            .ToList();

        return (rules, updates);
    }

    private record Rule<T>([NotNull] T EarlierPage, [NotNull] T LaterPage) where T : notnull;

    private class Update<T>(T[] pages) where T : notnull
    {
        private readonly T[] _pages = pages;
        
        public T GetMiddle() => _pages[(int)Math.Ceiling(_pages.Length / 2f) - 1];
        
        public bool IsValid(IEnumerable<Rule<T>> rules)
        {
            var rulesArray = rules.ToArray();
            
            foreach (var (index, page) in _pages.Enumerate())
                for (var i = index + 1; i < _pages.Length; i++)
                    if (rulesArray.Any(rule => rule.EarlierPage.Equals(_pages[i]) && rule.LaterPage.Equals(page)))
                        return false;
            
            return true;
        }

        public static Update<T> Rearrange(Update<T> old, IEnumerable<Rule<T>> rules)
        {
            var update = new Update<T>(old._pages.ToArray());
            Array.Sort(update._pages, SortPages(rules));
            return update;
        }

        public override string ToString() => string.Join(",", _pages);

        private static UpdateComparer<T> SortPages(IEnumerable<Rule<T>> rules) => new(rules);

        private class UpdateComparer<TCompare>(IEnumerable<Rule<TCompare>> rules)
            : IComparer<TCompare>
            where TCompare : notnull
        {
            public int Compare(TCompare? x, TCompare? y)
            {
                if (x is null && y is null)
                    return 0;
                
                if (rules.Any(r => r.EarlierPage.Equals(x) && r.LaterPage.Equals(y)))
                    return -1;

                if (rules.Any(r => r.EarlierPage.Equals(y) && r.LaterPage.Equals(x)))
                    return 1;
                
                return 0;
            }
        }
    }
}