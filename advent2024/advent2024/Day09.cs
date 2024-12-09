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
        var yuck = Fragment(disk);
        return Checksum(yuck);
    }

    /// <inheritdoc/>
    public override object PartB()
    {
        return "";
    }

    private char[] ParseInput()
    {
        var disk = new List<char>();
        var input = Input.First().ToCharArray();

        var currentFile = 0;
        var onFile = input.Last() != '.';
        
        foreach (var ch in input)
        {
            var n = int.Parse(ch.ToString());
            
            if (onFile)
                for (var i = 0; i < n; i++) disk.Add(currentFile.ToString()[0]);
            else
            {
                for (var i = 0; i < n; i++) disk.Add('.');
                currentFile++;
            }

            onFile = !onFile;
        }

        return disk.ToArray();
    }

    private char[] Defragment(char[] disk)
    {
        var filesOnly = disk.ToList();
        filesOnly.RemoveAll(b => b == '.');

        for (var i = 0; i < disk.Length - filesOnly.Count(); i++)
            filesOnly.Add('.');
        
        return filesOnly.ToArray();
    }

    private char[] Fragment(char[] disk)
    {
        var newFilesystem = disk.ToArray();
        
        for (var i = newFilesystem.Length - 1; i > 0; i--)
        {
            if (newFilesystem[i] == '.')
                continue;

            var openSpace = Array.IndexOf(newFilesystem, '.');

            if (openSpace > i)
                break;
            
            newFilesystem[openSpace] = newFilesystem[i];
            newFilesystem[i] = '.';
        }
        
        return newFilesystem;
    }

    private ulong Checksum(char[] disk)
    {
        var result = 0UL;

        for (var i = 0; i < disk.Length; i++)
        {
            if (disk[i] == '.')
                break;
            
            var digit = disk[i] - '0';
            result += (ulong)digit * (ulong)i;
        }

        return result;
    }
}