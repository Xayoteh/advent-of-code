using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day21
{
    const string Filename = "Puzzles/21/input.txt";
    static readonly Dictionary<(string, int), long> Cache = []; 
    static readonly Dictionary<char, Point> Keypad = new(){
        {'7', new(-2, -3)}, {'8', new(-1, -3)}, {'9', new(0, -3)},
        {'4', new(-2, -2)}, {'5', new(-1, -2)}, {'6', new(0, -2)},
        {'1', new(-2, -1)}, {'2', new(-1, -1)}, {'3', new(0, -1)},
        {'^', new(-1, 0)}, {'0', new(-1, 0)}, {'A', new(0, 0)},
        {'<', new(-2, 1)}, {'v', new(-1, 1)}, {'>', new(0, 1)}
    };

    public static void Run()
    {
        List<string> codes = ReadCodes();
        
        long part1 = 0;
        long part2 = 0;

        foreach (string code in codes)
        {
            part1 += GetComplexity(code, 3);
            part2 += GetComplexity(code, 26);
        }

        Console.WriteLine("** Day 21 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 1: {part2}");
    }

    static List<string> ReadCodes()
    {
        List<string> codes = [];

        foreach (string line in File.ReadLines(Filename))
        {
            codes.Add(line);
        }

        return codes;
    }

    // Complexity = code's numeric part * minimum moves needed
    static long GetComplexity(string code, int dKeypads) =>
        int.Parse(code[..(code.Length - 1)]) * GetMinimumMoves(code, dKeypads); 

    static List<string> FindMoveSequencesTo(this Point origin, Point destination, List<string> sequences, string currentSequence = "")
    {
        if (origin.IsGap()) return sequences;

        if (origin == destination)
        {
            sequences.Add(currentSequence + 'A');
            return sequences;
        }

        (int dx, int dy) = GetDistance(origin, destination);
        var xMove = dx > 0 ? '<' : '>';
        var yMove = dy > 0 ? '^' : 'v';

        if (dx is not 0)
        {
            (origin + Directions.Parse(xMove)).FindMoveSequencesTo(destination, sequences, currentSequence + xMove);
        }
        if (dy is not 0)
        {
            (origin + Directions.Parse(yMove)).FindMoveSequencesTo(destination, sequences, currentSequence + yMove);
        }

        return sequences;
    }

    static long GetMinimumMoves(string buttons, int dKeypads) =>
        Cache.TryGetValue((buttons, dKeypads), out var minimumMoves) ? minimumMoves
        : Cache[(buttons, dKeypads)] = CalculateMinimumMoves(buttons, dKeypads);

    static long CalculateMinimumMoves(string buttons, int dKeypads)
    {
        if (dKeypads is 0) return buttons.Length;

        long minimumMoves = 0;
        var previous = 'A';

        foreach (char button in buttons)
        {
            Point origin = Keypad[previous];
            Point destination = Keypad[button];
            var buttonMin = long.MaxValue;

            foreach (string moveSequence in origin.FindMoveSequencesTo(destination, []))
            {
                long sequenceMin = GetMinimumMoves(moveSequence, dKeypads - 1);

                buttonMin = Math.Min(buttonMin, sequenceMin);
            }

            minimumMoves += buttonMin;
            previous = button;
        }

        return minimumMoves;
    }

    static Point GetDistance(Point origin, Point destination) =>
        new(origin.X - destination.X, origin.Y - destination.Y);

    static bool IsGap(this Point position) => 
        position is (-2, 0);
}
