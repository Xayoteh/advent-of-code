/*
Solution to Advent of Code 2025 - Day 6
https://adventofcode.com/2025/day/6
*/

const string INPUT_PATH = "../input.txt";

List<Problem> problems = ReadInput(INPUT_PATH);
long grandTotal = problems.Select(p => p.Solve()).Sum();

Console.WriteLine("** Day 07 **");
Console.WriteLine($"Part 1: {grandTotal}");


List<Problem> ReadInput(string path)
{
    List<List<int>> operands = [];
    List<Problem> problems = [];

    foreach (string line in File.ReadLines(path))
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        for (var i = 0; i < parts.Length; i++)
        {
            if (int.TryParse(parts[i], out int operand))
            {
                if (operands.Count <= i)
                    operands.Add([]);
                operands[i].Add(operand);
            }
            else
            {
                problems.Add(new Problem(operands[i], parts[i][0]));
            }
        }
    }

    return problems;
}

record Problem(IEnumerable<int> operands, char operation)
{
    public long Solve() => operands.Aggregate(
        operation is '+' ? 0L : 1L,
        (result, operand) 
            => operation is '+' ? result + operand : result * operand,
        result => result
    );
}