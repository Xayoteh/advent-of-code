/*
Solution to Advent of Code 2025 - Day 7
https://adventofcode.com/2025/day/7
*/

const string INPUT_PATH = "../input.txt";

var (start, splittersRows) = ReadInput(INPUT_PATH);

int splitsCount = CountSplits(start, splittersRows);
long timelinesCount = CountTimelines(start, splittersRows);

Console.WriteLine("** Day 7 **");
Console.WriteLine($"Part 1: {splitsCount}");
Console.WriteLine($"Part 2: {timelinesCount}");

long CountTimelinesFromPoint(Point point, List<List<Point>> splittersRows, int rowIndex, Dictionary<(Point, int), long> memo)
{
    for (int r = rowIndex; r < splittersRows.Count; r++)
    {
        if (IsHit(point, splittersRows[r]))
        {
            var left = new Point(point.X + 1, point.Y + 1);
            var right = new Point(point.X - 1, point.Y + 1);

            return GetTimelinesFromPoint(left, splittersRows, r + 1, memo)
                + GetTimelinesFromPoint(right, splittersRows, r + 1, memo);
        }

        point = new Point(point.X, point.Y + 1);
    }

    return 1;
}

long GetTimelinesFromPoint(Point point, List<List<Point>> splittersRows, int rowIndex, Dictionary<(Point, int), long> memo)
    => memo.ContainsKey((point, rowIndex)) ? memo[(point, rowIndex)]
        : memo[(point, rowIndex)] = CountTimelinesFromPoint(point, splittersRows, rowIndex, memo);

long CountTimelines(Point start, List<List<Point>> splittersRows)
{
    Dictionary<(Point, int), long> memo = [];
    return GetTimelinesFromPoint(start, splittersRows, 0, memo);
}

bool IsHit(Point beam, List<Point> splitters)
    => splitters.Any(s => s == beam);

int CountSplits(Point start, List<List<Point>> splittersRows)
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

    return splitCount;
}


(Point, List<List<Point>>) ReadInput(string path)
{
    var lines = File.ReadAllLines(path);
    var start = new Point(lines[0].IndexOf('S'), 0);

    List<List<Point>> splitters = [[]];

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