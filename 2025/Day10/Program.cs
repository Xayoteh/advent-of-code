/*
Solution to Advent of Code 2025 - Day 10
https://adventofcode.com/2025/day/10
*/

const string INPUT_PATH = "../input.txt";

List<Machine> machines = ReadInput(INPUT_PATH);

var part1 = machines.Sum(m => GetMinimumButtonsPressLight(m.Lights, m.Buttons));
var part2 = machines.Sum(m => GetMinimumButtonsPressJoltage(m.Joltages, m.Buttons));

Console.WriteLine("** Day 10 **");
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

int GetMinimumButtonsPressJoltage(List<int> joltages, List<List<int>> buttons)
{
    Dictionary<string, int> patternCost = GetPatterns(buttons, joltages.Count, GetJoltageOutput);
    Dictionary<string, int> memo = [];

    memo.Add(string.Join(',', new int[joltages.Count]), 0);

    return Helper(joltages);

    int Helper(List<int> expectedJoltages)
    {
        string expected = string.Join(',', expectedJoltages);

        if (memo.TryGetValue(expected, out int value))
            return value;

        var res = 1000000;

        foreach ((string pattern, int cost) in patternCost)
        {
            List<int> patternJoltages = pattern.Split(',').Select(int.Parse).ToList();

            if(!IsValid(expectedJoltages, patternJoltages))
                continue;

            List<int> newExpectedJoltage = ReduceJoltage(expectedJoltages, patternJoltages);

            res = Math.Min(res, cost + 2 * Helper(newExpectedJoltage));

        }

        return memo[expected] = res;
    }

    bool IsValid(List<int> expected, List<int> pattern)
    {
        for(int i = 0; i < pattern.Count; i++)
            if(pattern[i] > expected[i] || pattern[i] % 2 != expected[i] % 2)
                return false;

        return true;
    }

    List<int> ReduceJoltage(List<int> original, List<int> toSubtract)
    {
        List<int> newJoltage = [];

        for(int i = 0; i < toSubtract.Count; i++)
            newJoltage.Add((original[i] - toSubtract[i])/2);

        return newJoltage;
    }
}

int GetMinimumButtonsPressLight(List<bool> lights, List<List<int>> buttons)
{
    Dictionary<string, int> patternCost = GetPatterns(buttons, lights.Count, GetLightOutput);
    string expected = string.Join("", lights.Select(l => l ? '#':'.'));
    var minimumPress = int.MaxValue;

    foreach((string pattern, int cost) in patternCost)
    {
        if(pattern == expected)
            minimumPress = Math.Min(minimumPress, cost);
    }

    return minimumPress;
}

Dictionary<string, int> GetPatterns(List<List<int>> buttons, int outputSize, Func<List<List<int>>, List<int>, int, string> GetOutput)
{
    Dictionary<string, int> patternCost = [];

    for (var size = 0; size <= buttons.Count; size++)
    {
        foreach (List<int> buttonsIndex in GetCombinations(buttons, size))
        {
            string pattern = GetOutput(buttons, buttonsIndex, outputSize);
            patternCost.TryAdd(pattern, size);
        }
    }

    return patternCost;
}

string GetJoltageOutput(List<List<int>> buttons, List<int> buttonIndices, int outputSize)
{
    var output = new int[outputSize];
    var indexCount = new int[outputSize];

    foreach (int buttonIndex in buttonIndices)
        foreach (int outputIndex in buttons[buttonIndex])
            indexCount[outputIndex]++;

    for (var i = 0; i < outputSize; i++)
        output[i] = indexCount[i];

    return string.Join(',', output);
}

string GetLightOutput(List<List<int>> buttons, List<int> buttonIndices, int outputSize)
{
    var output = new bool[outputSize];
    var indexCount = new int[outputSize];

    foreach (int buttonIndex in buttonIndices)
        foreach (int outputIndex in buttons[buttonIndex])
            indexCount[outputIndex]++;

    for (var i = 0; i < outputSize; i++)
        output[i] = indexCount[i] % 2 != 0;

    return string.Join("", output.Select(l => l ? '#':'.'));
}

List<List<int>> GetCombinations(List<List<int>> items, int size)
{
    List<List<int>> combinations = [];
    Stack<int> currentCombination = [];

    Backtrack();

    return combinations;

    void Backtrack(int index = 0)
    {
        if (currentCombination.Count == size)
        {
            combinations.Add([.. currentCombination]);
            return;
        }

        if (index == items.Count)
            return;

        Backtrack(index + 1);

        currentCombination.Push(index);
        Backtrack(index + 1);
        currentCombination.Pop();
    }
}

List<Machine> ReadInput(string path)
{
    List<Machine> machines = [];

    foreach (string line in File.ReadLines(path))
    {
        string[] parts = line.Split(' ');

        List<bool> lights = parts[0][1..^1].Select(l => l == '#').ToList();
        List<List<int>> buttons = parts[1..^1].Select(b => b[1..^1].Split(',').Select(int.Parse).ToList()).ToList();
        List<int> joltages = parts[^1][1..^1].Split(',').Select(int.Parse).ToList();

        machines.Add(new(lights, buttons, joltages));
    }

    return machines;
}

record Machine(List<bool> Lights, List<List<int>> Buttons, List<int> Joltages);
