using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonStructsAndAlgos;

namespace AdventOfCode.Day18.Problem0102.SubTypes;

public static class PathFinder
{
    public static bool Print { get; set; } = false;

    public static List<List<Point>> GetShortestPathes<T>(Map<T> map, T[] wallsTypes, Point currentCoords, Point endCoords)
    {
        var shortestPaths = new List<List<Point>>();
        var currentPath = new List<Point>();
        var coordScores = new Map<int>(map.Width, map.Height);
        foreach (var coordScore in coordScores)
        {
            coordScores[coordScore.Coords] = int.MaxValue;
        }
        var visitMap = new Map<bool>(map.Width, map.Height);

        foreach (var mapElement in map)
        {
            var x = mapElement.Coords.X;
            var y = mapElement.Coords.Y;
            if (Print) Console.SetCursorPosition(x, y);

            if (wallsTypes.Contains(mapElement.Value))
            {
                visitMap[mapElement.Coords] = true;
                if (Print) Console.Write("#");
            }
            else if (Print) Console.Write(".");
        }

        GetShortestPathes(currentCoords, endCoords, currentPath, coordScores, visitMap, shortestPaths);

        return shortestPaths;
    }

    private static void GetShortestPathes(Point currentCoords, Point endCoords,
            List<Point> currentPath, Map<int> coordScores, Map<bool> visitMap, List<List<Point>> shortestPaths)
    {
        var x = currentCoords.X;
        var y = currentCoords.Y;
        if (Print)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("0");
        }

        currentPath.Add(currentCoords);
        var currentPathLength = currentPath.Count + (endCoords.X - currentCoords.X) + (endCoords.Y - currentCoords.Y);

        if (coordScores[currentCoords] <= currentPathLength)
            goto Exit;

        coordScores[currentCoords] = currentPathLength;

        var shortestPathLength = shortestPaths.Count == 0 ? int.MaxValue : shortestPaths[0].Count;

        if (currentCoords == endCoords)
        {
            if (currentPathLength < shortestPathLength)
            {
                shortestPaths.Clear();
            }

            List<Point> shortestPath = new List<Point>(currentPath);

            shortestPaths.Add(shortestPath);

            goto Exit;
        }

        if (currentPathLength > shortestPathLength)
            goto Exit;

        visitMap[currentCoords] = true;
        var nextLocations = GetPossibleNextLocations(visitMap, currentCoords);
        foreach (var nextLocation in nextLocations)
        {
            GetShortestPathes(nextLocation, endCoords, currentPath, coordScores, visitMap, shortestPaths);
        }

        visitMap[currentCoords] = false;

    Exit:;

        if (Print)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(".");
        }

        currentPath.RemoveAt(currentPath.Count - 1);
    }

    private static Point[] GetPossibleNextLocations(Map<bool> visitMap, Point currentCoords)
    {
        var possibleNextLocations = new List<Point>();

        var possibleNextLocation = new Point(currentCoords.X + 1, currentCoords.Y);

        if (possibleNextLocation.X < visitMap.Width && visitMap[possibleNextLocation] == false)
            possibleNextLocations.Add(possibleNextLocation);

        possibleNextLocation = new Point(currentCoords.X, currentCoords.Y + 1);
        if (possibleNextLocation.Y < visitMap.Height && visitMap[possibleNextLocation] == false)
            possibleNextLocations.Add(possibleNextLocation);

        possibleNextLocation = new Point(currentCoords.X - 1, currentCoords.Y);
        if (possibleNextLocation.X >= 0 && visitMap[possibleNextLocation] == false)
            possibleNextLocations.Add(possibleNextLocation);

        possibleNextLocation = new Point(currentCoords.X, currentCoords.Y - 1);
        if (possibleNextLocation.Y >= 0 && visitMap[possibleNextLocation] == false)
            possibleNextLocations.Add(possibleNextLocation);

        return possibleNextLocations.ToArray();
    }
}