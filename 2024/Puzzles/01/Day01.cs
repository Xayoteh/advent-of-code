namespace AdventOfCode2024.Puzzles;

static class Day01
{
    const string Filename = "Puzzles/01/input.txt";
    
    public static void Run()
    {
        (List<int> left, List<int> right) = ReadInput();

        int part1 = left.Order().Zip(right.Order(), (x, y) => Math.Abs(x - y)).Sum();
        int part2 = right.Where(new HashSet<int>(left).Contains).Sum();

        Console.WriteLine("** Day 1 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static (List<int>, List<int>) ReadInput()
    {
        List<int> leftValues = [];
        List<int> rightValues = [];

        foreach(string line in File.ReadLines(Filename))
        {
            string[] values = line.Split("   ");
            
            var left = int.Parse(values[0]);
            var right = int.Parse(values[1]);

            leftValues.Add(left);
            rightValues.Add(right);
        }

        return (leftValues, rightValues);
    }
}
