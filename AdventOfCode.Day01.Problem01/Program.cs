using System;
using System.IO;

namespace AdventOfCode.Day01.Problem01;

internal class Program
{
    private static void Main()
    {
        var fileName = "day01.txt";
        var lines = File.ReadAllLines(fileName);

        var left = new int[lines.Length];
        var right = new int[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {
            var str = lines[i];
            var intStrs = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            left[i] = int.Parse(intStrs[0]);
            right[i] = int.Parse(intStrs[1]);
        }

        Array.Sort(left);
        Array.Sort(right);

        long totalDiff = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            totalDiff += Math.Abs(left[i] - right[i]);
        }

        Console.WriteLine(totalDiff);
    }
}