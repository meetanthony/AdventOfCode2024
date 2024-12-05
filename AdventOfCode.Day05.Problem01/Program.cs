using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day05.Problem01;

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

        var result = 0;
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
                        goto NextUpdate;
                }
            }

            result += values[values.Count/2];

            NextUpdate: ;
        }


        Console.WriteLine(result);
    }
}