/*
Solution to Advent of Code 2025 - Day 3
https://adventofcode.com/2025/day/3
*/

const string INPUT_PATH = "../input.txt";

IEnumerable<string> batteryBanks = ReadInput(INPUT_PATH);

List<long> banksJoltages = GetJoltages(batteryBanks, 2);
List<long> banksJoltages2 = GetJoltages(batteryBanks, 12);

Console.WriteLine("** Day 3 **");
Console.WriteLine($"Part 1: {banksJoltages.Sum()}");
Console.WriteLine($"Part 2: {banksJoltages2.Sum()}");

long GetMaxJoltage(string bank, int batteriesNeeded)
{
    var batteriesOn = new Stack<int>();

    for(var i = 0; i < bank.Length; i++)
    {
        var battery = bank[i] - '0';
        var remainingBatteries = bank.Length - i;
        
        while(batteriesOn.Count > 0 && battery > batteriesOn.Peek() 
            && remainingBatteries >= batteriesNeeded - batteriesOn.Count + 1)
        {
            batteriesOn.Pop();
        }

        if(batteriesOn.Count < batteriesNeeded)
            batteriesOn.Push(battery);
    }

    return GetJoltage(batteriesOn);
}

long GetJoltage(Stack<int> batteries)
{
    long joltage = 0;
    long multiplier = 1;

    foreach (int battery in batteries)
    {
        joltage += battery * multiplier;
        multiplier *= 10;
    }

    return joltage;
}

List<long> GetJoltages(IEnumerable<string> batteryBanks, int batteriesNeeded)
{
    List<long> joltages = [];

    foreach (string bank in batteryBanks)
        joltages.Add(GetMaxJoltage(bank, batteriesNeeded));

    return joltages;
}


IEnumerable<string> ReadInput(string path) =>
    File.ReadLines(path);