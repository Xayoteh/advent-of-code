/*
Solution to Advent of Code 2025 - Day 6
https://adventofcode.com/2025/day/6
*/

const string INPUT_PATH = "../input.txt";

string[][] problemsData = ReadInput(INPUT_PATH);

Problem[] problems = ParseProblems(problemsData);
long total = problems.Select(p => p.Solve()).Sum();

Problem[] realProblems = ParseProblemsByColumn(problemsData);
long realTotal = realProblems.Select(p => p.Solve()).Sum();

Console.WriteLine("** Day 06 **");
Console.WriteLine($"Part 1: {total}");
Console.WriteLine($"Part 2: {realTotal}");


Problem[] ParseProblems(string[][] problemsData)
{
    var problems = new Problem[problemsData[0].Length];

    for(var c = 0; c < problemsData[0].Length; c++)
    {
        var operands = new int[problemsData.Length - 1];
        char operation = problemsData[^1][c][0];

        for(var r = 0; r < problemsData.Length - 1; r++)
            operands[r] = int.Parse(problemsData[r][c]);


        problems[c] = new Problem(operands, operation);
    }

    return problems;
}

Problem[] ParseProblemsByColumn(string[][] problemsData)
{
    var problems = new Problem[problemsData[0].Length];

    for(var c = 0; c < problemsData[0].Length; c++)
    {
        var operands = new int[problemsData[0][c].Length];
        char operation = problemsData[^1][c][0];

        for(var i = 0; i < problemsData[0][c].Length; i++)
            for(var r = 0; r < problemsData.Length - 1; r++)
                operands[i] = problemsData[r][c][i] == ' ' ? operands[i]
                    : operands[i] * 10 + problemsData[r][c][i] - '0';

        problems[c] = new Problem(operands, operation);
    }

    return problems;
}

// TODO: refactor this?
string[][] ReadInput(string path)
{
    var lines = File.ReadAllLines(path);
    List<int> columnsWidths = [];

    string lastLine = lines.Last();
    int start = 0;

    while(lastLine.IndexOfAny(['+', '*'], start + 1) is int end && end != -1)
    {
        int width = end - start - 1;
        columnsWidths.Add(width);
        start = end;
    }

    columnsWidths.Add(lastLine.Length - start);

    string[][] problemsData = new string[lines.Length][];

    for(var i = 0; i < lines.Length - 1; i++)
    {
        var ss = new StringReader(lines[i]);

        problemsData[i] = new string[columnsWidths.Count];

        for(var j = 0; j < columnsWidths.Count; j++)
        {
            int width = columnsWidths[j];
            var buffer = new char[width];

            ss.ReadBlock(buffer, 0, width);
            ss.Read();
            problemsData[i][j] = new string(buffer);
        }
    }

    problemsData[^1] = lastLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    return problemsData;
}

record Problem(IEnumerable<int> Operands, char Operation)
{
    public long Solve() => Operands.Aggregate(
        Operation is '+' ? 0L : 1L,
        (result, operand) 
            => Operation is '+' ? result + operand : result * operand,
        result => result
    );
}