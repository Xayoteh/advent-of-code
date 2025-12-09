/*
Solution to Advent of Code 2025 - Day 8
https://adventofcode.com/2025/day/8
*/

const string INPUT_PATH = "../input.txt";

List<Point> boxes = ReadInput(INPUT_PATH);
IEnumerable<(Point, Point, double)> pairs = GetDistances(boxes);

List<HashSet<Point>> circuits = ConnectBoxes(pairs, 1000);
IEnumerable<int> circuitsSizes = circuits.Select(c => c.Count).OrderByDescending(s => s);
int part1 = circuitsSizes.Take(3).Aggregate((acc, x) => acc * x);

(Point a, Point b) = ConnectAllBoxes(pairs, 1000, circuits, boxes.Count);
int part2 = a.X * b.X;

Console.WriteLine("** Day 8 **");
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

(Point, Point) ConnectAllBoxes(IEnumerable<(Point a, Point b, double d)> pairs, int skip, List<HashSet<Point>> circuits, int boxes)
{
    foreach(var (a, b, d) in pairs.Skip(skip))
    {
        var candidates = circuits.Where(c => c.Contains(a) || c.Contains(b));
        HashSet<Point> circuit = [];
        
        switch(candidates.Count())
        {
            case 0:
                circuits.Add(circuit);
                break;
            case 1:
                circuit = candidates.First();
                break;
            default:
                circuits = circuits.Where(c => !c.Contains(a) && !c.Contains(b)).ToList();

                circuit = candidates.First();
                circuit.UnionWith(candidates.Last());

                circuits.Add(circuit);
                break;
        }

        circuit.Add(a);
        circuit.Add(b);   

        if(circuits[0].Count == boxes)
            return (a, b);
    }

    return (new(0, 0, 0), new(0, 0, 0));
}

List<HashSet<Point>> ConnectBoxes(IEnumerable<(Point a, Point b, double d)> pairs, int n)
{
    var nClosest = pairs.Take(n);
    List<HashSet<Point>> circuits = [];

    foreach(var (a, b, d) in nClosest)
    {
        var candidates = circuits.Where(c => c.Contains(a) || c.Contains(b));
        HashSet<Point> circuit = [];
        
        switch(candidates.Count())
        {
            case 0:
                circuits.Add(circuit);
                break;
            case 1:
                circuit = candidates.First();
                break;
            default:
                circuits = circuits.Where(c => !c.Contains(a) && !c.Contains(b)).ToList();

                circuit = candidates.First();
                circuit.UnionWith(candidates.Last());

                circuits.Add(circuit);
                break;
        }

        circuit.Add(a);
        circuit.Add(b);
    }

    return circuits;
}

IEnumerable<(Point, Point, double)> GetDistances(List<Point> points)
    => points.SelectMany(
        (p1, i) => points.Skip(i + 1),
        (p1, p2) => (p1, p2, p1.GetDistanceTo(p2)))
        .OrderBy(p => p.Item3);

List<Point> ReadInput(string path)
{
    List<Point> points = [];

    foreach (string line in File.ReadLines(path))
    {
        string[] parts = line.Split(',');

        int x = int.Parse(parts[0]);
        int y = int.Parse(parts[1]);
        int z = int.Parse(parts[2]);

        points.Add(new Point(x, y, z));
    }

    return points;
}

record Point(int X, int Y, int Z)
{
    public double GetDistanceTo(Point p)
    {
        int dx = p.X - X;
        int dy = p.Y - Y;
        int dz = p.Z - Z;

        return Math.Sqrt((long)dx * dx + (long)dy * dy + (long)dz * dz);
    }
}