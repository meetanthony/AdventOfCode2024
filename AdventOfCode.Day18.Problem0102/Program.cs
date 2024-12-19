using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using AdventOfCode.Day18.Problem0102.SubTypes;
using CommonStructsAndAlgos;

namespace AdventOfCode.Day18.Problem0102;

internal class Program
{
    private static void Main()
    {
        var example = false;

        Map<bool> map;
        string fileName;
        int fallenBytesCount;


        if (example)
        {
            map = new Map<bool>(7, 7);
            fileName = "TestData\\example.txt";
            fallenBytesCount = 12;
        }
        else
        {
            map = new Map<bool>(71, 71);
            fileName = "TestData\\day18.txt";
            fallenBytesCount = 1024;
        }

        var lines = File.ReadAllLines(fileName);

        var fallingBytes = new List<Point>(lines.Length);
        foreach (var line in lines)
        {
            var coordComponents = line.Split(',');
            var x = int.Parse(coordComponents[0]);
            var y = int.Parse(coordComponents[1]);
            fallingBytes.Add(new Point(x, y));
        }
        
        var sw = Stopwatch.StartNew();

        for (var bytesFallen = 0; bytesFallen < fallenBytesCount; bytesFallen++)
        {
            var fallingByte = fallingBytes[bytesFallen];
            map[fallingByte] = true;
        }

        // Problem 01
        {
            var shortestPaths = PathFinder.GetShortestPathes(map, new[] { true }, Point.Empty,
                new Point(map.Width - 1, map.Height - 1));

            Console.WriteLine(shortestPaths[0].Count - 1);
        }

        // Problem 02
        var oldShortestPath = new List<Point>();
        for (int bytesFallen = fallenBytesCount; bytesFallen < fallingBytes.Count; bytesFallen++)
        {
            var fallingByte = fallingBytes[bytesFallen];
            map[fallingByte] = true;
            if (oldShortestPath.Count > 0 && oldShortestPath.Contains(fallingByte) == false)
                continue;

            var shortestPaths = PathFinder.GetShortestPathes(map, new[] { true }, Point.Empty,
                new Point(map.Width - 1, map.Height - 1));
            
            if (shortestPaths.Count == 0)
            {
                Console.WriteLine(fallingByte);
                break;
            }
            oldShortestPath = shortestPaths[0];
        }

        Console.WriteLine(sw.Elapsed.ToString());
    }

    private static void PrintMap(Map<bool> map)
    {
        var startPoint = Console.GetCursorPosition();
        foreach (var mapElement in map)
        {
            Console.SetCursorPosition(mapElement.Coords.X, mapElement.Coords.Y);
            Console.Write(mapElement.Value ? '#' : '.');
        }

        Console.SetCursorPosition(startPoint.Left, startPoint.Top);
    }

    private static void PrintPath(List<Point> path, Map<bool> map)
    {
        for (var y = 0; y < map.Height; y++)
        {
            for (var x = 0; x < map.Width; x++)
            {
                Console.SetCursorPosition(x, y);
                Console.Write(path.Contains(new Point(x, y)) ? "O" : (map[new Point(x, y)] ? "#" : "."));
            }
        }
    }
}