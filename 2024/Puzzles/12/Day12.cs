using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day12
{
    const string Filename = "Puzzles/12/input.txt";

    public static void Run()
    {
        List<List<char>> gardenPlotsMap = ReadGardenPlotsMap();
        List<HashSet<Point>> regions = gardenPlotsMap.GetRegions();

        var part1 = regions.SumPrices();
        var part2 = regions.SumPricesWithDiscount();

        Console.WriteLine("** Day 12 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<List<char>> ReadGardenPlotsMap()
    {
        List<List<char>> gardenPlotsMap = [];

        foreach (string line in File.ReadLines(Filename))
        {
            gardenPlotsMap.Add([.. line]);
        }

        return gardenPlotsMap;
    }

    static List<HashSet<Point>> GetRegions(this List<List<char>> map)
    {
        List<HashSet<Point>> regions = [];
        HashSet<Point> seen = [];

        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                Point position = new(x, y);

                if (!seen.Contains(position))
                {
                    HashSet<Point> region = map.FindRegion(position);

                    regions.Add(region);
                    seen.UnionWith(region);
                }
            }
        }

        return regions;
    }

    static HashSet<Point> FindRegion(this List<List<char>> map, Point startPoint)
    {
        HashSet<Point> region = [];
        Queue<Point> queue = [];

        queue.Enqueue(startPoint);

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();

            if (!region.Add(current)) continue;

            foreach (Point direction in Directions.WithoutDiagonals)
            {
                Point next = current + direction;

                if (map.IsSameType(current, next)) queue.Enqueue(next);
            }
        }

        return region;
    }

    static long SumPricesWithDiscount(this List<HashSet<Point>> regions) =>
        regions.Sum(region => region.GetArea() * region.CountSides());

    static long SumPrices(this List<HashSet<Point>> regions) =>
        regions.Sum(region => region.GetArea() * region.GetPerimeter());

    static int GetArea(this HashSet<Point> region) =>
        region.Count;

    static long GetPerimeter(this HashSet<Point> region)
    {
        long totalPerimeter = 0;

        foreach (Point position in region)
        {
            var pointPerimeter = 4;

            foreach (Point direction in Directions.WithoutDiagonals)
            {
                Point neighbor = position + direction;

                if (region.Contains(neighbor)) pointPerimeter--;
            }

            totalPerimeter += pointPerimeter;
        }

        return totalPerimeter;
    }

    static long CountSides(this HashSet<Point> region)
    {
        HashSet<(Point, Point)> explored = [];
        HashSet<Point> seen = [];

        Queue<Point> queue = [];
        long sides = 0;

        queue.Enqueue(region.First());

        while (queue.Count > 0)
        {
            Point current = queue.Dequeue();

            if (!seen.Add(current)) continue;

            foreach (Point direction in Directions.WithoutDiagonals)
            {
                Point neighbor = current + direction;

                if (region.Contains(neighbor)) queue.Enqueue(neighbor);
                // If the neighbor is not part of the region,
                // then current is an edge. Check if it's part
                // of a new side
                else if (region.IsNewSide(current, direction, explored)) sides++;
            }
        }

        return sides;
    }

    static bool IsNewSide(this HashSet<Point> region, Point point, Point checkDirection, HashSet<(Point, Point)> explored)
    {
        // if it's already explored, then it's not part of a new side
        if (explored.Contains((point, checkDirection))) return false;

        // if not, then its a new side
        // explore in both perpendicular directions
        // i.e, if direction is up OR down
        // then explore left AND right
        region.ExploreNeighbors(point, checkDirection, Directions.Next(checkDirection), explored);
        region.ExploreNeighbors(point, checkDirection, Directions.Previous(checkDirection), explored);

        return true;
    }

    static void ExploreNeighbors(this HashSet<Point> region, Point point, Point checkDirection, Point moveDirection, HashSet<(Point, Point)> explored)
    {
        // if the region contains the current point and
        // it doesn't contain the point which faces to,
        // then it's part of the same side
        while (region.Contains(point) && !region.Contains(point + checkDirection))
        {
            explored.Add((point, checkDirection));
            point += moveDirection;
        }
    }

    static bool IsSameType(this List<List<char>> map, Point a, Point b) =>
        map.IsInBounds(a) && map.IsInBounds(b) && map.At(a) == map.At(b);

    

    
}
