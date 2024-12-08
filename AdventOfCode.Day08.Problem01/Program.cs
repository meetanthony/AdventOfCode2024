using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day08.Problem01;

internal class Program
{
    private static HashSet<Point> Marks = new HashSet<Point>();

    private static char[] GetAllAntennaTypes(char[,] map)
    {
        var antennaTypes = new HashSet<char>();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                var c = map[x, y];
                if (c != '#' && c != ' ' && c != '.')
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

    private static void MarkAntennaZone(Point a, Point b, char[,] map)
    {
        var x = a.X + (b.X - a.X) * 2;
        var y = a.Y + (b.Y - a.Y) * 2;

        if (x < 0 || x >= map.GetLength(0)
            || y < 0 || y >= map.GetLength(1))
            return;

        Marks.Add(new Point(x, y));

        if (map[x, y] == ' ')
            map[x, y] = '#';
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