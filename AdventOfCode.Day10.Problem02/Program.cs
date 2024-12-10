using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day10.Problem02;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day10.txt";
        var lines = File.ReadAllLines(fileName);

        var width = lines[0].Length;
        var height = lines.Length;
        int[,] map = new int[width, height];

        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                map[x, y] = int.Parse(line[x].ToString());
            }
        }

        int[] allRouteScores = FindAllRouteScores(map);

        var result = allRouteScores.Sum();

        Console.WriteLine(result);
    }

    private static int[] FindAllRouteScores(int[,] map)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        List<int> routeScores = new List<int>();

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                bool[,] marks = new bool[width, height];
                var routeScore = FindRouteScore(map, marks, x, y);
                if (routeScore > 0)
                {
                    //PrintMap(map, marks);
                    routeScores.Add(routeScore);
                }
            }
        }


        return routeScores.ToArray();
    }

    private static void PrintMap(int[,] map, bool[,] marks)
    {
        var width = map.GetLength(0);
        var height = map.GetLength(1);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (marks[x, y])
                    Console.Write(map[x, y]);
                else
                    Console.Write('.');
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static int FindRouteScore(int[,] map, bool[,] marks, int startX, int startY, int previousValue = -1)
    {
        var width = map.GetLength(0);
        var height = map.GetLength(1);

        if (startX < 0 || startX >= width)
            return 0;
        if (startY < 0 || startY >= height)
            return 0;

        /*if (marks[startX, startY])
            return 0;*/

        var currentValue = previousValue + 1;
        if (map[startX, startY] == 9 && currentValue == 9)
            return 1;

        if (map[startX, startY] != currentValue)
            return 0;

        if (currentValue == 0)
            marks[startX, startY] = true;

        var result = 0;
        var score = FindRouteScore(map, marks, startX - 1, startY, currentValue);
        if (score > 0)
        {
            marks[startX - 1, startY] = true;
            result += score;
        }

        score = FindRouteScore(map, marks, startX + 1, startY, currentValue);
        if (score > 0)
        {
            marks[startX + 1, startY] = true;
            result += score;
        }

        score = FindRouteScore(map, marks, startX, startY - 1, currentValue);
        if (score > 0)
        {
            marks[startX, startY - 1] = true;
            result += score;
        }

        score = FindRouteScore(map, marks, startX, startY + 1, currentValue);
        if (score > 0)
        {
            marks[startX, startY + 1] = true;
            result += score;
        }
        return result;
    }
}