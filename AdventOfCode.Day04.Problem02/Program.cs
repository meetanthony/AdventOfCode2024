using System;
using System.IO;

namespace AdventOfCode.Day04.Problem02;

internal class Program
{
    private const string MAS = "MAS";
    private const string SAM = "SAM";
    private const int MasLength = 3;

    private static void Main()
    {
        const string fileName = "day04.txt";
        var lines = File.ReadAllLines(fileName);

        var xmasCounter = 0;

        for (int y = 0; y < lines.Length; y++)
        {
            var lineLength = lines[y].Length;
            for (int x = 0; x < lines[y].Length; x++)
            {
                var enoughX = x + MasLength <= lineLength;
                var enoughY = y + MasLength <= lines.Length;

                if (enoughX && enoughY)
                {
                    if (MasDigonales(lines, y, x))
                        xmasCounter++;
                }
            }
        }

        Console.WriteLine(xmasCounter);
    }

    private static bool MasDigonales(string[] strs, int y, int x)
    {
        var chars = string.Empty;
        for (int i = 0; i < MasLength; i++)
        {
            chars += strs[y + MasLength - 1 - i][x + i];
        }

        if (chars != MAS && chars != SAM)
            return false;

        chars = string.Empty;
        for (int i = 0; i < MasLength; i++)
        {
            chars += strs[y + i][x + i];
        }

        if (chars != MAS && chars != SAM)
            return false;

        return true;
    }
}