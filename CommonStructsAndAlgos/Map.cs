using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace CommonStructsAndAlgos;

public class Map<T>(int width = 0, int height = 0) : IEnumerable<Map<T>.MapElement>
{
    protected T[,] MapData = new T[width, height];

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

    public class MapElement
    {
        internal MapElement(Point coords, T value)
        {
            Coords = coords;
            Value = value;
        }

        public Point Coords { get; }
        public T Value { get; }
    }

    private class MapElementEnumerator(Map<T> map) : IEnumerator<MapElement>
    {
        private Map<T> _map = map;

        private int _currentX = -1;
        private int _currentY = 0;

        public bool MoveNext()
        {
            _currentX++;
            if (_currentX == _map.Width)
            {
                _currentX = 0;
                _currentY++;
            }

            if (_currentY == _map.Height)
                return false;

            return true;
        }

        public void Reset()
        {
            _currentX = -1;
            _currentY = 0;
        }

        public MapElement Current => new MapElement(new Point(_currentX, _currentY), map[_currentX, _currentY]);

        object IEnumerator.Current => Current;

        public void Dispose()
        {
            // ignore
        }
    }

    

    public IEnumerator<MapElement> GetEnumerator()
    {
        return new MapElementEnumerator(this);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}