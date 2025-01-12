namespace AdventOfCode2024.Puzzles;

static class Day02
{
    const string Filename = "Puzzles/02/input.txt";

    public static void Run()
    {
        List<List<int>> reports = ReadReports();

        int part1 = reports.Count(IsSafe);
        int part2 = reports.Count(report => report.Expand().Any(IsSafe));

        Console.WriteLine("** Day 2 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<List<int>> ReadReports()
    {
        List<List<int>> reports = [];

        foreach (string line in File.ReadLines(Filename))
        {
            var report = line.Split(' ').Select(int.Parse);

            reports.Add([..report]);
        }

        return reports;
    }

    static IEnumerable<(int prev, int next)> AdjacentPairs(this List<int> values)
    {
        using var enumerator = values.GetEnumerator();

        if (!enumerator.MoveNext()) yield break;

        int prev = enumerator.Current;

        while (enumerator.MoveNext())
        {
            yield return (prev, enumerator.Current);

            prev = enumerator.Current;
        }
    }

    static bool IsSafe(this (int prev, int next) pair, int expectedSign) =>
        Math.Abs(pair.next - pair.prev) is >= 1 and <= 3 && 
        Math.Sign(pair.next - pair.prev) == expectedSign;

    static bool IsSafe(this List<int> values) =>
        values.Count < 2 || values.IsSafe(Math.Sign(values[1] - values[0]));

    static bool IsSafe(this List<int> values, int expectedSign) =>
        values.AdjacentPairs().All(pair => pair.IsSafe(expectedSign));

    static IEnumerable<List<int>> Expand(this List<int> values) =>
        new[] { values }.Concat(Enumerable.Range(0, values.Count).Select(values.ExceptAt));

    static List<int> ExceptAt(this List<int> values, int index) =>
        values.Take(index).Concat(values.Skip(index + 1)).ToList();

}