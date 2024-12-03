using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day03.Problem02;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day03.txt";
        var lines = File.ReadAllLines(fileName);

        var megaline = string.Concat(lines);

        megaline = PrepareMegaline(megaline);
        
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

    private static string PrepareMegaline(string megaline)
    {
        const string doString = "do()";
        const string dontString = "don't()";

        var dontCounter = 0;
        var doCounter = 0;
        while (megaline.Contains(dontString))
        {
            dontCounter++;
            var dontIndex = megaline.IndexOf(dontString, StringComparison.Ordinal);
            var nextDoIndex = megaline.IndexOf(doString, dontIndex, StringComparison.Ordinal);
            if (nextDoIndex == -1)
                nextDoIndex = megaline.Length-1;
            else
                doCounter++;
            megaline = megaline.Remove(dontIndex, nextDoIndex - dontIndex);

        }
        
        return megaline;
    }
}