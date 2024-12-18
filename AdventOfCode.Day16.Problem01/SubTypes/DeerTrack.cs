using CommonStructsAndAlgos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day16.Problem01.SubTypes;

internal class DeerTrack : Map<TrackUnits>
{
    public DeerTrack(string[] lines)
    {
        List<TrackUnits[]> storage = new List<TrackUnits[]>();
        int width = 0;
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            if (string.IsNullOrWhiteSpace(line))
                break;
            width = Math.Max(width, line.Length);
            var storageLine = new TrackUnits[width];
            for (var x = 0; x < line.Length; x++)
            {
                var symbol = line[x];
                TrackUnits unit = TrackUnits.Empty;
                switch (symbol)
                {
                    case '.':
                        unit = TrackUnits.Empty;
                        break;

                    case '#':
                        unit = TrackUnits.Wall;
                        break;

                    case 'S':
                        DeerStartLocation = new Point(x, y);
                        break;

                    case 'E':
                        FinishLocation = new Point(x, y);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
                }

                storageLine[x] = unit;
            }
            storage.Add(storageLine);
        }

        MapData = new TrackUnits[width, storage.Count];
        for (var y = 0; y < storage.Count; y++)
        {
            var units = storage[y];
            for (int x = 0; x < units.Length; x++)
            {
                MapData[x, y] = units[x];
            }
        }
    }

    private DeerTrack(DeerTrack deerTrack)
    {
        FinishLocation = deerTrack.FinishLocation;
        DeerStartLocation = deerTrack.DeerStartLocation;
        MapData = new TrackUnits[deerTrack.Width, deerTrack.Height];
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                MapData[x, y] = deerTrack[x, y];
            }
        }
    }

    public Point DeerStartLocation { get; }

    public Point FinishLocation { get; private set; }

    public int VisitedPoints
    {
        get
        {
            var result = 0;
            foreach (var trackUnit in MapData)
            {
                if (trackUnit == TrackUnits.DeerWasHere)
                    result++;
            }

            return result;
        }
    }

    public DeerTrack GetCopy()
    {
        var map = new DeerTrack(this);
        return map;
    }

    public override string ToString()
    {
        return ToString(new Point(), null);
    }

    public string ToString(Point currentDeerLocation, Moves[]? movesHistory = null)
    {
        var sb = new StringBuilder();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var currentPoint = new Point(x, y);
                char symbol;
                var c = this[currentPoint];
                switch (c)
                {
                    case TrackUnits.Empty:
                        symbol = '.';
                        break;

                    case TrackUnits.Wall:
                        symbol = '#';
                        break;

                    case TrackUnits.DeerWasHere:
                        symbol = '*';
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (movesHistory != null)
                {
                    var move = movesHistory.FirstOrDefault(move => move.Location == currentPoint);

                    if (move != null)
                    {
                        switch (move.Direction)
                        {
                            case Directions.Up:
                                symbol = '^';
                                break;

                            case Directions.Right:
                                symbol = '>';
                                break;

                            case Directions.Down:
                                symbol = 'v';
                                break;

                            case Directions.Left:
                                symbol = '<';
                                break;

                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                }
                else
                {
                    if (DeerStartLocation == currentPoint)
                        symbol = 'S';
                }

                if (FinishLocation == currentPoint)
                    symbol = 'E';

                if (currentPoint == currentDeerLocation)
                    symbol = '@';

                sb.Append(symbol);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}