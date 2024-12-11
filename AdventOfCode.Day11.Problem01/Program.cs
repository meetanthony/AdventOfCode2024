using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day11.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day11.txt";
        var lines = File.ReadAllLines(fileName);
        var line = lines[0];

        var strs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<long> stones = strs.Select(long.Parse).ToList();

        int blinkCount = 25;

        for (int i = 0; i < blinkCount; i++)
        {
            stones = Blink(stones);

            /*foreach (var stone in stones)
            {
                Console.Write(stone + " ");
            }
            Console.WriteLine();*/
        }

        Console.WriteLine(stones.Count);
    }

    private static List<long> Blink(List<long> stones)
    {
        var result = new List<long>();
        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                result.Add(1);
                continue;
            }
            var stoneStr = stone.ToString();
            if (stoneStr.Length % 2 == 0)
            {
                var halfLength = stoneStr.Length / 2;
                var firstStone = long.Parse(stoneStr.Substring(0, halfLength));
                var secondStone = long.Parse(stoneStr.Substring(halfLength));
                result.Add(firstStone);
                result.Add(secondStone);
                continue;
            }
            
            result.Add(stone * 2024);
        }

        return result;
    }
}