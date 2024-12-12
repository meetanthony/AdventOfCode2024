using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AdventOfCode.Day12.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day12.txt";
        var lines = File.ReadAllLines(fileName);

        Map map = new Map(lines);
        
        var areas = GetAllAreas(map);

        var totalCost = 0;

        foreach (var area in areas)
        {
            var perimeter = GetPerimeter(area);
            var areaSize = area.Count;
            totalCost += areaSize * perimeter;
        }

        Console.WriteLine(totalCost);
    }

    private static List<List<Point>> GetAllAreas(Map map)
    {
        var result = new List<List<Point>>();
        var processedPoints = new HashSet<Point>();

        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                var point = new Point(x,y);
                if (processedPoints.Contains(point))
                    continue;
                var areaPoints = new List<Point>();
                GetArea(areaPoints, map, x, y);
                foreach (var areaPoint in areaPoints)
                {
                    processedPoints.Add(areaPoint);
                }
                result.Add(areaPoints);
            }
        }

        return result;
    }

    private enum Sides
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    private static Sides[] GetSameNeighbors(Map map, int x, int y)
    {
        var width = map.Width;
        var height = map.Height;
        var type = map[x, y];
        var sides = new List<Sides>();
        if (x > 0 && map[x - 1, y] == type) sides.Add(Sides.Left);
        if (x < width-1 && map[x + 1, y] == type) sides.Add(Sides.Right);

        if (y > 0 && map[x, y - 1] == type) sides.Add(Sides.Top);
        if (y < height-1 && map[x, y + 1] == type) sides.Add(Sides.Bottom);

        return sides.ToArray();
    }

    private class Map
    {
        public Map(string[] lines)
        {
            var width = lines[0].Length;
            var height = lines.Length;
            char[,] map = new char[width, height];

            for (int y = 0; y < lines.Length; y++)
            {
                var line = lines[y];
                for (int x = 0; x < line.Length; x++)
                {
                    map[x, y] = line[x];
                }
            }
            _map = map;
        }

        private readonly char[,] _map;

        public char this[int x, int y]
        {
            get => _map[x, y];
        }

        public int Width => _map.GetLength(0);
        public int Height => _map.GetLength(1);
    }

    private static void GetArea(List<Point> points, Map map, int x, int y)
    {
        var width = map.Width;
        var height = map.Height;

        if (x < 0 || x >= width || y < 0 || y >= height)
            return;

        var point = new Point(x, y);
        if (points.Contains(point))
            return;

        points.Add(point);
        var neighbors = GetSameNeighbors(map, x, y);
        foreach (var neighbor in neighbors)
        {
            switch (neighbor)
            {
                case Sides.Top:
                    GetArea(points, map, x, y - 1);
                    break;
                case Sides.Right:
                    GetArea(points, map, x + 1, y);
                    break;
                case Sides.Bottom:
                    GetArea(points, map, x, y + 1);
                    break;
                case Sides.Left:
                    GetArea(points, map, x - 1, y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private static int GetPerimeter(List<Point> areaPoints)
    {
        var perimeterCounter = 0;

        foreach (var point in areaPoints)
        {
            if (areaPoints.Contains(new Point(point.X, point.Y - 1)) == false)
                perimeterCounter++;
            if (areaPoints.Contains(new Point(point.X + 1, point.Y)) == false)
                perimeterCounter++;
            if (areaPoints.Contains(new Point(point.X, point.Y + 1)) == false)
                perimeterCounter++;
            if (areaPoints.Contains(new Point(point.X - 1, point.Y)) == false)
                perimeterCounter++;
        }

        return perimeterCounter;
    }
}