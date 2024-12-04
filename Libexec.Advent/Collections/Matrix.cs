using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Libexec.Advent.Collections;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class Matrix<T>(int xSize, int ySize)
{
    //private readonly T[,] _data = new T[xSize, ySize];
    private readonly T[] _data = new T[xSize * ySize];
    
    /// <summary>
    /// Gets the size of the matrix.
    /// </summary>
    /// <remarks>
    /// The first item is the width, the second item is the height.
    /// </remarks>
    public (int, int) Size { get; } = (xSize, ySize);

    /// <summary>
    /// Creates a new <see cref="Matrix{T}"/> of size <paramref name="xSize"/>x<paramref name="ySize"/> containing
    /// the data in <paramref name="data"/>.
    /// </summary>
    /// <param name="data">The data to put into the matrix.</param>
    /// <param name="xSize">The width of the data.</param>
    /// <param name="ySize">The height of the data.</param>
    /// <returns>A new <see cref="Matrix{T}"/>.</returns>
    public static Matrix<T> Create(T[,] data, int xSize, int ySize)
    {
        var m = new Matrix<T>(xSize, ySize);
        
        for (var x = 0; x < xSize; x++)
            for (var y = 0; y < ySize; y++)
                m[x, y] = data[x, y];

        return m;
    }

    /// <summary>
    /// Gets or sets the element at (<paramref name="x"/>, <paramref name="y"/>).
    /// </summary>
    /// <param name="x">The X coordinate.</param>
    /// <param name="y">The Y coordinate.</param>
    public T this[int x, int y]
    {
        //get => _data[x, y];
        //set => _data[x, y] = value;
        
        get => _data[x * xSize + y];
        set => _data[x * xSize + y] = value;
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
        for (var x = 0; x < Size.Item1; x++)
        {
            for (var y = 0; y < Size.Item2; y++)
                sb.Append($"{this[x, y]} ");
            sb.AppendLine();
        }
        
        return sb.ToString();
    }
}