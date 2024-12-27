using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using AdventOfCode.Day20.Problem01.SubTypes;
using CommonStructsAndAlgos;

namespace AdventOfCode.Day20.Problem01;

internal class Program
{
    private static void Main()
    {
        var example = false;
        
        var fileName = example ? "TestData\\example.txt" : "TestData\\day20.txt";

        var lines = File.ReadAllLines(fileName);

        RacetrackMap racetrackMap = new RacetrackMap(lines);

        Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);

        var visitedMap = racetrackMap.GetVisitedMap();
        var sw = Stopwatch.StartNew();
        var stepsFromEnd = new StepsMap(racetrackMap.Width, racetrackMap.Height);
        var stepsFromStart = new StepsMap(racetrackMap.Width, racetrackMap.Height);

        Console.Clear();
        Console.Write(racetrackMap.ToString());

        PathFinder.MarkSteps(racetrackMap.End, stepsFromEnd, visitedMap, 0);
        PathFinder.MarkSteps(racetrackMap.Start, stepsFromStart, visitedMap, 0);

        stepsFromEnd.Print();
        stepsFromStart.Print();

        var directPathLength = stepsFromEnd[racetrackMap.Start] - 2;

        var cheatsStats = GetCheatsStats(racetrackMap, stepsFromStart, stepsFromEnd, directPathLength);

        var cheats = from stat in cheatsStats
                     orderby stat.Key
                     select stat;

        var atLeastHundredCheatsCount = 0;
        foreach (var cheat in cheats)
        {
            var cheatsCount = cheat.Value;
            var profit = cheat.Key;

            Console.WriteLine($"Cheats count: {cheatsCount}, Cheat profit: {profit}");
            if (profit >= 100)
                atLeastHundredCheatsCount += cheatsCount;
        }

        Console.WriteLine($"At least 100 cheats count: {atLeastHundredCheatsCount}");

        Console.WriteLine(sw.Elapsed.ToString());
    }

    private static Dictionary<int, int> GetCheatsStats(RacetrackMap racetrackMap, StepsMap stepsFromStart, StepsMap stepsFromEnd, int directPathLength)
    {
        var width = racetrackMap.Width;
        var height = racetrackMap.Height;
        var result = new Dictionary<int, int>();
        for (int i = 0; i < 2; i++)
        {
            var xDiff = 0;
            var yDiff = 0;
            if (i == 0)
                xDiff = 1;
            else
                yDiff = 1;
            for (int y = 0; y < height ; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var middle = racetrackMap[x, y];
                    if (middle != StorageUnits.Wall)
                        continue;

                    var xPlus = x + xDiff;
                    var yPlus = y + yDiff;
                    var xMinus = x - xDiff;
                    var yMinus = y - yDiff;
                    if (xPlus >= width || xMinus < 0
                        || yPlus >= height || yMinus < 0)
                        continue;

                    var point0 = new Point(xPlus, yPlus);
                    var point1 = new Point(xMinus, yMinus);

                    var top = racetrackMap[point0];
                    var bottom = racetrackMap[point1];

                    if (top == StorageUnits.Wall || bottom == StorageUnits.Wall)
                        continue;

                    var pathLength = GetPathLength(stepsFromStart, stepsFromEnd, point0, point1);

                    if (pathLength >= directPathLength)
                        continue;

                    var cheatProfit = directPathLength - pathLength;
                    int cheatsCount = 0;
                    result.TryGetValue(cheatProfit, out cheatsCount);
                    cheatsCount++;
                    result[cheatProfit] = cheatsCount;
                }
            }
        }

        return result;
    }

    private static int GetPathLength(StepsMap stepsFromStart, StepsMap stepsFromEnd, Point point0, Point point1)
    {
        var stepsCountFromStart = stepsFromStart[point0];
        var stepsCountFromEnd = stepsFromEnd[point1];
        var pathLength = stepsCountFromStart + stepsCountFromEnd;

        stepsCountFromStart = stepsFromStart[point1];
        stepsCountFromEnd = stepsFromEnd[point0];
        var pathLength2 = stepsCountFromStart + stepsCountFromEnd;

        return Math.Min(pathLength, pathLength2);
    }
}