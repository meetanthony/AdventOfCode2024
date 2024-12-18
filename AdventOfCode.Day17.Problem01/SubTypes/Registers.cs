namespace AdventOfCode.Day17.Problem01.SubTypes;

internal class Registers
{
    public Registers(string[] lines)
    {
        var valuesStartMarker = ": ";
        var line = lines[0];
        A = int.Parse(line.Remove(0, line.IndexOf(valuesStartMarker) + valuesStartMarker.Length));
        line = lines[1];
        B = int.Parse(line.Remove(0, line.IndexOf(valuesStartMarker) + valuesStartMarker.Length));
        line = lines[2];
        C = int.Parse(line.Remove(0, line.IndexOf(valuesStartMarker) + valuesStartMarker.Length));
    }

    public int A { get; set; }
    public int B { get; set; }
    public int C { get; set; }
}