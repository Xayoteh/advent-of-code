namespace AdventOfCode2024.Puzzles;

static class Day19
{
    const string Filename = "Puzzles/19/input.txt";

    public static void Run()
    {
        (HashSet<string> patterns, List<string> designs) = ReadTowels();
        Dictionary<string, long> waysToFormDesings = designs.GetWaysToFormDesigns(patterns);

        int part1 = waysToFormDesings.Count(kvp => kvp.Value is not 0);
        long part2 = waysToFormDesings.Sum(kvp => kvp.Value);

        Console.WriteLine("** Day 19 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static Dictionary<string, long> GetWaysToFormDesigns(this List<string> designs, HashSet<string> patterns)
    {
        Dictionary<string, long> waysToFormDesing = [];
        Dictionary<string, long> cache = [];

        foreach(string design in designs)
        {
            long waysToForm = design.GetDifferentWays(patterns, cache);

            if(waysToForm is not 0) waysToFormDesing[design] = waysToForm;
        }

        return waysToFormDesing;
    }

    static long GetDifferentWays(this string design, HashSet<string> patterns, Dictionary<string, long> cache) =>
        cache.TryGetValue(design, out var differentWays) ? differentWays
        : cache[design] = design.CountDifferentWays(patterns, cache);

    static long CountDifferentWays(this string design, HashSet<string> patterns, Dictionary<string, long> cache)
    {
        if(string.IsNullOrEmpty(design)) return 1;

        long differentWays = 0;

        for(var i = 0; i < design.Length; i++)
        {
            if(patterns.Contains(design[..(i + 1)]))
            {
                differentWays += design[(i + 1)..].GetDifferentWays(patterns, cache);
            }
        }

        return differentWays;
    }

    static (HashSet<string>, List<string>) ReadTowels()
    {
        using var file = File.OpenText(Filename);

        HashSet<string> patterns = [];

        while(file.ReadLine() is string line && !string.IsNullOrEmpty(line))
        {
            var values = line.Split(',').Select(s => s.Trim());

            foreach(string value in values)
            {
                patterns.Add(value);
            }
        }

        List<string> designs = [];

        while(file.ReadLine() is string design)
        {
            designs.Add(design);
        }

        return (patterns, designs);
    }
}
