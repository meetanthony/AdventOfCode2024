using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day15.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day15.txt";
        var lines = File.ReadAllLines(fileName);

        var map = new StorageMap(lines);

        Console.WriteLine(map);

        var moveLines = from line in lines
                        where line.IndexOfAny(new[] { '<', '>', '>', 'v' }) != -1
                        select line;

        var moves = GetMoves(moveLines);

        foreach (var move in moves)
        {
            MoveRobotOneStep(map, move);
            //Console.WriteLine(map);
        }

        Console.WriteLine(map);

        var coords = GetStuffCoordsSum(map);

        Console.WriteLine(coords);
    }

    private static bool MoveStuff(StorageMap map, int x, int y, Point coordsDiff)
    {
        var oldX = x;
        var oldY = y;

        x += coordsDiff.X;
        y += coordsDiff.Y;

        switch (map[x, y])
        {
            case StorageUnits.Empty:
                map[oldX, oldY] = StorageUnits.Empty;
                map[x, y] = StorageUnits.Stuff;
                return true;

            case StorageUnits.Stuff:
                var stuffMoved = MoveStuff(map, x, y, coordsDiff);
                if (stuffMoved)
                {
                    map[oldX, oldY] = StorageUnits.Empty;
                    map[x, y] = StorageUnits.Stuff;
                }
                return stuffMoved;

            case StorageUnits.Wall:
                return false;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void MoveRobotOneStep(StorageMap map, Directions direction)
    {
        Point coordsDiff;
        switch (direction)
        {
            case Directions.Up:
                coordsDiff = new Point(0, -1);
                break;

            case Directions.Right:
                coordsDiff = new Point(1, 0);
                break;

            case Directions.Down:
                coordsDiff = new Point(0, 1);
                break;

            case Directions.Left:
                coordsDiff = new Point(-1, 0);
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        var currentCoords = map.RobotLocation;
        var x = currentCoords.X;
        var y = currentCoords.Y;

        x += coordsDiff.X;
        y += coordsDiff.Y;

        switch (map[x, y])
        {
            case StorageUnits.Empty:
                map.RobotLocation = new Point(x, y);
                break;

            case StorageUnits.Stuff:
                if (MoveStuff(map, x, y, coordsDiff))
                    map.RobotLocation = new Point(x, y);
                break;

            case StorageUnits.Wall:
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static int GetStuffCoordsSum(StorageMap map)
    {
        var result = 0;
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                if (map[x, y] == StorageUnits.Stuff)
                    result += GetCoords(x, y);
            }
        }

        return result;
    }

    private static int GetCoords(int x, int y)
    {
        return y * 100 + x;
    }

    private static List<Directions> GetMoves(IEnumerable<string> moveLines)
    {
        var result = new List<Directions>();
        foreach (var line in moveLines)
        {
            Console.WriteLine(line);
            for (int x = 0; x < line.Length; x++)
            {
                var symbol = line[x];
                Directions direction;
                switch (symbol)
                {
                    case '^':
                        direction = Directions.Up;
                        break;

                    case '>':
                        direction = Directions.Right;
                        break;

                    case 'v':
                        direction = Directions.Down;
                        break;

                    case '<':
                        direction = Directions.Left;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
                }
                result.Add(direction);
            }
        }

        return result;
    }
}