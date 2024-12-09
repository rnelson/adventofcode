using System.Diagnostics.CodeAnalysis;
using Libexec.Advent;
using Xunit.Abstractions;

namespace advent2024;

/// <summary>
/// 2024 day 9.
/// </summary>
/// <param name="output">A <see cref="ITestOutputHelper"/> to use for logging.</param>
/// <param name="isTest"><c>true</c> to load test data, <c>false</c> to load real data.</param>
/// <param name="fileSuffix">test file suffix.</param>
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day09(ITestOutputHelper output, bool isTest = false, string fileSuffix = "") : Day(2024, 9, output, isTest, fileSuffix)
{
    /// <inheritdoc/>
    public override object PartA()
    {
        var disk = ParseInput();
        return Fragment(disk);
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        var disk = ParseInput();
        return DoNotFragment(disk);
    }

    private int[] ParseInput()
    {
        var disk = new List<int>();
        var input = Input.First().ToCharArray();

        var currentFile = 0;
        var onFile = input.Last() != '.';
        
        foreach (var ch in input)
        {
            var n = ch - '0';
            
            if (onFile)
                for (var i = 0; i < n; i++) disk.Add(currentFile);
            else
            {
                for (var i = 0; i < n; i++) disk.Add(-1);
                currentFile++;
            }

            onFile = !onFile;
        }

        return disk.ToArray();
    }

    private static ulong Fragment(int[] disk)
    {
        var checksum = 0UL;
        var newFilesystem = disk.ToArray();
        
        for (var i = newFilesystem.Length - 1; i > 0; i--)
        {
            if (newFilesystem[i] == -1)
                continue;

            var openSpace = Array.IndexOf(newFilesystem, -1);

            if (openSpace > i)
                break;
            
            newFilesystem[openSpace] = newFilesystem[i];
            newFilesystem[i] = -1;
        }

        for (var i = 0; i < newFilesystem.Length; i++)
        {
            if (newFilesystem[i] != -1)
                checksum += (ulong)newFilesystem[i] * (ulong)i;
        }
        
        return checksum;
    }

    private static int FindChunk(int[] disk, int size)
    {
        var startIndex = -1;
        var currentSize = 0;
        var fits = false;
        
        for (var i = 0; i < disk.Length; i++)
        {
            if (disk[i] > -1)
            {
                startIndex = -1;
                currentSize = 0;
                continue;
            }

            if (startIndex < 0)
                startIndex = i;
            
            currentSize++;
            if (currentSize < size)
                continue;
            
            fits = true;
            break;
        }

        return fits ? startIndex : -1;
    }

    private static ulong DoNotFragment(int[] disk)
    {
        var checksum = 0UL;
        var newFilesystem = disk.ToArray();
        
        for (var i = newFilesystem.Length - 1; i > 0; i--)
        {
            if (newFilesystem[i] == -1)
                continue;

            var thisFile = newFilesystem[i];
            var startOfFile = Array.IndexOf(newFilesystem, thisFile);
            var fileSize = i - startOfFile + 1;
            var newLocation = FindChunk(newFilesystem, fileSize);

            if (newLocation > -1 && newLocation < startOfFile)
            {
                Array.Fill(newFilesystem, thisFile, newLocation, fileSize);
                Array.Fill(newFilesystem, -1, startOfFile, fileSize);
            }
            else
                i = startOfFile;
        }

        for (var i = 0; i < newFilesystem.Length; i++)
        {
            if (newFilesystem[i] != -1)
                checksum += (ulong)newFilesystem[i] * (ulong)i;
        }
        
        return checksum;
    }
}