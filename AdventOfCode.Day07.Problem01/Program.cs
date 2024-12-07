﻿using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day07.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "day07.txt";
        var lines = File.ReadAllLines(fileName);

        var records = new List<Record>();
        foreach (var line in lines)
        {
            records.Add(new Record(line));
        }

        long result = 0;
        foreach (var record in records)
        {
            if (CanBeResolved(record))
                result += record.Result;
        }

        Console.WriteLine(result);
    }

    private enum Operands
    {
        Sum,
        Multi
    }

    private static bool CanBeResolved(Record record)
    {
        var values = record.Values;
        var result = record.Result;
        int operandsCount = values.Length - 1;
        var maxOperandsValue = Math.Pow(2, operandsCount);
        var operands = 0;

        while (operands < maxOperandsValue)
        {
            long value = 0;
            var a = values[0];
            for (int i = 0; i < operandsCount; i++)
            {
                var b = values[i + 1];
                var operand = (Operands)((operands >> i) & 0x1);

                switch (operand)
                {
                    case Operands.Sum:
                        value = a + b;
                        break;
                    case Operands.Multi:
                        value = a * b;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                a = value;
            }

            if (value == result)
                return true;

            operands++;
        }

        return false;
    }

    

    private class Record
    {
        public Record(string line)
        {
            var strs = line.Split(':');

            Result = long.Parse(strs[0]);
            strs = strs[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Values = new long[strs.Length];
            for (var i = 0; i < strs.Length; i++)
            {
                var str = strs[i];
                Values[i] = long.Parse(str);
            }
        }

        public long Result { get; }

        public long[] Values { get; }

    }
}