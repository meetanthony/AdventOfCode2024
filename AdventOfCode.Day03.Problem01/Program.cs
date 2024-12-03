using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day03.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day03.txt";
        var lines = File.ReadAllLines(fileName);

        var megaline = string.Concat(lines);

        var regex = new Regex(@"mul\((\d+),\s*(\d+)\)");
        MatchCollection matches = regex.Matches(megaline);

        long result = 0;

        foreach (Match match in matches)
        {
            int first = int.Parse(match.Groups[1].Value);
            int second = int.Parse(match.Groups[2].Value);

            result += first * second;
        }

        Console.WriteLine(result);
    }
}