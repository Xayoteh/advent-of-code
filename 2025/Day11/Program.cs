/*
Solution to Advent of Code 2025 - Day 11
https://adventofcode.com/2025/day/11
*/

const string INPUT_PATH = "../input.txt";

Dictionary<string, List<string>> deviceOutputs = ReadInput(INPUT_PATH);
long youToOut = CountPaths(deviceOutputs, "you", "out");

// svr -> fft -> dac -> out
long svrToFft = CountPaths(deviceOutputs, "svr", "fft");
long fftToDac = CountPaths(deviceOutputs, "fft", "dac");
long dacToOut = CountPaths(deviceOutputs, "dac", "out");

// svr -> dac -> fft -> out
long svrToDac = CountPaths(deviceOutputs, "svr", "dac");
long dacToFft = CountPaths(deviceOutputs, "dac", "fft");
long fftToOut = CountPaths(deviceOutputs, "fft", "out");

long part1 = youToOut;
long part2 = svrToFft * fftToDac * dacToOut + svrToDac * dacToFft * fftToOut;

Console.WriteLine("** Day 11 **");
Console.WriteLine($"Part 1: {part1}");
Console.WriteLine($"Part 2: {part2}");

long CountPaths(Dictionary<string, List<string>> deviceOutputs, string origin, string destination)
{
    Dictionary<string, long> memo = [];

    memo[destination] = 1;

    return Aux(origin);

    long Aux(string current)
    {
        if(memo.TryGetValue(current, out long paths))
            return paths;

        paths = 0;

        if(deviceOutputs.TryGetValue(current, out List<string>? outputs))
            foreach(string output in outputs)
                paths += Aux(output);

        return memo[current] = paths;
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