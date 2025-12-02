/*
Solution to Advent of Code 2025 - Day 1
https://adventofcode.com/2025/day/1
*/

const string INPUT_PATH = "../input.txt";

IEnumerable<string> rotations = ReadInput(INPUT_PATH);

List<int> dialSequence = GetDialSequence(rotations);
int password = dialSequence.Count(x => x == 0);

Console.WriteLine("** Day 1 **");
Console.WriteLine($"Part 1: {password}");

IEnumerable<string> ReadInput(string path) =>
    File.ReadLines(path).Select(x => x.Trim());

List<int> GetDialSequence(IEnumerable<string> rotations)
{
    List<int> sequence = [];
    int current = 50;

    foreach(string rotation in rotations)
    {
        char direction = rotation[0];
        int distance = int.Parse(rotation[1..]) % 100;

        current = direction switch {
            'L' when distance > current => 99 - distance + 1 + current,
            'L' => current - distance,
            _ => (current + distance) % 100
        };

        sequence.Add(current);
    }

    return sequence;
}