using System;
using System.IO;

namespace AdventOfCode.Day01.Problem02;

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

        var maxValue = Math.Max(left[^1], right[^1]);
        var counters = new int[maxValue + 1];
        for (int i = 0; i < right.Length; i++)
        {
            counters[right[i]] += 1;
        }

        long accum = 0;

        for (int i = 0; i < left.Length; i++)
        {
            var value = left[i];
            accum += value * counters[value];
        }

        Console.WriteLine(accum);
    }
}