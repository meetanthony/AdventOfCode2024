using AdventOfCode.Day16.Problem02.SubTypes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day16.Problem02;

internal class Program
{
    private static void Main()
    {
        const string fileName = "TestData\\day16.txt";
        var lines = File.ReadAllLines(fileName);

        var deerTrack = new DeerTrack(lines);

        var deer = new Deer(deerTrack.DeerStartLocation);

        Console.WriteLine(deerTrack.ToString());

        var sw = Stopwatch.StartNew();

        var result = GetFastestDeersAndTheirTracks(deerTrack, deer);

        var fastestDeersTracks = result.fastestDeersTracks;

        Console.WriteLine(deerTrack.ToString(new Point(0, 0), fastestDeersTracks.ToArray()));

        fastestDeersTracks.Add(deerTrack.FinishLocation);

        Console.WriteLine(fastestDeersTracks.Count);

        Console.WriteLine(sw.Elapsed.ToString());
    }

    private static int _minPoints = int.MaxValue;

    private static (List<Deer> fastestDeers, HashSet<Point> fastestDeersTracks)
        GetFastestDeersAndTheirTracks(DeerTrack map, Deer deer)
    {
        var fastestDeers = new List<Deer>();
        var fastestDeersTracks = new HashSet<Point>();

        var scores = new Scores(map.Width, map.Height);

        GetSuccessfullDeers(map, deer, fastestDeers, fastestDeersTracks, scores);

        return (fastestDeers, fastestDeersTracks);
    }

    private static void GetSuccessfullDeers(
        DeerTrack map, Deer deer,
        List<Deer> fastestDeers, HashSet<Point> fastestDeersTracks,
        Scores scores)
    {
        try
        {
            if (scores[deer.Location] < deer.Points)
            {
                return;
            }

            map[deer.Location] = TrackUnits.DeerWasHere;

            if (deer.Location == map.FinishLocation)
            {
                if (deer.Points < _minPoints)
                {
                    _minPoints = deer.Points;
                    fastestDeers.Clear();
                    fastestDeersTracks.Clear();
                }

                for (int y = 0; y < map.Height; y++)
                {
                    for (int x = 0; x < map.Width; x++)
                    {
                        if (map[x, y] == TrackUnits.DeerWasHere)
                            fastestDeersTracks.Add(new Point(x, y));
                    }
                }

                fastestDeers.Add(deer);
                return;
            }

            if (deer.Points > _minPoints)
            {
                return;
            }

            var possibleWays = deer.GetDeerPossibleWays();
            foreach (var possibleWay in possibleWays)
            {
                var deerCopy = deer.GetCopy();

                var oldLocation = deer.Location;
                var stepSuccess = deerCopy.DoNextStep(map, possibleWay);
                if (stepSuccess == false)
                {
                    continue;
                }

                scores[oldLocation] = deerCopy.Points - 1;

                GetSuccessfullDeers(map, deerCopy, fastestDeers, fastestDeersTracks, scores);
            }
        }
        finally
        {
            map[deer.Location] = TrackUnits.Empty;
        }
    }
}