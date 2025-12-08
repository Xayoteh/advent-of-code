/*
Solution to Advent of Code 2025 - Day 7
https://adventofcode.com/2025/day/7
*/

const string INPUT_PATH = "../input.txt";

var (start, splittersRows) = ReadInput(INPUT_PATH);
var (beams, splits) = Simulate(start, splittersRows);

Console.WriteLine("** Day 7 **");
Console.WriteLine($"Part 1: {splits}");

bool IsHit(Point beam, List<Point> splitters)
    => splitters.Any(s => s == beam);

(Queue<Point>, int) Simulate(Point start, List<List<Point>> splittersRows)
{
    Queue<Point> beams = [];
    int splitCount = 0;

    beams.Enqueue(start);

    foreach (List<Point> row in splittersRows)
    {
        HashSet<Point> newBeams = [];
        int currentBeams = beams.Count;

        for (int i = 0; i < currentBeams; i++)
        {
            Point beam = beams.Dequeue();

            if (IsHit(beam, row))
            {
                newBeams.Add(new Point(beam.X - 1, beam.Y + 1));
                newBeams.Add(new Point(beam.X + 1, beam.Y + 1));
                splitCount++;
            }
            else
            {
                newBeams.Add(new Point(beam.X, beam.Y + 1));
            }
        }

        foreach (Point beam in newBeams)
            beams.Enqueue(beam);
    }

    return (beams, splitCount);
}


(Point, List<List<Point>>) ReadInput(string path)
{
    var lines = File.ReadAllLines(path);
    var start = new Point(lines[0].IndexOf('S'), 1);

    List<List<Point>> splitters = [];

    for (int i = 1; i < lines.Length; i++)
    {
        List<Point> row = [];
        string line = lines[i];
        int idx = 0;

        while ((idx = line.IndexOf('^', idx)) != -1)
        {
            row.Add(new Point(idx, i));
            idx++;
        }

        splitters.Add(row);
    }

    return (start, splitters);
}

record Point(int X, int Y);