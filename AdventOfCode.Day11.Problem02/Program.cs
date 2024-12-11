using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day11.Problem02;

internal class Program
{
    private static void Main()
    {
        var sw = Stopwatch.StartNew();
        const string fileName = "day11.txt";
        var lines = File.ReadAllLines(fileName);
        var line = lines[0];

        var strs = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<ulong> stones = strs.Select(ulong.Parse).ToList();

        int blinkCount = 75;

        ulong stonesCount = Blink(stones, 0, blinkCount);

        Console.WriteLine(stonesCount);
        Console.WriteLine(sw.Elapsed);
    }

    private static ulong Blink(List<ulong> stones, int currentBlink, int maxBlink)
    {
        if (currentBlink == maxBlink)
            return (ulong)stones.Count;

        stones = Blink(stones);

        const int subStonesMaxCount = 8*1024;

        ulong result = 0;

        if (stones.Count > subStonesMaxCount)
        {
            var valuesAndResults = new Dictionary<ulong, ulong>();

            for (var i = 0; i < stones.Count; i++)
            {
                var stone = stones[i];
                if (valuesAndResults.TryGetValue(stone, out var rs))
                {
                    result += rs;
                    continue;
                }

                var stoneResult = Blink(new List<ulong>() { stone }, currentBlink + 1, maxBlink);
                valuesAndResults.Add(stone, stoneResult);
                result += stoneResult;
            }
        }
        else
        {
            result += Blink(stones, currentBlink + 1, maxBlink);
        }
        
        return result;
    }

    private static List<ulong> Blink(List<ulong> stones)
    {
        var result = new List<ulong>(stones.Count*2);
        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                result.Add(1ul);
                continue;
            }

            var digitsCount = 0;
            var temp = stone;
            while (temp > 0)
            {
                digitsCount++;
                temp /= 10;
            }

            if (digitsCount%2 == 0)
            {
                ulong d = 1;
                for (int j = 0; j < digitsCount/2; j++)
                {
                    d *= 10;
                }

                ulong firstStone = stone / d;
                ulong secondStone = stone % d;
                result.Add(firstStone);
                result.Add(secondStone);
                goto NextStone;
            }

            result.Add(stone * 2024);

            NextStone: ;
        }

        return result;
    }
}