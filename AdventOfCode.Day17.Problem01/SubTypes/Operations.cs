using System;

namespace AdventOfCode.Day17.Problem01.SubTypes;

internal class Operations
{
    public Operations(string line)
    {
        var valuesStartMarker = ": ";
        line = line.Remove(0, line.IndexOf(valuesStartMarker) + valuesStartMarker.Length);

        var values = line.Split(',', StringSplitOptions.RemoveEmptyEntries);
        _operations = new int[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            _operations[i] = int.Parse(values[i]);
        }
    }

    private readonly int[] _operations;

    public int this[int index] => _operations[index];

    public int Length => _operations.Length;
}