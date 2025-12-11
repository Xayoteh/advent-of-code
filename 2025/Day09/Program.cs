/*
Solution to Advent of Code 2025 - Day 9
https://adventofcode.com/2025/day/9
*/

using System.Diagnostics;

const string INPUT_PATH = "../input.txt";

List<Point> corners = ReadInput(INPUT_PATH);
var rectangles = GetRectangles(corners);

List<(Point, Point)> edges = GetEdges(corners);
var validRectangles = GetValidRectangles(rectangles, edges);

long part1 = rectangles.Max(r => r.Value);
long part2 = validRectangles.Max(r => r.Value);

Console.WriteLine("** Day 9 **");
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

Dictionary<(Point, Point), long> GetValidRectangles(Dictionary<(Point a, Point b), long>  rectangles, List<(Point, Point)> edges)
{
    return rectangles.Where(r =>
    {
        var (minX, maxX) = MinMax(r.Key.a.X, r.Key.b.X);
        var (minY, maxY) = MinMax(r.Key.a.Y, r.Key.b.Y);

        return !IsIntersection(minX, maxX, minY, maxY);
    }).ToDictionary();

    bool IsIntersection(int minX, int maxX, int minY, int maxY)
    {
        foreach(var (p1, p2) in edges)
        {
            var (iMinY, iMaxY) = MinMax(p1.Y, p2.Y);
            var (iMinX, iMaxX) = MinMax(p1.X, p2.X);

            if(minX < iMaxX && maxX > iMinX && minY < iMaxY && maxY > iMinY)
                return true;
        }
        return false;
    }
}

List<(Point, Point)> GetEdges(List<Point> corners) =>
    corners.Select((c, i) => (c, corners[(i + 1) % corners.Count])).ToList();

Dictionary<(Point, Point), long> GetRectangles(IEnumerable<Point> corners) =>
    corners.SelectMany(
        (c, i) => corners.Skip(i + 1),
        (c1, c2) => 
            new {P = (c1, c2), Area = (Math.Abs(c1.X - c2.X) + 1L) * (Math.Abs(c1.Y - c2.Y) + 1L)}
    ).ToDictionary(r => r.P, r => r.Area);

List<Point> ReadInput(string path) =>
    File.ReadAllLines(path)
        .Select(line =>
        {
            var parts = line.Split(',');
            return new Point(int.Parse(parts[0]), int.Parse(parts[1]));
        })
        .ToList();

(int, int) MinMax(int a, int b) => a < b ? (a, b) : (b, a);

record Point(int X, int Y);
