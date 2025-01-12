using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day20
{
    const string Filename = "Puzzles/20/input.txt";
    const int TimeToSave = 100;

    public static void Run()
    {
        List<List<char>> map = ReadMap();
        Dictionary<int, int> cheatCount = map.CountCheats([2, 20]);

        int part1 = cheatCount[2];
        int part2 = cheatCount[20];

        Console.WriteLine("** Day 20 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<List<char>> ReadMap()
    {
        List<List<char>> map = [];

        foreach (string line in File.ReadLines(Filename))
        {
            map.Add([.. line]);
        }

        return map;
    }

    static Dictionary<Point, int> GetTimesToEnd(this List<List<char>> map)
    {
        Dictionary<Point, int> timeToEnd = [];
        Queue<(Point, int)> queue = [];

        Point end = map.GetEndPosition();

        queue.Enqueue((end, 0));

        while (queue.Count > 0)
        {
            (Point point, int time) = queue.Dequeue();

            if (!timeToEnd.TryAdd(point, time)) continue;

            foreach (Point direction in Directions.WithoutDiagonals)
            {
                Point nextPoint = point + direction;

                if (map.IsValidPosition(nextPoint)) queue.Enqueue((nextPoint, time + 1));
            }
        }

        return timeToEnd;
    }

    static Dictionary<int, int> CountCheats(this List<List<char>> map, List<int> durations)
    {
        Dictionary<Point, int> timeToEnd = map.GetTimesToEnd();

        Dictionary<int, int> cheatCount = [];
        HashSet<Point> seen = [];

        int totalTime = timeToEnd[map.GetStartPosition()];
        int longestCheat = durations.Max();

        foreach (int duration in durations)
        {
            cheatCount[duration] = 0;
        }

        foreach ((Point pointA, int timeA) in timeToEnd)
        {
            seen.Add(pointA);

            foreach ((Point pointB, int timeB) in timeToEnd)
            {
                int cheatDuration = GetDistance(pointA, pointB);

                if (seen.Contains(pointB) || cheatDuration > longestCheat) continue;

                int timeSaved = CalculateTimeSaved(timeA, timeB, totalTime, cheatDuration);

                foreach (int duration in durations)
                {
                    if (cheatDuration > duration) continue;
                    if (timeSaved >= TimeToSave) cheatCount[duration]++;
                }
            }
        }

        return cheatCount;
    }

    static int CalculateTimeSaved(int timeA, int timeB, int totalTime, int cheatDuration)
    {
        (int minTime, int maxTime) = MinMax(timeA, timeB);
        int newTime = totalTime - maxTime + minTime + cheatDuration;

        return totalTime - newTime;
    }

    static Point GetStartPosition(this List<List<char>> map)
    {
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] is 'S') return new(x, y);
            }
        }

        return new(-1, -1);
    }

    static Point GetEndPosition(this List<List<char>> map)
    {
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] is 'E') return new(x, y);
            }
        }

        return new(-1, -1);
    }

    static (int min, int max) MinMax(int a, int b) =>
        a > b ? (b, a) : (a, b);

    static int GetDistance(Point a, Point b) =>
        Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        
    static bool IsValidPosition(this List<List<char>> map, Point position) =>
        !map.EqualsAt(position, '#');

}
