using System;
using System.Drawing;
using System.IO;

namespace AdventOfCode.Day06.Problem01;

internal class Program
{
    private enum CellStates
    {
        Empty,
        Obstruction,
        Visited
    }

    private enum Directions
    {
        Up, Down, Left, Right
    }

    private static void Main()
    {
        const string fileName = "day06.txt";
        var lines = File.ReadAllLines(fileName);

        var fieldHeight = lines.Length;
        var fieldWidth = lines[0].Length;

        var field = new CellStates[fieldWidth, fieldHeight];
        Point? guardPosition = new Point();
        var direction = Directions.Up;
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] == '#')
                    field[x, y] = CellStates.Obstruction;
                if (line[x] == '^')
                    guardPosition = new Point(x, y);
            }
        }

        do
        {
            field[guardPosition.Value.X, guardPosition.Value.Y] = CellStates.Visited;
            guardPosition = NextPosition(guardPosition.Value, ref direction, field);
        } while (guardPosition.HasValue);

        var result = 0;
        foreach (var cell in field)
        {
            if (cell == CellStates.Visited)
                result++;
        }

        Console.WriteLine(result);
    }

    private static Point? NextPosition(Point currentPosition, ref Directions direction, CellStates[,] field)
    {
        var x = currentPosition.X;
        var y = currentPosition.Y;
        switch (direction)
        {
            case Directions.Up:
                y--;
                break;

            case Directions.Down:
                y++;
                break;

            case Directions.Left:
                x--;
                break;

            case Directions.Right:
                x++;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        if (x < 0 || y < 0 || x == field.GetLength(0) || y == field.GetLength(1))
            return null;

        if (field[x, y] == CellStates.Obstruction)
        {
            switch (direction)
            {
                case Directions.Up:
                    direction = Directions.Right;
                    y++;
                    break;

                case Directions.Down:
                    direction = Directions.Left;
                    y--;
                    break;

                case Directions.Left:
                    direction = Directions.Up;
                    x++;
                    break;

                case Directions.Right:
                    direction = Directions.Down;
                    x--;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }

        return new Point(x, y);
    }
}