/*
Solution to Advent of Code 2025 - Day 2
https://adventofcode.com/2025/day/2
*/

using System.Text;

const string INPUT_PATH = "../input.txt";

IEnumerable<(long, long)> ranges = ReadInput(INPUT_PATH);
List<long> invalidIds = GetInvalidIds(ranges);
List<long> invalidIds2 = GetInvalidIds(ranges, onlyHalfs: false);

Console.WriteLine("** Day 2 **");
Console.WriteLine($"Part 1: {invalidIds.Sum()}");
Console.WriteLine($"Part 2: {invalidIds2.Sum()}");

List<long> GetInvalidIds(IEnumerable<(long, long)> ranges, bool onlyHalfs = true)
{
    List<long> invalidIds = [];

    foreach (var range in ranges)
    {
        var sb = new StringBuilder();

        for (var id = range.Item1; id <= range.Item2; id++)
        {
            sb.Clear();

            var text = $"{id}";
            int limit = onlyHalfs ? 2 : text.Length;

            for (var p = 2; p <= limit; p++)
            {
                if (text.Length % p != 0)
                    continue;

                int length = text.Length / p;

                for (int _ = 0; _ < p; _++)
                    sb.Append(text[..length]);

                if (sb.ToString() == text)
                {
                    invalidIds.Add(id);
                    break;
                }

                sb.Clear();
            }
        }
    }

    return invalidIds;
}

IEnumerable<(long, long)> ReadInput(string path) =>
    File.ReadAllText(path).Split(',').Select(line =>
    {
        var parts = line.Split('-');
        return (long.Parse(parts[0]), long.Parse(parts[1]));
    });
