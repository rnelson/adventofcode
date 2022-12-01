﻿using System.Globalization;
using System.Numerics;
using advent2022.Share;

namespace advent2022;

public abstract class DayBase : IDay
{
    /// <inheritdoc />
    public abstract object A(IEnumerable<string> input);

    /// <inheritdoc />
    public abstract object B(IEnumerable<string> input);

    /// <summary>
    /// Gets the input as a list of <typeparamref name="T"/>s.
    /// </summary>
    /// <param name="input">The puzzle input.</param>
    /// <typeparam name="T">The numeric type to parse input as.</typeparam>
    /// <returns>The parsed inputs.</returns>
    protected static IList<T> GetNumericInputs<T>(IEnumerable<string> input)
        where T: INumber<T> =>
        input.Select(s => T.Parse(s, CultureInfo.CurrentCulture)).ToList();

    /// <summary>
    /// Loads the input file and returns its values as a list of strings.
    /// </summary>
    /// <returns>The contents of the input file.</returns>
    public IEnumerable<string> LoadInput()
    {
        var typeName = GetType().Name;
        var inputFile = $"Input/{typeName}.txt";

        return File.ReadAllLines(inputFile);
    }
}