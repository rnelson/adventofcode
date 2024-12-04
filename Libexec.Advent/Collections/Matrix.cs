using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Libexec.Advent.Collections;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class Matrix<T>(int rows, int columns)
{
    private readonly T[] _data = new T[rows * columns];
    
    /// <summary>
    /// Gets the size of the matrix.
    /// </summary>
    /// <remarks>
    /// The first item is the number of rows, the second item is the number of columns.
    /// </remarks>
    public (int, int) Size { get; } = (rows, columns);

    /// <summary>
    /// Creates a new <see cref="Matrix{T}"/> of size <paramref name="rows"/>x<paramref name="columns"/> containing
    /// the data in <paramref name="data"/>.
    /// </summary>
    /// <param name="data">The data to put into the matrix.</param>
    /// <param name="rows">The width of the data.</param>
    /// <param name="columns">The height of the data.</param>
    /// <returns>A new <see cref="Matrix{T}"/>.</returns>
    public static Matrix<T> Create(T[,] data, int rows, int columns)
    {
        var m = new Matrix<T>(rows, columns);
        
        for (var r = 0; r < rows; r++)
            for (var c = 0; c < columns; c++)
                m[r, c] = data[r, c];

        return m;
    }

    /// <summary>
    /// Gets or sets the element at (<paramref name="row"/>, <paramref name="column"/>).
    /// </summary>
    /// <param name="row">The row.</param>
    /// <param name="column">The Y coordinate.</param>
    public T this[int row, int column]
    {
        get => _data[row * columns + column];
        set => _data[row * columns + column] = value;
    }
    
    /// <summary>
    /// Determines whether this matrix contains a data point at (<paramref name="x"/>, <paramref name="y"/>).
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    /// <returns><c>true</c> if (<paramref name="x"/>, <paramref name="y"/>) is a valid point in the matrix.</returns>
    public bool ContainsPoint(int x, int y) => x >= 0 && x < Size.Item1 && y >= 0 && y < Size.Item2;

    /// <inheritdoc/>
    public override string ToString()
    {
        var sb = new StringBuilder();
        for (var r = 0; r < Size.Item1; r++)
        {
            for (var c = 0; c < Size.Item2; c++)
                sb.Append($"{this[r, c]}");
            sb.AppendLine();
        }
        
        return sb.ToString();
    }
}