using System.Drawing;

namespace AdventOfCode.Day16.Problem02.SubTypes;

internal class Move
{
    public Move(Point location, Directions direction)
    {
        Location = location;
        Direction = direction;
    }

    public Point Location { get; private set; }
    public Directions Direction { get; private set; }
}