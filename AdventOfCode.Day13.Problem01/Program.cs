using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day13.Problem01;

internal class Program
{
    private const int PressAPrice = 3;

    private const int PressBPrice = 1;

    private static void Main()
    {
        const string fileName = "day13.txt";
        var lines = File.ReadAllLines(fileName);

        var machines = GetMachines(lines);

        var totalPrice = 0;
        foreach (var machine in machines)
        {
            var price = GetMinPrice(machine);
            totalPrice += price;
        }

        Console.WriteLine(totalPrice);
    }

    private static Combintation[] GetAllAxisCombinations(int valueA, int valueB, int target)
    {
        var aMaxPresses = target / valueA;
        var bMaxPresses = target / valueB;

        var result = new HashSet<Combintation>();

        for (int i = 0; i <= aMaxPresses; i++)
        {
            var a = valueA * i;
            if ((target - a) % valueB != 0)
                continue;

            var b = (target - a) / valueB;

            result.Add(new Combintation(i, b));
        }

        for (int i = 0; i <= bMaxPresses; i++)
        {
            var b = valueB * i;
            if ((target - b) % valueB != 0)
                continue;

            var a = (target - b) / valueB;

            result.Add(new Combintation(a, i));
        }

        return result.ToArray();
    }

    private static List<SlotMachine> GetMachines(string[] lines)
    {
        var machines = new List<SlotMachine>();

        for (int i = 0; i + 3 <= lines.Length; i += 4)
        {
            var line0 = lines[i];
            var line1 = lines[i + 1];
            var line2 = lines[i + 2];

            var machine = new SlotMachine(new[] { line0, line1, line2 });
            machines.Add(machine);
        }

        return machines;
    }

    private static int GetMinPrice(SlotMachine slotMachine)
    {
        var targetX = slotMachine.PrizeLocation.X;
        var targetY = slotMachine.PrizeLocation.Y;

        var allCombinationsX = GetAllAxisCombinations(slotMachine.A.X, slotMachine.B.X, targetX);
        var allCombinationsY = GetAllAxisCombinations(slotMachine.A.Y, slotMachine.B.Y, targetY);

        var minPrice = int.MaxValue;

        foreach (var x in allCombinationsX)
        {
            if (allCombinationsY.Contains(x) == false)
                continue;

            var price = x.A * PressAPrice + x.B * PressBPrice;
            if (price < minPrice)
                minPrice = price;
        }

        if (minPrice == int.MaxValue)
            minPrice = 0;

        return minPrice;
    }

    private struct Combintation
    {
        public int A;

        public int B;

        public Combintation(int a, int b)
        {
            A = a;
            B = b;
        }
    }

    private class Button
    {
        public Button(string line)
        {
            string pattern = @"X\+(\d+),\s*Y\+(\d+)";

            Regex regex = new Regex(pattern);

            var match = regex.Match(line);
            X = int.Parse(match.Groups[1].Value);
            Y = int.Parse(match.Groups[2].Value);
        }

        public int X { get; }
        public int Y { get; }
    }

    private class SlotMachine
    {
        public SlotMachine(string[] lines)
        {
            var lineA = lines[0];
            A = new Button(lineA);

            var lineB = lines[1];
            B = new Button(lineB);

            var linePrize = lines[2];

            string pattern = @"X\=(\d+),\s*Y\=(\d+)";

            Regex regex = new Regex(pattern);
            var match = regex.Match(linePrize);

            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            PrizeLocation = new Point(x, y);
        }

        public Button A { get; }
        public Button B { get; }

        public Point PrizeLocation { get; }
    }
}