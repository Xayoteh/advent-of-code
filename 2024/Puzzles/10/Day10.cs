using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day10
{
    const string Filename = "Puzzles/10/input.txt";

    public static void Run()
    {
        List<List<int>> map = ReadTopographicMap();

        int part1 = map.GetTrailheads().Sum(map.GetTrailheadScore);
        int part2 = map.GetTrailheads().Sum(map.GetTrailheadRating);

        Console.WriteLine("** Day 10 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<List<int>> ReadTopographicMap()
    {
        List<List<int>> topographicMap = [];

        foreach (string line in File.ReadLines(Filename))
        {
            topographicMap.Add([.. line.Select(x => x - '0')]);
        }

        return topographicMap;
    }

    static int GetTrailheadScore(this List<List<int>> map, Point trailhead) =>
        map.WalkFrom(trailhead).Count(x => map.At(x.position) is 9);

    static int GetTrailheadRating(this List<List<int>> map, Point trailhead) =>
        map.WalkFrom(trailhead).Where(x => map.At(x.position) is 9).Sum(x => x.count);

    static IEnumerable<(Point position, int count)> WalkFrom(this List<List<int>> map, Point trailhead)
    {
        Dictionary<Point, int> pathsCount = new() { [trailhead] = 1 };
        Queue<Point> queue = [];

        queue.Enqueue(trailhead);

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();

            yield return (current, pathsCount[current]);

            foreach (Point neighbor in map.GetUphillNeighbors(current))
            {
                if (pathsCount.TryAdd(neighbor, 0)) queue.Enqueue(neighbor);

                pathsCount[neighbor] += pathsCount[current];
            }
        }
    }

    static IEnumerable<Point> GetTrailheads(this List<List<int>> map) =>
        from y in Enumerable.Range(0, map.Count)
        from x in Enumerable.Range(0, map[0].Count)
        where map[y][x] is 0
        select new Point(x, y);

    static IEnumerable<Point> GetUphillNeighbors(this List<List<int>> map, Point position) =>
        Directions.WithoutDiagonals
        .Select(direction => position + direction)
        .Where(newPosition => map.IsUpHill(position, newPosition));

    static bool IsUpHill(this List<List<int>> map, Point position, Point neighbor) =>
        map.IsInBounds(position) && map.IsInBounds(neighbor) && map.At(neighbor) == map.At(position) + 1;
}
