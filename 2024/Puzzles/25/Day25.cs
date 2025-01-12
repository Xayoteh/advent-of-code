namespace AdventOfCode2024.Puzzles;

static class Day25
{
    const string Filename = "Puzzles/25/input.txt";
    const int MaxPinHeight = 5;
    const int Pins = 5;

    public static void Run()
    {
        (List<List<int>> keys, List<List<int>> locks) = ReadInput();

        int part1 = CountKeyLockMatches(keys, locks);

        Console.WriteLine("** Day 25 **");
        Console.WriteLine($"Part 1: {part1}");
    }

    static (List<List<int>>, List<List<int>>) ReadInput()
    {
        const int RowsPerBlock = 7;

        List<List<int>> keys = [];
        List<List<int>> locks = [];

        List<string> block = [];

        foreach(string line in File.ReadLines(Filename))
        {
            if(string.IsNullOrEmpty(line)) continue;

            block.Add(line);

            if(block.Count is not RowsPerBlock) continue;

            int[] heights = GetBlockHeights(block);

            if(block[0][0] is '#') locks.Add([..heights]);
            else keys.Add([..heights]);

            block = [];
        }

        return (keys, locks);
    }

    static int[] GetBlockHeights(List<string> block)
    {
        var heights = new int[Pins];

        for(var i = 0; i < Pins; i++)
        {
            for(var j = 0; j < MaxPinHeight; j++)
            {
                heights[i] += block[j + 1][i] is '#' ? 1 : 0;
            }
        }

        return heights;
    }

    static int CountKeyLockMatches(List<List<int>> keys, List<List<int>> locks)
    {
        int matches = 0;

        foreach(List<int> key in keys)
        {
            foreach(List<int> @lock in locks)
            {
                if(key.IsMatch(@lock)) matches++;
            }
        }

        return matches;
    }

    static bool IsMatch(this List<int> a, List<int> b)
    {
        for(var i = 0; i < Pins; i++)
        {
            if(a[i] + b[i] > MaxPinHeight) return false;
        }

        return true;
    }      
}
