using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode.Day19.Problem02;

internal class Program
{
    private static void Main()
    {
        const string fileName = "TestData\\day19.txt";
        var lines = File.ReadAllLines(fileName);
        
        var sw = Stopwatch.StartNew();

        var patterns = lines[0].Split(", ");

        //Array.Sort(baseCombinations, (s0, s1) => s1.Length - s0.Length);

        long result = 0;
        for (int lineIndex = 2; lineIndex < lines.Length; lineIndex++)
        {
            var targetCombination = lines[lineIndex];
            result += IsPossibleCombination(patterns, targetCombination);
            Console.WriteLine($"{lineIndex - 2}/{lines.Length - 2}");
        }

        Console.WriteLine();
        Console.WriteLine(result);

        Console.WriteLine(sw.Elapsed);
    }

    private static long IsPossibleCombination(string[] patterns, string design)
    {
        long result = 0;
        for (int i = 0; i < patterns.Length; i++)
        {
            result += IsPossibleCombination(patterns, i,  design);
        }

        return result;
    }

    private static readonly Dictionary<string, long> _cache = new();
    private static long IsPossibleCombination(
        string[] patterns, int patternIndex, 
        string design)
    {
        var pattern = patterns[patternIndex];
        var patternPos = design.IndexOf(pattern, StringComparison.Ordinal);

        if (patternPos != 0)
            return 0;

        design = design.Remove(0, pattern.Length);

        if (design.Length == 0)
        {
            return 1;
        }

        if (_cache.TryGetValue(design, out var value))
            return value;

        long result = 0;
        for (int i = 0; i < patterns.Length; i++)
        {
            result += IsPossibleCombination(patterns, i, design);
        }

        _cache[design] = result;
        return result;
    }
}