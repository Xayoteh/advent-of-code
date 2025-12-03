/*
Solution to Advent of Code 2025 - Day 3
https://adventofcode.com/2025/day/3
*/

const string INPUT_PATH = "../input.txt";

IEnumerable<string> batteryBanks = ReadInput(INPUT_PATH);
List<int> banksJoltages = GetJoltages(batteryBanks);

Console.WriteLine("** Day 3 **");
Console.WriteLine($"Part 1: {banksJoltages.Sum()}");

int GetMaxJoltage(string bank)
{
    var d = 0;
    var u = -1;

    for(var i = 0; i < bank.Length - 1; i++)
    {
        var battery = bank[i] - '0';

        if(battery > d)
        {
            d = battery;
            u = - 1;
        }
        else if(battery > u)
        {
            u = battery;
        }
    }

    u = Math.Max(u, bank[^1] - '0');

    return d * 10 + u; 
}

List<int> GetJoltages(IEnumerable<string> batteryBanks)
{
    List<int> joltages = [];

    foreach(string bank in batteryBanks)
    {
        joltages.Add(GetMaxJoltage(bank));
    }

    return joltages;
}


IEnumerable<string> ReadInput(string path) =>
    File.ReadLines(path);