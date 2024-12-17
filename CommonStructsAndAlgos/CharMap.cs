namespace CommonStructsAndAlgos;

public class CharsMap : Map<char>
{
    public CharsMap(string[] lines)
    {
        var width = lines[0].Length;
        var height = lines.Length;
        char[,] map = new char[width, height];

        for (int y = 0; y < lines.Length; y++)
        {
            var line = lines[y];
            if (line.Length == 0)
                break;
            for (int x = 0; x < line.Length; x++)
            {
                map[x, y] = line[x];
            }
        }
        MapData = map;
    }
}