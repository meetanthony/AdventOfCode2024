using System;
using System.Collections.Generic;
using System.Drawing;
using CommonStructsAndAlgos;

namespace AdventOfCode.Day20.Problem01.SubTypes;

public static class PathFinder
{
    public static bool Print { get; set; } = false;
    
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

    public static void MarkSteps(Point currentCoords, StepsMap stepsMap, Map<bool> visitMap, int currentPathLength)
    {
        var x = currentCoords.X;
        var y = currentCoords.Y;
        if (Print)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("0");
        }

        if (stepsMap[currentCoords] > currentPathLength)
            return;

        stepsMap[currentCoords] = currentPathLength;

        
        visitMap[currentCoords] = true;
        var nextLocations = GetPossibleNextLocations(visitMap, currentCoords);
        foreach (var nextLocation in nextLocations)
        {
            MarkSteps(nextLocation, stepsMap, visitMap, currentPathLength + 1);
        }

        visitMap[currentCoords] = false;
    }
}