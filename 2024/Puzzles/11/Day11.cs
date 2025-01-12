namespace AdventOfCode2024.Puzzles;

static class Day11
{
    const string Filename = "Puzzles/11/input.txt";
    static readonly Dictionary<(long, int), long> Cache = [];

    public static void Run()
    {
        List<long> stones = GetStones();

        long part1 = stones.CountStones(25);
        long part2 = stones.CountStones(75);

        Console.WriteLine($"** Day 11 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<long> GetStones()
    {
        List<long> stones = [];

        foreach (string line in File.ReadLines(Filename))
        {
            stones.AddRange([.. line.Split(' ').Select(long.Parse)]);
        }

        return stones;
    }

    static long CountStones(this IEnumerable<long> stones, int blinks) =>
        stones.Sum(stone => stone.CountStones(blinks));

    static long CountStones(this long stone, int blinks) =>
        blinks is 0 ? 1
        : Cache.TryGetValue((stone, blinks), out var count) ? count
        : Cache[(stone, blinks)] = stone.Blink().CountStones(blinks - 1);

    static long[] Blink(this long stone) =>
        stone is 0 ? [1]
        : stone.DigitsCount() is int count && count % 2 is 0 ? stone.Split((count / 2).Pow10())
        : [stone * 2024];

    static int Pow10(this int n) =>
        (int)Math.Pow(10, n);

    static long[] Split(this long n, int div) =>
        [n / div, n % div];
    
    static int DigitsCount(this long n) =>
        (int)Math.Log10(n) + 1;
}
