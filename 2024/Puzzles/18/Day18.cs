using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day18
{
    const string Filename = "Puzzles/18/input.txt";
    const int MemorySize = 71;
    const int BytesToSimulate = 1024;

    public static void Run()
    {
        List<Point> bytes = ReadBytes();

        List<List<char>> memory = GetMemoryAfterSimulation(bytes);
        HashSet<Point> path = memory.GetPathToEnd();

        // -1 to exclude the start position
        int part1 = path.Count - 1;
        Point part2 = memory.GetByteToMakeUnreachable(bytes, path);

        Console.WriteLine("** Day 18 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<Point> ReadBytes()
    {
        List<Point> bytes = [];

        foreach(string line in File.ReadLines(Filename))
        {
            int[] values = [..line.Split(',').Select(int.Parse)];

            bytes.Add(new(values[0], values[1]));
        }

        return bytes;
    }

    static List<List<char>> GetMemoryAfterSimulation(List<Point> bytes)
    {
        List<List<char>> memory = [];

        for(var r = 0; r < MemorySize; r++)
        {
            memory.Add([..new string('.', MemorySize)]);
        }

        for(var i = 0; i < BytesToSimulate; i++)
        {
            memory.ReplaceAt(bytes[i], '#');
        }

        return memory;
    }

    static HashSet<Point> GetPathToEnd(this List<List<char>> memory)
    {
        Queue<(Point current, Point previous)> q = [];
        Dictionary<Point, Point> previousInPath = [];

        Point start = new(0, 0);
        Point end = new(MemorySize - 1, MemorySize - 1);
        
        q.Enqueue((start, new(-1, -1)));

        while(q.Count > 0)
        {
            (Point current, Point previous) = q.Dequeue();

            if(!previousInPath.TryAdd(current, previous)) continue;
            if(current == end) break;

            foreach (Point direction in Directions.WithoutDiagonals)
            {
                Point next = current + direction;

                if(memory.IsValidPosition(next)) q.Enqueue((next, current));
            }
        }

        HashSet<Point> path = [];
        Point pointInPath = end;

        while(previousInPath.ContainsKey(pointInPath))
        {
            path.Add(pointInPath);
            pointInPath = previousInPath[pointInPath];
        }

        return path;
    }

    static Point GetByteToMakeUnreachable(this List<List<char>> memory, List<Point> bytes, HashSet<Point> path)
    {
        for(var i = BytesToSimulate; i < bytes.Count; i++)
        {
            memory.ReplaceAt(bytes[i], '#');

            if(!path.Contains(bytes[i])) continue;
            if((path = memory.GetPathToEnd()).Count is 0) return bytes[i];
        }
        
        return new(-1, -1);
    }

    static bool IsValidPosition(this List<List<char>> memory, Point point) => 
        memory.EqualsAt(point, '.');

}
