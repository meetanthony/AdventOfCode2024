using System.Drawing;

namespace CommonStructsAndAlgos;

public abstract class Map<T>
{
    protected T[,] MapData = new T[0, 0];

    public T this[int x, int y]
    {
        get => MapData[x, y];
        set => MapData[x, y] = value;
    }
    public T this[Point point]
    {
        get => MapData[point.X, point.Y];
        set => MapData[point.X, point.Y] = value;
    }

    public int Width => MapData.GetLength(0);
    public int Height => MapData.GetLength(1);
}