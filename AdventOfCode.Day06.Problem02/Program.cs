using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace AdventOfCode.Day06.Problem02;

internal class Program
{
    private enum CellStates
    {
        Empty,
        Obstruction,
        Trap,
        UpMovement,
        RightMovement,
        DownMovement,
        LeftMovement,
        Turn
    }

    private enum Directions
    {
        Up,
        Right,
        Down,
        Left,
        Round
    }

    private static Directions NextDirection(Directions direction)
    {
        return (Directions)(((int)direction + 1) % (int)Directions.Round);
    }

    private static void Main()
    {
        const string fileName = "day06.txt";
        var lines = File.ReadAllLines(fileName);

        var fieldHeight = lines.Length;
        var fieldWidth = lines[0].Length;

        var field = new CellStates[fieldWidth, fieldHeight];
        Point? guardPosition = new Point();
        var startDirection = Directions.Up;
        var direction = startDirection;
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

        if (guardPosition == null)
            return;
        Point startGuardPosition = guardPosition.Value;

        while (true)
        {
            var nextPosition = NextPosition(guardPosition.Value, ref direction, field);

            if (nextPosition == null)
                break;
            
            var next = nextPosition.Value;

            if (next == guardPosition)
                continue;

            var oldValue = field[next.X, next.Y];
            field[next.X, next.Y] = CellStates.Obstruction;
            var trapPosition = IsItGoodPlaceForTrap(startGuardPosition, startDirection, field);
            if (trapPosition && next != startGuardPosition)
            {
                field[next.X, next.Y] = CellStates.Trap;
                //PrintField(field, guardPosition.Value, direction);
                Traps.Add(next);
            }
            ClearField(field);

            guardPosition = next;
        }

        //PrintField(field, guardPosition.Value, direction);
        Console.WriteLine(Traps.Count);
    }

    private static readonly HashSet<Point> Traps = new HashSet<Point>();

    private struct PositionAndDirection
    {
        public PositionAndDirection(Point pos, Directions direction)
        {
            Pos = pos;
            Direction = direction;
        }
        public Point Pos;
        public Directions Direction;

    }

    private static bool IsItGoodPlaceForTrap(Point startGuardPosition, Directions startDirection, CellStates[,] field)
    {
        var direction = startDirection;

        Point? guardPosition = startGuardPosition;
        
        HashSet<PositionAndDirection> previousMoves = new HashSet<PositionAndDirection>();

        var pad = new PositionAndDirection(guardPosition.Value, direction);
        previousMoves.Add(pad);

        while (true)
        {
            var nextPosition = NextPosition(guardPosition.Value, ref direction, field);

            if (nextPosition == null)
                return false;
            
            guardPosition = nextPosition;

            pad = new PositionAndDirection(guardPosition.Value, direction);

            if (previousMoves.Add(pad) == false)
                return true;
        }
    }

    private static Point? NextPosition(Point currentPosition, ref Directions direction, CellStates[,] field)
    {
        var x = currentPosition.X;
        var y = currentPosition.Y;

        switch (direction)
        {
            case Directions.Up:
                if (field[x,y] != CellStates.Turn)
                    field[x, y] = CellStates.UpMovement;
                y--;
                break;

            case Directions.Down:
                if (field[x, y] != CellStates.Turn)
                    field[x,y] = CellStates.DownMovement;
                y++;
                break;

            case Directions.Left:
                if (field[x, y] != CellStates.Turn)
                    field[x, y] = CellStates.LeftMovement;
                x--;
                break;

            case Directions.Right:
                if (field[x, y] != CellStates.Turn)
                    field[x, y] = CellStates.RightMovement;
                x++;
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
        }

        if (x < 0 || y < 0 || x == field.GetLength(0) || y == field.GetLength(1))
            return null;

        if (field[x, y] == CellStates.Obstruction)
        {
            field[currentPosition.X, currentPosition.Y] = CellStates.Turn;
            direction = NextDirection(direction);
            return currentPosition;
        }

        return new Point(x, y);
    }

    private static void PrintField(CellStates[,] field, Point guardPosition, Directions direction)
    {
        for (int y = 0; y < field.GetLength(1); y++)
        {
            for (int x = 0; x < field.GetLength(0); x++)
            {
                if (x == guardPosition.X && y == guardPosition.Y)
                {
                    switch (direction)
                    {
                        case Directions.Up:
                            Print('^');
                            break;

                        case Directions.Right:
                            Print('>');
                            break;

                        case Directions.Down:
                            Print('V');
                            break;

                        case Directions.Left:
                            Print('<');
                            break;

                        case Directions.Round:
                            break;
                        
                        default:
                            throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
                    }
                    continue;
                }

                var cellState = field[x, y];
                switch (cellState)
                {
                    case CellStates.Empty:
                        Print('.');
                        break;

                    case CellStates.Obstruction:
                        Print('#');
                        break;

                    case CellStates.Trap:
                        Print('O');
                        break;

                    case CellStates.UpMovement:
                        Print('|');
                        field[x, y] = CellStates.Empty;
                        break;

                    case CellStates.RightMovement:
                        Print('-');
                        field[x, y] = CellStates.Empty;
                        break;

                    case CellStates.DownMovement:
                        Print('|');
                        field[x, y] = CellStates.Empty;
                        break;

                    case CellStates.LeftMovement:
                        Print('-');
                        field[x, y] = CellStates.Empty;
                        break;

                    case CellStates.Turn:
                        Print('+');
                        field[x, y] = CellStates.Empty;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    private static void ClearField(CellStates[,] field)
    {
        for (int y = 0; y < field.GetLength(1); y++)
        {
            for (int x = 0; x < field.GetLength(0); x++)
            {
                switch (field[x,y])
                {
                    case CellStates.UpMovement:
                    case CellStates.RightMovement:
                    case CellStates.DownMovement:
                    case CellStates.LeftMovement:
                    case CellStates.Turn:
                        field[x, y] = CellStates.Empty;
                        break;
                }
            }
        }
    }

    private static void Print(char c)
    {
        Console.Write(c);
    }
}