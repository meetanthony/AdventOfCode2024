using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day04.Problem01;

internal class Program
{
    private const int CharsCount = 4;

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
                var enoughX = x + CharsCount <= lineLength;
                var enoughY = y + CharsCount <= lines.Length;
                if (enoughX)
                    if (Horizontal(lines, y, x))
                        xmasCounter++;

                if (enoughY)
                {
                    if (Vertical(lines, y, x))
                        xmasCounter++;
                }

                if (enoughX && enoughY)
                {
                    if (DiagonalDown(lines, y, x))
                        xmasCounter++;

                    if (DiagonalUp(lines, y, x))
                        xmasCounter++;
                }
            }
        }

        Console.WriteLine(xmasCounter);
    }

    private static bool Horizontal(string[] strs, int y, int x)
    {
        var chars = string.Empty;
        for (int i = 0; i < CharsCount; i++)
        {
            chars += (strs[y][x + i]);
        }

        return chars == XMAS || chars == SAMX;
    }

    private static bool Vertical(string[] strs, int y, int x)
    {
        var chars = string.Empty;
        for (int i = 0; i < CharsCount; i++)
        {
            chars += (strs[y + i][x]);
        }

        return chars == XMAS || chars == SAMX;
    }

    private static bool DiagonalDown(string[] strs, int y, int x)
    {
        var chars = string.Empty;
        for (int i = 0; i < CharsCount; i++)
        {
            chars += (strs[y + i][x + i]);
        }

        return chars == XMAS || chars == SAMX;
    }

    private static bool DiagonalUp(string[] strs, int y, int x)
    {
        var chars = string.Empty;
        for (int i = 0; i < CharsCount; i++)
        {
            chars += (strs[y + CharsCount - 1 - i][x + i]);
        }

        return chars == XMAS || chars == SAMX;
    }

    private const string XMAS = "XMAS";
    private const string SAMX = "SAMX";
}