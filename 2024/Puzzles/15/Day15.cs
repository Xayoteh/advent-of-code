using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day15
{
    const string Filename = "Puzzles/15/input.txt";

    public static void Run()
    {
        (List<List<char>> map, List<char> moves) = ReadInput();

        int part1 = map.Simulate(moves).SumBoxGPSCoordinates();
        int part2 = map.ScaleUp().Simulate(moves).SumBoxGPSCoordinates();

        Console.WriteLine("** Day 15 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static (List<List<char>> map, List<char> moves) ReadInput()
    {
        using var file = File.OpenText(Filename);

        List<List<char>> map = [];
        List<char> moves = [];

        while (file.ReadLine() is string input && !string.IsNullOrEmpty(input))
        {
            map.Add([.. input]);
        }

        while (file.ReadLine() is string input)
        {
            moves.AddRange(input);
        }

        return (map, moves);
    }

    static Point GetRobotPosition(this List<List<char>> map)
    {
        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] == '@') return new(x, y);
            }
        }

        return new(-1, -1);
    }

    static List<List<char>> Simulate(this List<List<char>> map, List<char> moves)
    {
        List<List<char>> mapCopy = map.Copy();
        Point robot = mapCopy.GetRobotPosition();

        foreach (char move in moves)
        {
            Point direction = Directions.Parse(move);

            if (mapCopy.CanMove(robot, direction, [])) robot = mapCopy.Move(robot, direction, []);
        }

        return mapCopy;
    }

    static bool CanMove(this List<List<char>> map, Point position, Point direction, HashSet<Point> seen)
    {
        if (!seen.Add(position)) return true;

        Point nextPosition = position + direction;
        char nextValue = map.At(nextPosition);

        if(nextValue is 'O' or '[' or ']')
        {
            bool canMove = map.CanMove(nextPosition, direction, seen);

            if(nextValue is'[') canMove &= map.CanMove(nextPosition + Directions.Right, direction, seen);
            else if(nextValue is ']') canMove &= map.CanMove(nextPosition + Directions.Left, direction, seen);

            return canMove;
        }

        return nextValue is not '#';
    }

    static Point Move(this List<List<char>> map, Point position, Point direction, HashSet<Point> seen)
    {
        if (!seen.Add(position)) return position;

        Point nextPosition = position + direction;
        char nextValue = map.At(nextPosition);

        if (nextValue is '[' or ']' or 'O')
        {
            map.Move(nextPosition, direction, seen);

            if (nextValue is '[') map.Move(nextPosition + Directions.Right, direction, seen);
            else if (nextValue is ']') map.Move(nextPosition + Directions.Left, direction, seen);
        }

        map.ReplaceAt(nextPosition, map.At(position));
        map.ReplaceAt(position, '.');

        return nextPosition;
    }

    static List<List<char>> ScaleUp(this List<List<char>> map)
    {
        List<List<char>> mapCopy = map.Copy();
        List<List<char>> newMap = [];

        foreach (List<char> row in mapCopy)
        {
            List<char> newRow = [];

            foreach (char value in row)
            {
                if (value is '@') newRow.AddRange("@.");
                else if (value is 'O') newRow.AddRange("[]");
                else newRow.AddRange(new string(value, 2));
            }

            newMap.Add(newRow);
        }

        return newMap;
    }

    static int SumBoxGPSCoordinates(this List<List<char>> map)
    {
        var sum = 0;

        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                if (map[y][x] is 'O' or '[')
                    sum += (x, y).GPSCoordinate();
            }
        }

        return sum;
    }

    static int GPSCoordinate(this (int x, int y) point) =>
        100 * point.y + point.x;

    static public List<List<char>> Copy(this List<List<char>> lists) =>
        lists.Select(list => list.ToList()).ToList();
}
