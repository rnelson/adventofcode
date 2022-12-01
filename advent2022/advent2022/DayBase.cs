using System.Globalization;
using System.Numerics;
using advent2022.Share;

namespace advent2022;

public abstract class DayBase : IDay
{
    /// <inheritdoc />
    public IEnumerable<string>? Input { get; set; } = null;

    /// <inheritdoc />
    public abstract (object, object) Solve();

    /// <summary>
    /// Gets <paramref name="input"/> as a list of <typeparamref name="T"/>s.
    /// </summary>
    /// <param name="input">The enumerable string list to convert to a list of <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The numeric type to parse input as.</typeparam>
    /// <returns>The parsed inputs.</returns>
    protected static IList<T> GetNumbers<T>(IEnumerable<string> input)
        where T: INumber<T> =>
        input.Select(s => T.Parse(s, CultureInfo.CurrentCulture)).ToList();

    /// <inheritdoc />
    public IEnumerable<string> LoadInput()
    {
        var typeName = GetType().Name;
        var inputFile = $"Input/{typeName}.txt";

        return File.ReadAllLines(inputFile);
    }
}