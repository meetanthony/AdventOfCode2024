using System.Drawing;

namespace AdventOfCode.Day16.Problem01.SubTypes;

internal class Moves
{
    public Moves(Point location, Directions direction)
    {
        Location = location;
        Direction = direction;
    }

    public Point Location { get; private set; }
    public Directions Direction { get; private set; }
}