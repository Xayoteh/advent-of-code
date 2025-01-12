using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day06
{
    const string Filename = "Puzzles/06/input.txt";

    public static void Run()
    {
        List<List<char>> map = ReadMap();
        Dictionary<Point, Point> path = map.GetPath(map.GetStartPosition(), Directions.Up);

        int part1 = path.Count;
        int part2 = map.CountPossibleLoops(path);

        Console.WriteLine("** Day 6 **");
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

    static Dictionary<Point, Point> GetPath(this List<List<char>> map, Point position, Point direction)
    {
        Dictionary<Point, Point> path = [];

        while (map.IsInBounds(position))
        {
            if (!path.TryAdd(position, direction) && path[position] == direction) return [];

            Point nextPosition = position + direction;

            if (map.IsWall(nextPosition))
            {
                direction = Directions.Next(direction);
                continue;
            }

            position = nextPosition;
        }

        return path;
    }

    private static Point GetStartPosition(this List<List<char>> map)
    {
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] is '^') return new(x, y);
            }
        }

        return new(-1, -1);
    }

    static int CountPossibleLoops(this List<List<char>> map, Dictionary<Point, Point> path)
    {
        Point startPosition = map.GetStartPosition();
        var count = 0;

        foreach ((Point position, Point direction) in path)
        {
            if (position == startPosition) continue;
            if (map.IsLoop(position, direction)) count++;
        }

        return count;
    }

    static bool IsLoop(this List<List<char>> map, Point obstacle, Point direction)
    {
        map.ReplaceAt(obstacle, '#');

        bool isLoop = map.GetPath(obstacle - direction, direction).Count is 0;

        map.ReplaceAt(obstacle, '.');

        return isLoop;
    }

    static bool IsWall(this List<List<char>> map, Point position) =>
        map.EqualsAt(position, '#');
}
