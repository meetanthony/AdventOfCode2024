using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using CommonStructsAndAlgos;

namespace AdventOfCode.Day20.Problem01.SubTypes;

public class StepsMap(int width, int height) : Map<int>(width, height)
{
    static void SetColor(int r, int g, int b)
    {
        // ANSI escape sequence для установки цвета текста
        Console.Write($"\u001b[38;2;{r};{g};{b}m");
    }

    static void ResetColor()
    {
        // ANSI escape sequence для сброса цвета
        Console.Write("\u001b[0m");
    }

    public void Print()
    {
        var oldPos = Console.GetCursorPosition();
        var max = MapData.Cast<int>().Prepend(0).Max();
        var min = 0;
        var lightPerValue = byte.MaxValue / (float)(max - min);
        
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                var c = MapData[x, y];

                if (c == 0)
                    continue;

                int rgb = (int)(c * lightPerValue);

                Console.SetCursorPosition(x, y);
                SetColor(rgb, rgb, rgb);

                Console.Write(c%10);
            }
        }

        Console.SetCursorPosition(oldPos.Left, oldPos.Top);
        ResetColor();
    }
}

internal class RacetrackMap : Map<StorageUnits>
{
    public Point Start { get; }

    public Point End { get; }

    public RacetrackMap(string[] lines)
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

                    case 'S':
                        unit = StorageUnits.Start;
                        Start = new Point(x, y);
                        break;

                    case 'E':
                        unit = StorageUnits.End;
                        End = new Point(x, y);
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

    public Map<bool> GetVisitedMap()
    {
        Map<bool> result = new Map<bool>(Width, Height);
        for (var y = 0; y < Height; y++)
        {
            for (var x = 0; x < Width; x++)
            {
                result[x, y] = MapData[x, y] == StorageUnits.Wall;
            }
        }
        return result;
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

                    case StorageUnits.Start:
                        symbol = 'S';
                        break;

                    case StorageUnits.Wall:
                        symbol = '#';
                        break;

                    case StorageUnits.End:
                        symbol = 'E';
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                sb.Append(symbol);
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }
}