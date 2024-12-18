using CommonStructsAndAlgos;

namespace AdventOfCode.Day16.Problem01.SubTypes;

internal class Scores : Map<int>
{
    public Scores(int width, int height)
    {
        MapData = new int[width, height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                MapData[x, y] = int.MaxValue;
            }
        }

    }
}