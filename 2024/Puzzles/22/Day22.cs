using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day22
{
    const string Filename = "Puzzles/22/input.txt";
    const int ithIteration = 2000;

    public static void Run()
    {
        List<long> initialValues = ReadInitialValues();
        Dictionary<string, int> bananasPerSequence = [];

        long part1 = 0;

        foreach (long initialValue in initialValues)
        {
            (long finalValue, List<(int, int)> priceChanges) = ComputeSecretNumber(initialValue);

            part1 += finalValue;
            AddBananasPerSequence(priceChanges, bananasPerSequence);
        }

        long part2 = bananasPerSequence.Values.Max();

        Console.WriteLine("** Day 22 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static (long, List<(int, int)>) ComputeSecretNumber(long initialValue)
    {
        List<(int, int)> priceChanges = [];

        long secretNumber = initialValue;
        var currentPrice = (int)secretNumber % 10;

        for (var i = 0; i < ithIteration; i++)
        {
            secretNumber = GetNextSecretNumber(secretNumber);

            var newPrice = (int)(secretNumber % 10);
            int priceChange = newPrice - currentPrice;

            priceChanges.Add((newPrice, priceChange));
            currentPrice = newPrice;
        }

        return (secretNumber, priceChanges);
    }

    static void AddBananasPerSequence(List<(int price, int change)> priceChanges, Dictionary<string, int> bananasPerSequence)
    {
        LinkedList<int> changeSequence = new(priceChanges[..3].Select(x => x.change));
        HashSet<string> seen = [];

        for (var i = 3; i < priceChanges.Count; i++)
        {
            changeSequence.AddLast(priceChanges[i].change);

            int price = priceChanges[i].price;
            string sequence = changeSequence.Join(',');

            changeSequence.RemoveFirst();

            if (seen.Add(sequence))
            {
                bananasPerSequence.TryAdd(sequence, 0);
                bananasPerSequence[sequence] += price;
            }
        }
    }

    static List<long> ReadInitialValues()
    {
        List<long> values = [];

        foreach (string line in File.ReadLines(Filename))
        {
            values.Add(long.Parse(line));
        }

        return values;
    }

    static long GetNextSecretNumber(long previous)
    {
        previous = (previous ^ previous << 6) % 16777216;
        previous = (previous ^ previous >> 5) % 16777216;
        previous = (previous ^ previous << 11) % 16777216;

        return previous;
    }
}
