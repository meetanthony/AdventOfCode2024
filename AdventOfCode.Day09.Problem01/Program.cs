using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AdventOfCode.Day09.Problem01;

internal class Program
{
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
        //Print(unpackedBytes);
        var repackedValues = Repack(unpackedBytes);
        //Print(repackedValues);
        Console.WriteLine(GetChecksum(repackedValues));
    }

    private static long GetChecksum(int?[] values)
    {
        long checksum = 0;
        for (int i = 0; i < values.Length; i++)
        {
            var value = values[i];
            if (value.HasValue)
                checksum += value.Value * i;
        }
        return checksum;
    }

    private static int?[] Repack(int?[] unpackedValues)
    {
        List<int?> repackedValues = new();
        for (var i = 0; i < unpackedValues.Length; i++)
        {
            var unpackedValue = unpackedValues[i];
            if (unpackedValue.HasValue)
                repackedValues.Add(unpackedValue);
            else
            {
                for (int j = unpackedValues.Length - 1; j >= i; j--)
                {
                    var endValue = unpackedValues[j];
                    if (endValue.HasValue)
                    {
                        repackedValues.Add(endValue);
                        unpackedValues[j] = null;
                        break;
                    }
                }
            }
        }

        return repackedValues.ToArray();
    }

    private static void Print(int?[] values)
    {
        var sb = new StringBuilder();
        for (int i = 0; i < values.Length; i++)
        {
            var value = values[i];
            sb.Append(value.HasValue ? value.Value : ".");
        }

        Console.WriteLine(sb.ToString());
    }

    private static int?[] Unpack(byte[] packedBytes)
    {
        List<int?> unpackedBytes = new();

        var currentId = 0;

        var values = true;
        foreach (var packedByte in packedBytes)
        {
            for (int i = 0; i < packedByte; i++)
            {
                if (values)
                    unpackedBytes.Add(currentId);
                else
                    unpackedBytes.Add(null);
            }

            if (values)
                currentId++;

            values = !values;
        }

        return unpackedBytes.ToArray();
    }
}