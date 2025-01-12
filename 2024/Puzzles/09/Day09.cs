namespace AdventOfCode2024.Puzzles;

static class Day09
{
    const string Filename = "Puzzles/09/input.txt";

    public static void Run()
    {
        List<char> memory = GetMemoryBlocks();

        long part1 = memory.RearrangeBlocks().GetChecksum();
        long part2 = memory.RearrangeFiles().GetChecksum();

        Console.WriteLine("** Day 9 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static string ReadDiskMap()
    {
        var diskMap = "";

        foreach(string line in File.ReadLines(Filename))
        {
            diskMap += line;
        }
        
        return diskMap;
    }

    static List<char> GetMemoryBlocks()
    {
        string diskMap = ReadDiskMap();

        List<char> memory = [];
        var currId = 0;

        for (var i = 0; i < diskMap.Length; i++)
        {
            int count = diskMap[i] - '0';
            var value = i % 2 is not 0 ? '.' : (char)('0' + currId++);

            memory.AddRange(new string(value, count));
        }

        return memory;
    }

    static List<char> RearrangeBlocks(this List<char> memory)
    {
        List<char> rearranged = [..memory];
        int r = rearranged.Count - 1;
        var l = 0;

        while(l < r)
        {
            while(l < r && rearranged[r] is '.') r--;
            while(l < r && rearranged[l] is not '.') l++;

            (rearranged[r], rearranged[l]) = (rearranged[l], rearranged[r]);
        }

        return rearranged;
    }

    static List<char> RearrangeFiles(this List<char> memory)
    {
        List<char> rearranged = [..memory];
        (int start, int size) fileBlock = rearranged.GetNextFileBlock(rearranged.Count);

        while (fileBlock.size is not 0)
        {
            (int start, int size) emptyBlock = rearranged.FindEmptyBlock(fileBlock);

            if (emptyBlock.size >= fileBlock.size) SwapMemoryBlocks(rearranged, fileBlock, emptyBlock);

            fileBlock = rearranged.GetNextFileBlock(fileBlock.start);
        }

        return rearranged;
    }

    static (int start, int size) GetNextFileBlock(this List<char> memory, int rBound)
    {
        int i = rBound - 1;
        int start; 
        int end;

        while (i >= 0 && memory[i] == '.') i--;
        end = i;

        while (i >= 0  && memory[i] == memory[end]) i--;
        start = i + 1;

        return (start, end - start + 1);
    }

    static (int start, int size) FindEmptyBlock(this List<char> memory, (int start, int size) block)
    {
        var i = 0;
        var start = 0;
        var end = -1;

        while (i < block.start && (end - start + 1) < block.size)
        {
            i = end + 1;

            while (i < block.start && memory[i] != '.') i++;
            start = i;

            while (i < block.start && memory[i] == '.') i++;
            end = i - 1;
        }

        return (start, end - start + 1);
    }

    static void SwapMemoryBlocks(List<char> memory, (int start, int size) file, (int start, int size) empty)
    {
        for (var i = 0; i < file.size; i++)
        {
            (memory[file.start + i], memory[empty.start + i]) = (memory[empty.start + i], memory[file.start + i]);
        }
    }

    static long GetChecksum(this List<char> memory) =>
        memory
        .Select((block, i) => (block, i))
        .Where(data => data.block is not '.')
        .Sum(data => (long)data.i * (data.block - '0'));
}
