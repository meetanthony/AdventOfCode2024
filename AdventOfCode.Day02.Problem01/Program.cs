using System;
using System.IO;

namespace AdventOfCode.Day02.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day02.txt";
        var lines = File.ReadAllLines(fileName);

        int safeReportsCounter = 0;

        for (int i = 0; i < lines.Length; i++)
        {
            var str = lines[i];
            var intStrs = str.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int diffAggregator = 0;

            for (int j = 0; j < intStrs.Length - 1; j++)
            {
                var j0 = int.Parse(intStrs[j]);
                var j1 = int.Parse(intStrs[j + 1]);

                var diff = j0 - j1;
                if (Math.Abs(diff) > 3 || diff == 0)
                    goto NextReport;

                if (diff * diffAggregator < 0)
                    goto NextReport;

                diffAggregator += diff;
            }

            safeReportsCounter++;

        NextReport:;
        }

        Console.WriteLine(safeReportsCounter);
    }
}