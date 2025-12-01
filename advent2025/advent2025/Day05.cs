using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Libexec.Advent.Extensions;
using Xunit.Abstractions;

namespace advent2025;

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
        : base(2024, 5, output, isTest, fileSuffix)
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

    /// <summary>
    /// Parses the collection of input strings to determine all rules and updates.
    /// </summary>
    /// <returns>A tuple containing <see cref="Rule{T}"/>s and <see cref="Update{T}"/>s.</returns>
    private (IEnumerable<Rule<int>>, IEnumerable<Update<int>>) ParseInput()
    {
        var lines = Input.Select(i => i.Trim()).ToArray();
        
        // Find the blank line separating rules and updates, then take the respective two groups and shove them
        // into arrays.
        var delimiter = Array.IndexOf(lines, string.Empty);
        var rulesLines = lines.Take(delimiter).ToArray();
        var updatesLines = lines.Skip(delimiter + 1).ToArray();

        // Split each line (on "|" for rules, "," for updates) and send those bits to int.Parse, then build
        // the appropriate Rule or Update instances.
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

    /// <summary>
    /// An individual rule.
    /// </summary>
    /// <param name="EarlierPage">The <typeparamref name="T"/> that must come before <paramref name="LaterPage"/>.</param>
    /// <param name="LaterPage">The <typeparamref name="T"/> that must come after <paramref name="EarlierPage"/>.</param>
    /// <typeparam name="T">The type for <paramref name="EarlierPage"/> and <paramref name="LaterPage"/>.</typeparam>
    private record Rule<T>(T EarlierPage, T LaterPage) where T : notnull;

    /// <summary>
    /// A list of pages comprising an update.
    /// </summary>
    /// <param name="pages">The <typeparamref name="T"/>s that make up the update.</param>
    /// <typeparam name="T">The type for <paramref name="pages"/>.</typeparam>
    private class Update<T>(T[] pages) where T : notnull
    {
        private readonly T[] _pages = pages;
        
        /// <summary>
        /// Gets the middle item in <see cref="pages"/>.
        /// </summary>
        /// <returns>The middle <see cref="T"/> in <see cref="pages"/>.</returns>
        public T GetMiddle() => _pages[(int)Math.Floor(_pages.Length / 2f)];
        
        /// <summary>
        /// Determines whether this update is valid for the specified <paramref name="rules"/>.
        /// </summary>
        /// <param name="rules">The collection of <see cref="Rule{T}"/>s that must be followed.</param>
        /// <returns><c>true</c> if the update is valid, otherwise <c>false</c>.</returns>
        public bool IsValid(IEnumerable<Rule<T>> rules)
        {
            var rulesArray = rules.ToArray();
            
            // Iterate through every page in _pages, then compare all values after it in the update. If any of them
            // violate `rules`, nope out.
            foreach (var (index, page) in _pages.Enumerate())
                for (var i = index + 1; i < _pages.Length; i++)
                    if (rulesArray.Any(rule => rule.EarlierPage.Equals(_pages[i]) && rule.LaterPage.Equals(page)))
                        return false;
            
            return true;
        }

        /// <summary>
        /// Rearranges the pages in <paramref name="old"/> to compile with the <paramref name="rules"/>.
        /// </summary>
        /// <param name="old">The original <see cref="Update{T}"/>.</param>
        /// <param name="rules">The <see cref="Rule{T}"/>s that must be followed.</param>
        /// <returns>The type of data in <paramref name="old"/>/<paramref name="rules"/>.</returns>
        /// <remarks>
        /// This is the bit I'm most proud of here. Rather than go through a bunch of work to manually sort
        /// the list, I just make sure it's in array and call Array.Sort() on it, passing in an
        /// <see cref="IComparer{T}"/>. It will ensure the entire list is sorted appropriately and I don't
        /// have to worry about it. :-)
        /// </remarks>
        public static Update<T> Rearrange(Update<T> old, IEnumerable<Rule<T>> rules)
        {
            var update = new Update<T>(old._pages.ToArray());
            Array.Sort(update._pages, SortPages(rules));
            return update;
        }

        /// <inheritdoc/>
        public override string ToString() => string.Join(",", _pages);

        /// <summary>
        /// Creates an <see cref="UpdateComparer{TCompare}"/> to sort an update with.
        /// </summary>
        /// <param name="rules">The <see cref="Rule{T}"/>s to follow.</param>
        /// <returns>An <see cref="UpdateComparer{TCompare}"/> that will sort the update.</returns>
        private static UpdateComparer<T> SortPages(IEnumerable<Rule<T>> rules) => new(rules);

        /// <summary>
        /// An <see cref="IComparer{T}"/> used for sorting the pages in an update.
        /// </summary>
        /// <param name="rules">The <see cref="Rule{T}"/>s that must be followed.</param>
        /// <typeparam name="TCompare">The type of data in the <see cref="Update{T}._pages"/>.</typeparam>
        private class UpdateComparer<TCompare>(IEnumerable<Rule<TCompare>> rules)
            : IComparer<TCompare>
            where TCompare : notnull
        {
            /// <inheritdoc/>
            public int Compare(TCompare? x, TCompare? y)
            {
                // While IComparer<T> supports nulls, custom generics in Day05 all specify that
                // the contained type is notnull. Just err out if somehow that constraint is violated.
                if (x is null || y is null)
                    throw new NotSupportedException("Cannot compare with a null value");
                
                // If `x` should come before `y` according to the rules, return -1 so whatever is using
                // the comparer knows to keep `x` earlier.
                if (rules.Any(r => r.EarlierPage.Equals(x) && r.LaterPage.Equals(y)))
                    return -1;
                
                // If `y` should come before `x` according to the rules, return 1 so whatever is using
                // the comparer knows to keep `x` later.
                if (rules.Any(r => r.EarlierPage.Equals(y) && r.LaterPage.Equals(x)))
                    return 1;
                
                // Neither before nor after? Mark them as equal and call it a day.
                return 0;
            }
        }
    }
}