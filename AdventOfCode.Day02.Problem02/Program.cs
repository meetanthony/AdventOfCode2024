using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day02.Problem02;

internal static class Program
{
    private static void Main()
    {
        const string fileName = "day02.txt";
        var lines = File.ReadAllLines(fileName);

        int safeReportsCounter = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var str = lines[i];
            var strs = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var values = new List<int>(strs.Length);
            for (int j = 0; j < strs.Length; j++)
            {
                values.Add(int.Parse(strs[j]));
            }

            var dampeners = 1;
            if (IsItSafe(values, ref dampeners))
            {
                safeReportsCounter++;
            }
        }

        Console.WriteLine(safeReportsCounter);
    }

    private static bool IsItSafe(List<int> values, ref int dampeners)
    {
        int diffAggregator = 0;

        for (int i = 0; i < values.Count - 1; i++)
        {
            var i0 = values[i];
            var i1 = values[i + 1];

            var diff = i0 - i1;
            if (Math.Abs(diff) > 3 || diff == 0 || diff * diffAggregator < 0)
            {
                if (dampeners == 0)
                {
                    return false;
                }

                dampeners--;

                for (int j = 0; j < values.Count; j++)
                {
                    var copy = new List<int>(values);
                    copy.RemoveAt(j);
                    if (IsItSafe(copy, ref dampeners))
                        return true;
                }

                return false;
            }

            diffAggregator += diff;
        }

        return true;
    }
}