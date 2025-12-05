/*
Solution to Advent of Code 2025 - Day 5
https://adventofcode.com/2025/day/5
*/

const string INPUT_PATH = "../input.txt";

var (ranges, ingredients) = ReadInput(INPUT_PATH);

var freshIngredients = GetFreshIngredients(ranges, ingredients);
var ingredientsPerRange = GetIngredientsPerRange(ranges);

Console.WriteLine("** Day 5 **");
Console.WriteLine($"Part 1: {freshIngredients.Count()}");
Console.WriteLine($"Part 2: {ingredientsPerRange.Sum()}");

List<(long, long)> MergeRanges(List<(long start, long end)> ranges)
{
    List<(long, long)> mergedRanges = [];

    var sorted = ranges.OrderBy(r => r.start);
    var current = sorted.First();

    foreach (var range in sorted)
    {
        if (current.end >= range.start)
        {
            current.end = Math.Max(current.end, range.end);
        }
        else
        {
            mergedRanges.Add(current);
            current = range;
        }
    }

    mergedRanges.Add(current);

    return mergedRanges;
}

IEnumerable<long> GetIngredientsPerRange(List<(long start, long end)> ranges)
    => ranges.Select(r => r.end - r.start + 1);

IEnumerable<long> GetFreshIngredients(List<(long start, long end)> ranges, List<long> ingredients)
    => ingredients.Where(x => ranges.Any(r => x >= r.start && x <= r.end));

(List<(long, long)>, List<long>) ReadInput(string path)
{
    using var file = new StreamReader(path);

    List<(long, long)> ranges = [];
    while (file.ReadLine() is string line && !string.IsNullOrWhiteSpace(line))
    {
        var parts = line.Split('-');
        long rangeStart = long.Parse(parts[0]);
        long rangeEnd = long.Parse(parts[1]);

        ranges.Add((rangeStart, rangeEnd));
    }

    List<long> ingredients = [];
    while (file.ReadLine() is string line)
    {
        ingredients.Add(long.Parse(line));
    }

    ranges = MergeRanges(ranges);

    return (ranges, ingredients);
}