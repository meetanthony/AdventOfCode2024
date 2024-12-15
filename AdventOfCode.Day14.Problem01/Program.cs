using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day14.Problem01;

internal class Program
{
    private static void Main()
    {
        const bool example = false;
        const int secsCount = 100;

        string fileName;
        int mapWidth, mapHeight;
        if (example)
        {
            fileName = "example.txt";
            mapWidth = 11;
            mapHeight = 7;
        }
        else
        {
            fileName = "day14.txt";
            mapWidth = 101;
            mapHeight = 103;
        }

        var lines = File.ReadAllLines(fileName);

        var robots = GetRobots(lines);

        var quadrants = new int[4];

        foreach (var robot in robots)
        {
            var position = robot.GetCoordsAfterSecs(secsCount, mapWidth, mapHeight);
            int quadrantNumber = GetQuadrantNumber(position, mapWidth, mapHeight);
            if (quadrantNumber == -1)
                continue;

            quadrants[quadrantNumber] += 1;
        }

        var result = 1;
        foreach (var q in quadrants)
            result *= q;

        Console.WriteLine(result);
    }

    private static int GetQuadrantNumber(Point position, int mapWidth, int mapHeight)
    {
        var widthHalf = mapWidth / 2;
        var heightHalf = mapHeight / 2;
        var x = position.X;
        var y = position.Y;
        
        if (x == widthHalf || y == heightHalf)
            return -1;

        var result = x/(widthHalf + 1);
        result += y / (heightHalf + 1) * 2;

        return result;
    }

    private static IEnumerable<Robot> GetRobots(string[] lines)
    {
        var robots = new List<Robot>();
        foreach (var line in lines)
        {
            robots.Add(new Robot(line));
        }

        return robots;
    }

    private class Robot
    {
        public Robot(string line)
        {
            string pattern = @"(-?\d+),(-?\d+)";

            Regex regex = new Regex(pattern);
            var matches = regex.Matches(line);

            var match = matches[0];

            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            StartPos = new Point(x, y);

            match = matches[1];
            x = int.Parse(match.Groups[1].Value);
            y = int.Parse(match.Groups[2].Value);
            Speed = new Point(x, y);
        }

        public Point StartPos { get; }
        public Point Speed { get; }

        public Point GetCoordsAfterSecs(int secs, int mapWidth, int mapHeight)
        {
            var x = StartPos.X + Speed.X * secs;
            var y = StartPos.Y + Speed.Y * secs;

            x %= mapWidth;
            if (x < 0)
                x += mapWidth;
            y %= mapHeight;
            if (y < 0)
                y += mapHeight;
            return new Point(x, y);
        }
    }
}