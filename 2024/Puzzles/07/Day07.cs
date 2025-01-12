namespace AdventOfCode2024.Puzzles;

static class Day07
{
    const string Filename = "Puzzles/07/input.txt";

    public static void Run()
    {
        List<Equation> equations = ReadEquations();

        long part1 = equations.GetTrueEquations(['+', '*']).SumTestValues();
        long part2 = equations.GetTrueEquations(['+', '*', '|']).SumTestValues();

        Console.WriteLine("** Day 7 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<Equation> ReadEquations()
    {
        List<Equation> equations = [];

        foreach (string line in File.ReadLines(Filename))
        {
            equations.Add(Equation.Parse(line));
        }

        return equations;
    }

    static long SumTestValues(this IEnumerable<Equation> equations) =>
        equations.Sum(equation => equation.TestValue);

    static IEnumerable<Equation> GetTrueEquations(this IEnumerable<Equation> equations, char[] operators) =>
        equations.Where(equation => equation.IsPossiblyTrue(operators));

    static long Evaluate(this char op, long a, int b) =>
        op is '+' ? a + b
        : op is '*' ? a * b
        : long.Parse($"{a}{b}");

    record Equation(long TestValue, List<int> Numbers)
    {
        public static Equation Parse(string rawData)
        {
            string[] equationData = rawData.Split(' ');

            var result = long.Parse(equationData[0][..^1]);
            List<int> numbers = [.. equationData[1..].Select(int.Parse)];

            return new(result, numbers);
        }

        public bool IsPossiblyTrue(char[] operators, long currResult = 0, int valueIndex = 0)
        {
            if (valueIndex is 0) return IsPossiblyTrue(operators, Numbers[0], 1);
            if (currResult > TestValue) return false;
            if (valueIndex == Numbers.Count) return currResult == TestValue;

            foreach (char op in operators)
            {
                long newResult = op.Evaluate(currResult, Numbers[valueIndex]);

                if (IsPossiblyTrue(operators, newResult, valueIndex + 1)) return true;
            }

            return false;
        }
    }
}
