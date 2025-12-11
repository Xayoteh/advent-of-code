/*
Solution to Advent of Code 2025 - Day 9
https://adventofcode.com/2025/day/9
*/

const string INPUT_PATH = "../input.txt";

List<Machine> machines = ReadInput(INPUT_PATH);

// foreach(var machine in machines)
// {
//     foreach(var sol in GetMachineSolutions(machine))
//     {
//         foreach(var button in sol)
//         {
//             var b = string.Join(",", machine.Buttons[button]);
//             Console.Write($"{b} - ");
//         }
//         Console.WriteLine();
//     }

//     Console.WriteLine("---------------");
// }

var solutions = machines.Select(m => GetMachineSolutions(m).MinBy(s => s.Count));

var part1 = solutions.Sum(s => s!.Count);

Console.WriteLine("** Day 10 **");
Console.WriteLine($"Part 1: {part1}");

void Toggle(List<bool> state, List<int>lightsToToggle)
{
    foreach(var light in lightsToToggle)
        state[light] = !state[light];
}

List<List<int>> GetMachineSolutions(Machine m)
{
    List<List<int>> solutions = [];
    Stack<int> currentCombination = [];
    List<bool> currentState = m.Lights.Select(_ => false).ToList();

    Backtrack();

    return solutions;

    void Backtrack(int idx = 0)
    {
        if(currentState.SequenceEqual(m.Lights))
        {
            solutions.Add([..currentCombination]);
            return;
        }

        if(idx == m.Buttons.Count)
            return;       

        Backtrack(idx + 1);

        Toggle(currentState, m.Buttons[idx]);
        currentCombination.Push(idx);

        Backtrack(idx + 1);

        currentCombination.Pop();
        Toggle(currentState, m.Buttons[idx]);
    }
}

List<Machine> ReadInput(string path)
{
    List<Machine> machines = [];

    foreach(string line in File.ReadLines(path))
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
