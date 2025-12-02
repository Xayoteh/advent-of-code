/*
Solution to Advent of Code 2025 - Day 2
https://adventofcode.com/2025/day/2
*/

const string INPUT_PATH = "../input.txt";

IEnumerable<(long, long)> ranges = ReadInput(INPUT_PATH);
List<long> invalidIds = GetInvalidIds(ranges);

Console.WriteLine("** Day 2 **");
Console.WriteLine($"Part 1 {invalidIds.Sum()}");

List<long> GetInvalidIds(IEnumerable<(long, long)> ranges)
{
    List<long> invalidIds = [];

    foreach(var range in ranges)
    {
        for(var id = range.Item1; id <= range.Item2; id++)
        {
            var text = $"{id}";

            if(text.Length % 2 != 0)
                continue;

            var middle = text.Length/2;

            if(text[..middle] == text[middle..])
                invalidIds.Add(id);
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
