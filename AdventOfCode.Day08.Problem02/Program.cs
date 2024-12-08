using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day08.Problem02;

internal class Program
{
    private static HashSet<Point> Marks = new HashSet<Point>();

    private static void Main()
    {
        const string fileName = "day08.txt";
        var lines = File.ReadAllLines(fileName);

        var map = new char[lines[0].Length, lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                map[x, y] = line[x];
            }
        }

        var antennaTypes = GetAllAntennaTypes(map);

        foreach (var antennaType in antennaTypes)
        {
            MarkAntennaZones(antennaType, map);
        }

        PrintMap(map);
        Console.WriteLine();

        Console.WriteLine(Marks.Count);
    }

    private static char[] GetAllAntennaTypes(char[,] map)
    {
        var antennaTypes = new HashSet<char>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var c = map[x, y];
                if (c != '#' && c != '.')
                    antennaTypes.Add(c);
            }
        }

        return antennaTypes.ToArray();
    }

    private static Point[] GetAntennas(char antennaType, char[,] map)
    {
        var antennas = new List<Point>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var c = map[x, y];
                if (c == antennaType)
                    antennas.Add(new Point(x, y));
            }
        }

        return antennas.ToArray();
    }

    private static void MarkAntennaZone(Point a, Point b, char[,] map)
    {
        var xIncrement = b.X - a.X;
        var yIncrement = b.Y - a.Y;
        var x = a.X + xIncrement;
        var y = a.Y + yIncrement;

        while (x >= 0 && x < map.GetLength(0)
         && y >= 0 && y < map.GetLength(1))
        {
            Marks.Add(new Point(x, y));

            if (map[x, y] == '.')
                map[x, y] = '#';

            x += xIncrement;
            y += yIncrement;
        }
    }

    private static void MarkAntennaZones(char antennaType, char[,] map)
    {
        var allAnntennas = GetAntennas(antennaType, map);

        for (int i = 0; i < allAnntennas.Length; i++)
        {
            for (int j = 0; j < allAnntennas.Length; j++)
            {
                if (i == j)
                    continue;
                var a = allAnntennas[i];
                var b = allAnntennas[j];
                MarkAntennaZone(a, b, map);
                MarkAntennaZone(b, a, map);
            }
        }
    }

    private static void PrintMap(char[,] map)
    {
        for (int y = 0; y < map.GetLength(1); y++)
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                Console.Write(map[x, y]);
            }
            Console.WriteLine();
        }
    }
}