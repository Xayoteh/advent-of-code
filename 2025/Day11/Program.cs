/*
Solution to Advent of Code 2025 - Day 11
https://adventofcode.com/2025/day/11
*/

const string INPUT_PATH = "../input.txt";

Dictionary<string, List<string>> deviceOutputs = ReadInput(INPUT_PATH);
List<List<string>> paths = GetPaths(deviceOutputs, "you", "out");

int part1 = paths.Count;

Console.WriteLine("** Day 11 **");
Console.WriteLine($"Part 1: {part1}");

List<List<string>> GetPaths(Dictionary<string, List<string>> deviceOutputs, string origin, string destination)
{
    List<List<string>> paths = [];
    List<string> currentPath = [];
    
    if(deviceOutputs.ContainsKey(origin))
        Backtrack(origin);

    return paths;

    void Backtrack(string current)
    {
        currentPath.Add(current);

        if(current == destination)
        {
            paths.Add([..currentPath]);
            currentPath.RemoveAt(currentPath.Count - 1);
            return;
        }

        foreach(string output in deviceOutputs[current])
            Backtrack(output);

        currentPath.RemoveAt(currentPath.Count - 1);
    }
}

Dictionary<string, List<string>> ReadInput(string path) =>
    File.ReadLines(path)
        .Select(l =>
        {
            var parts = l.Split(": ");
            
            string device = parts[0];
            List<string> outputs = parts[1].Split().ToList();

            return new {device, outputs};
        }).ToDictionary(a => a.device, a => a.outputs);