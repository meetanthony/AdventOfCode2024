using System;
using System.Drawing;

namespace AdventOfCode.Day16.Problem02.SubTypes;

internal class Deer
{
    public Deer(Point location)
    {
        Location = location;
        Direction = Directions.Right;
    }

    private Deer(Point location, Directions direction, int points)
    {
        Location = location;
        Direction = direction;
        Points = points;
    }

    public Directions Direction { get; private set; }

    public Point Location { get; private set; }

    public int Points { get; private set; }

    public Deer GetCopy()
    {
        return new Deer(Location, Direction, Points);
    }

    public Directions[] GetDeerPossibleWays()
    {
        switch (Direction)
        {
            case Directions.Up:
                return new[] { Directions.Up, Directions.Right, Directions.Left };

            case Directions.Right:
                return new[] { Directions.Up, Directions.Right, Directions.Down };

            case Directions.Down:
                return new[] { Directions.Down, Directions.Right, Directions.Left };

            case Directions.Left:
                return new[] { Directions.Up, Directions.Down, Directions.Left };

            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public static Point GetNextLocation(Point currentLocation, Directions direction)
    {
        Point location;
        switch (direction)
        {
            case Directions.Up:
                location = new Point(currentLocation.X, currentLocation.Y - 1);
                break;

            case Directions.Right:
                location = new Point(currentLocation.X + 1, currentLocation.Y);
                break;

            case Directions.Down:
                location = new Point(currentLocation.X, currentLocation.Y + 1);
                break;

            case Directions.Left:
                location = new Point(currentLocation.X - 1, currentLocation.Y);
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }

        return location;
    }

    public bool DoNextStep(DeerTrack deerTrack, Directions targetDirection)
    {
        if (targetDirection == Direction)
            return GoForward(deerTrack);
        var oldDirection = Direction;
        switch (targetDirection)
        {
            case Directions.Up:
                if (Direction == Directions.Left) TurnRight();
                if (Direction == Directions.Right) TurnLeft();
                break;

            case Directions.Right:
                if (Direction == Directions.Up) TurnRight();
                if (Direction == Directions.Down) TurnLeft();
                break;

            case Directions.Down:
                if (Direction == Directions.Right) TurnRight();
                if (Direction == Directions.Left) TurnLeft();
                break;

            case Directions.Left:
                if (Direction == Directions.Down) TurnRight();
                if (Direction == Directions.Up) TurnLeft();
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(targetDirection), targetDirection, null);
        }
        if (oldDirection == Direction)
            return false;

        return GoForward(deerTrack);
    }

    private bool GoForward(DeerTrack deerTrack)
    {
        var nextLocation = GetNextLocation(Location, Direction);
        if (deerTrack[nextLocation] == TrackUnits.Wall || deerTrack[nextLocation] == TrackUnits.DeerWasHere)
            return false;

        Points += 1;

        Location = nextLocation;

        return true;
    }

    private void TurnLeft()
    {
        switch (Direction)
        {
            case Directions.Up:
                Direction = Directions.Left;
                break;

            case Directions.Right:
                Direction = Directions.Up;
                break;

            case Directions.Down:
                Direction = Directions.Right;
                break;

            case Directions.Left:
                Direction = Directions.Down;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
        Points += 1000;
    }

    private void TurnRight()
    {
        switch (Direction)
        {
            case Directions.Up:
                Direction = Directions.Right;
                break;

            case Directions.Right:
                Direction = Directions.Down;
                break;

            case Directions.Down:
                Direction = Directions.Left;
                break;

            case Directions.Left:
                Direction = Directions.Up;
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
        Points += 1000;
    }
}