using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Day09.Problem02;

internal class Program
{
    private enum SectorTypes
    {
        File,
        EmptySpace
    }

    private static void Main()
    {
        const string fileName = "day09.txt";
        var lines = File.ReadAllLines(fileName);

        var line = lines[0];
        byte[] packedBytes = new byte[line.Length];
        for (int i = 0; i < line.Length; i++)
        {
            packedBytes[i] = byte.Parse(line[i].ToString());
        }

        var unpackedBytes = Unpack(packedBytes);
        Print(unpackedBytes);
        var repackedValues = Repack(unpackedBytes);
        Print(repackedValues);
        Console.WriteLine(GetChecksum(repackedValues));
    }

    private static long GetChecksum(DiskSector[] sectors)
    {
        long checksum = 0;
        int multiplier = 0;

        foreach (var sector in sectors)
        {
            if (sector.Type == SectorTypes.EmptySpace)
            {
                multiplier += sector.Length;
                continue;
            }

            for (int i = 0; i < sector.Length; i++)
            {
                checksum += sector.Id * multiplier;
                multiplier++;
            }
        }

        return checksum;
    }

    private static void Print(DiskSector[] sectors)
    {
        var sb = new StringBuilder();

        foreach (var sector in sectors)
        {
            var filler = sector.Type == SectorTypes.EmptySpace ? "." : sector.Id.ToString();
            for (int i = 0; i < sector.Length; i++)
            {
                sb.Append(filler);
            }
        }

        Console.WriteLine(sb.ToString());
    }

    private static DiskSector[] Repack(DiskSector[] unpackedValues)
    {
        List<DiskSector> sectors = new List<DiskSector>(unpackedValues);

        for (var begin = 0; begin < sectors.Count; begin++)
        {
            var beginSector = sectors[begin];
            if (beginSector.Type == SectorTypes.EmptySpace)
            {
                for (int end = sectors.Count - 1; end > begin; end--)
                {
                    var endSector = sectors[end];
                    if (endSector.Type == SectorTypes.EmptySpace)
                        continue;

                    if (endSector.Length <= beginSector.Length)
                    {
                        sectors.Remove(endSector);
                        sectors.Remove(beginSector);
                        sectors.Insert(begin, endSector);
                        sectors.Insert(end, new DiskSector(endSector.Length));

                        if (endSector.Length < beginSector.Length)
                        {
                            sectors.Insert(begin + 1, new DiskSector(beginSector.Length - endSector.Length));
                        }
                        break;
                    }
                }
            }
        }

        return sectors.ToArray();
    }

    private static DiskSector[] Unpack(byte[] packedBytes)
    {
        List<DiskSector> unpackedBytes = new();

        var currentId = 0;

        var values = true;
        foreach (var packedByte in packedBytes)
        {
            if (values)
                unpackedBytes.Add(new DiskSector(currentId, packedByte));
            else
                unpackedBytes.Add(new DiskSector(packedByte));

            if (values)
                currentId++;

            values = !values;
        }

        return unpackedBytes.ToArray();
    }

    private class DiskSector
    {
        public int Id;

        public int Length;

        public SectorTypes Type;

        public DiskSector(int length)
        {
            Type = SectorTypes.EmptySpace;
            Length = length;
        }

        public DiskSector(int id, int length)
        {
            Type = SectorTypes.File;
            Id = id;
            Length = length;
        }
    }
}