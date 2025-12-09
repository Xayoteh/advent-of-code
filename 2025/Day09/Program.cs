/*
Solution to Advent of Code 2025 - Day 9
https://adventofcode.com/2025/day/9
*/

const string INPUT_PATH = "../input.txt";

IEnumerable<Point> redTiles = ReadInput(INPUT_PATH);
IEnumerable<long> rectangleAreas = GetRectangleAreas(redTiles);

long part1 = rectangleAreas.Max();

Console.WriteLine("** Day 9 **");
Console.WriteLine($"Part 1: {part1}");

IEnumerable<long> GetRectangleAreas(IEnumerable<Point> corners) =>
    corners.SelectMany(
        c => corners.Where(p => p.X != c.X && p.Y != c.Y),
        (c1, c2) => (long)Math.Abs(c1.X - c2.X + 1) * Math.Abs(c1.Y - c2.Y + 1)
    );

IEnumerable<Point> ReadInput(string path) =>
    File.ReadAllLines(path)
        .Select(
            line => new Point(int.Parse(line.Split(',')[0]), int.Parse(line.Split(',')[1]))
        );

record Point(int X, int Y);
