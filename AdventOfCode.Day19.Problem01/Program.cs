using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace AdventOfCode.Day19.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "TestData\\day19.txt";
        var lines = File.ReadAllLines(fileName);
        
        var sw = Stopwatch.StartNew();

        var baseCombinations = lines[0].Split(", ");

        Array.Sort(baseCombinations, (s0, s1) => s1.Length - s0.Length);

        var possibleCombinationsCounter = 0;
        var impossibleCombinationsCounter = 0;
        for (int lineIndex = 2; lineIndex < lines.Length; lineIndex++)
        {
            var targetCombination = lines[lineIndex];
            var variants = IsPossibleCombination(new List<string>(baseCombinations), targetCombination);
            if (variants)
            {
                possibleCombinationsCounter++;
            }
            else
            {
                impossibleCombinationsCounter++;
                //Console.WriteLine(design);
            }
            Console.WriteLine($"{lineIndex - 2}/{lines.Length - 2}");
        }

        Console.WriteLine();
        Console.WriteLine(possibleCombinationsCounter);
        Console.WriteLine(impossibleCombinationsCounter);

        Console.WriteLine(sw.Elapsed);
    }

    private static bool IsPossibleCombination(List<string> patterns, string design)
    {
        for (int i = 0; i < patterns.Count; i++)
        {
            if (design.IndexOf(patterns[i], StringComparison.Ordinal) == -1)
            {
                patterns.RemoveAt(i);
                i--;
            }
        }

        for (int i = 0; i < patterns.Count; i++)
        {
            if (IsPossibleCombination(patterns, i, design))
                return true;
        }

        return false;
    }

    private static bool IsPossibleCombination(List<string> patterns, int patternIndex, string design)
    {
        var pattern = patterns[patternIndex];
        var patternPos = design.IndexOf(pattern, StringComparison.Ordinal);

        if (patternPos != 0)
            return false;

        design = design.Remove(0, pattern.Length);

        if (design.Length == 0)
            return true;

        for (int i = 0; i < patterns.Count; i++)
        {
            if (IsPossibleCombination(patterns, i, design))
                return true;
        }

        return false;
    }
}