using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day08
{
    const string Filename = "Puzzles/08/input.txt";

    public static void Run()
    {
        List<List<char>> map = ReadMap();
        Dictionary<char, List<Point>> antennas = map.LocateAntennas();

        int part1 = map.GetPairAntinodes(antennas).Count;
        int part2 = map.GetInlineAntinodes(antennas).Count;

        Console.WriteLine("** Day 8 **");
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

    static Dictionary<char, List<Point>> LocateAntennas(this List<List<char>> map)
    {
        Dictionary<char, List<Point>> antennas = [];

        for (var y = 0; y < map.Count; y++)
        {
            for (var x = 0; x < map[0].Count; x++)
            {
                char frequency = map[y][x];

                if (frequency is not '.')
                {
                    antennas.TryAdd(frequency, []);
                    antennas[frequency].Add(new(x, y));
                }
            }
        }

        return antennas;
    }

    static HashSet<Point> GetPairAntinodes(this List<List<char>> map, Dictionary<char, List<Point>> antennas)
    {
        HashSet<Point> antinodes = [];

        foreach ((char frequecy, List<Point> points) in antennas)
        {
            foreach ((Point antennaA, Point antennaB) in points.Pairs())
            {
                Point antinodeA = antennaA + antennaA - antennaB;
                Point antinodeB = antennaB + antennaB - antennaA;

                if (map.IsInBounds(antinodeA)) antinodes.Add(antinodeA);
                if (map.IsInBounds(antinodeB)) antinodes.Add(antinodeB);
            }
        }

        return antinodes;
    }

    static HashSet<Point> GetInlineAntinodes(this List<List<char>> map, Dictionary<char, List<Point>> antennas)
    {
        HashSet<Point> antinodes = [];

        foreach ((char frequecy, List<Point> points) in antennas)
        {
            foreach ((Point antennaA, Point antennaB) in points.Pairs())
            {
                Point directionA = antennaA - antennaB;
                Point directionB = antennaB - antennaA;

                antinodes.UnionWith(map.GetInlineAntinodes(antennaA, directionA));
                antinodes.UnionWith(map.GetInlineAntinodes(antennaB, directionB));
            }
        }

        return antinodes;
    }

    static List<Point> GetInlineAntinodes(this List<List<char>> map, Point position, Point direction)
    {
        List<Point> locations = [];

        while (map.IsInBounds(position))
        {
            locations.Add(position);
            position += direction;
        }

        return locations;
    }
}
