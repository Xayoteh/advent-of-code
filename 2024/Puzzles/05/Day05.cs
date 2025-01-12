namespace AdventOfCode2024.Puzzles;

static class Day05
{
    const string Filename = "Puzzles/05/input.txt";

    public static void Run()
    {
        (Dictionary<int, HashSet<int>> rules, List<List<int>> updates) = ReadInput();

        int part1 = updates.GetValidUpdates(rules).SumMiddleValues();
        int part2 = updates.GetInvalidUpdatesSorted(rules, new PageComparer(rules)).SumMiddleValues();

        Console.WriteLine("** Day 5 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static (Dictionary<int, HashSet<int>>, List<List<int>>) ReadInput()
    {
        using var file = File.OpenText(Filename);

        Dictionary<int, HashSet<int>> rules = [];
        List<List<int>> updates = [];

        while (file.ReadLine() is string line && !string.IsNullOrEmpty(line))
        {
            string[] values = line.Split('|');

            var before = int.Parse(values[0]);
            var after = int.Parse(values[1]);

            rules.TryAdd(before, []);
            rules[before].Add(after);
        }

        while (file.ReadLine() is string line)
        {
            var update = line.Split(',').Select(int.Parse);

            updates.Add([.. update]);
        }

        return (rules, updates);
    }

    static IEnumerable<List<int>> GetValidUpdates(this List<List<int>> updates, Dictionary<int, HashSet<int>> rules) =>
        updates.Where(update => update.IsValid(rules));

    static IEnumerable<List<int>> GetInvalidUpdatesSorted(this List<List<int>> updates, Dictionary<int, HashSet<int>> rules, PageComparer comparer) =>
        updates
        .Where(update => !update.IsValid(rules))
        .Select(update => update.Order(comparer).ToList());

    static int SumMiddleValues(this IEnumerable<List<int>> pageUpdates) =>
        pageUpdates.Sum(update => update[update.Count / 2]);

    static bool IsValid(this List<int> update, Dictionary<int, HashSet<int>> orderingRules)
    {
        HashSet<int> seen = [];

        foreach (int page in update)
        {
            seen.Add(page);

            if (!orderingRules.TryGetValue(page, out var rules)) continue;
            if (rules.Any(seen.Contains)) return false;
        }

        return true;
    }

    class PageComparer(Dictionary<int, HashSet<int>> orderingRules) : IComparer<int>
    {
        readonly Dictionary<int, HashSet<int>> OrderingRules = orderingRules;
        public int Compare(int x, int y) =>
            OrderingRules.TryGetValue(x, out var rules) ? rules.Contains(y) ? -1 : 1
            : OrderingRules.TryGetValue(x, out rules) ? rules.Contains(x) ? 1 : -1
            : 0;
    }
}
