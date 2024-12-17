using CommonStructsAndAlgos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AdventOfCode.Day15.Problem01;

internal class StorageMap : Map<StorageUnits>
{
    public Point RobotLocation { get; set; }

    public StorageMap(string[] lines)
    {
        List<StorageUnits[]> storage = new List<StorageUnits[]>();
        int width = 0;
        for (var y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            if (string.IsNullOrWhiteSpace(line))
                break;
            width = Math.Max(width, line.Length);
            var storageLine = new StorageUnits[width];
            for (var x = 0; x < line.Length; x++)
            {
                var symbol = line[x];
                StorageUnits unit = StorageUnits.Empty;
                switch (symbol)
                {
                    case '.':
                        unit = StorageUnits.Empty;
                        break;

                    case '#':
                        unit = StorageUnits.Wall;
                        break;

                    case 'O':
                        unit = StorageUnits.Stuff;
                        break;

                    case '@':
                        RobotLocation = new Point(x, y);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(symbol), symbol, null);
                }

                storageLine[x] = unit;
            }
            storage.Add(storageLine);
        }

        MapData = new StorageUnits[width, storage.Count];
        for (var y = 0; y < storage.Count; y++)
        {
            var units = storage[y];
            for (int x = 0; x < units.Length; x++)
            {
                MapData[x, y] = units[x];
            }
        }
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                char symbol;
                var c = MapData[x, y];
                switch (c)
                {
                    case StorageUnits.Empty:
                        symbol = '.';
                        break;

                    case StorageUnits.Stuff:
                        symbol = 'O';
                        break;

                    case StorageUnits.Wall:
                        symbol = '#';
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (x == RobotLocation.X && y == RobotLocation.Y)
                    symbol = '@';

                sb.Append(symbol);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}