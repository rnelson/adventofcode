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
        return "";
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

    private ulong Fragment(int[] disk)
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
}