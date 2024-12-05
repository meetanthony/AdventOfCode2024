using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day05.Problem02;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day05.txt";
        var lines = File.ReadAllLines(fileName);

        var mustBeRight = new Dictionary<int, HashSet<int>>();

        List<string> rules = new List<string>();
        List<string> updates = new List<string>();
        foreach (string line in lines)
        {
            if (line.Contains("|"))
                rules.Add(line);
            else if (line.Contains(","))
                updates.Add(line);
        }

        foreach (var rule in rules)
        {
            var strs = rule.Split("|");
            int a = int.Parse(strs[0]);
            int b = int.Parse(strs[1]);

            if (mustBeRight.ContainsKey(a) == false)
                mustBeRight.Add(a, new HashSet<int>());
            mustBeRight[a].Add(b);
        }
        var incorrectUpdates = new List<List<int>>();
        foreach (var update in updates)
        {
            List<int> values = new List<int>();
            var strs = update.Split(",");
            foreach (var str in strs)
            {
                values.Add(int.Parse(str));
            }

            for (int i = 1; i < values.Count; i++)
            {
                var current = values[i];
                if (mustBeRight.ContainsKey(current) == false)
                    continue;
                for (int j = 0; j < i; j++)
                {
                    var left = values[j];
                    if (mustBeRight[current].Contains(left))
                    {
                        incorrectUpdates.Add(values);
                        goto NextUpdate;
                    }
                }
            }

            NextUpdate:;
        }

        var result = 0;

        foreach (var incorrectUpdate in incorrectUpdates)
        {
            var correct = true;
            do
            {
                correct = true;
                for (int i = 1; i < incorrectUpdate.Count; i++)
                {
                    var current = incorrectUpdate[i];
                    if (mustBeRight.ContainsKey(current) == false)
                        continue;
                    for (int j = 0; j < i; j++)
                    {
                        var left = incorrectUpdate[j];
                        if (mustBeRight[current].Contains(left))
                        {
                            correct = false;
                            (incorrectUpdate[i], incorrectUpdate[j]) = (incorrectUpdate[j], incorrectUpdate[i]);
                        }
                    }
                }
            } while (correct == false);

            var res = incorrectUpdate[incorrectUpdate.Count / 2];
            result += res;
        }

        Console.WriteLine(result);
    }
}