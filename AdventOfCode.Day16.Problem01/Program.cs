using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using AdventOfCode.Day16.Problem01.SubTypes;

namespace AdventOfCode.Day16.Problem01;

internal class Program
{
    private static void Main()
    {
        const string fileName = "TestData\\day16.txt";
        var lines = File.ReadAllLines(fileName);

        var map = new DeerTrack(lines);

        var deer = new Deer(map.DeerStartLocation);

        Console.WriteLine(map.ToString());
        var sw = Stopwatch.StartNew();
        List<Deer> successfullDeers = new();
        var scores = new Scores(map.Width, map.Height);
        GetSuccessfullDeers(map, deer, successfullDeers, scores);

        if (successfullDeers.Count == 0)
            throw new Exception();

        Deer fastestDeer = new Deer(new Point());

        int minPoints = int.MaxValue;
        foreach (var successfullDeer in successfullDeers)
        {
            if (successfullDeer.Points < minPoints)
            {
                minPoints = successfullDeer.Points;
                fastestDeer = successfullDeer;
            }
        }

        Console.WriteLine(map.ToString(fastestDeer.Location, fastestDeer.GetMovesHistory()));

        Console.WriteLine(fastestDeer?.Points);

        Console.WriteLine(sw.Elapsed.ToString());
    }

    private static int _minPoints = int.MaxValue;

    private static void GetSuccessfullDeers(DeerTrack deerTrack, Deer deer, List<Deer> successfullDeers, Scores scores)
    {
        if (scores[deer.Location] < deer.Points)
        {
            return;
        }

        scores[deer.Location] = deer.Points;

        if (deer.Location == deerTrack.FinishLocation)
        {
            if (deer.Points < _minPoints)
                _minPoints = deer.Points;
            successfullDeers.Add(deer);
            deerTrack[deer.Location] = TrackUnits.Empty;
            return;
        }

        if (deer.Points >= _minPoints)
        {
            deerTrack[deer.Location] = TrackUnits.Empty;
            return;
        }

        var possibleWays = deer.GetDeerPossibleWays();
        foreach (var possibleWay in possibleWays)
        {
            var deerCopy = deer.GetCopy();
            var stepSuccess = deerCopy.DoNextStep(deerTrack, possibleWay);
            if (stepSuccess == false)
            {
                continue;
            }

            GetSuccessfullDeers(deerTrack, deerCopy, successfullDeers, scores);
        }
        deerTrack[deer.Location] = TrackUnits.Empty;
    }
}