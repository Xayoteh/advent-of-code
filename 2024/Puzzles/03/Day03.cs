using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Puzzles;

static class Day03
{
    const string Filename = "Puzzles/03/input.txt";

    public static void Run()
    {
        List<Instruction> instructions = GetInstructions();

        int part1 = instructions.OfType<Multiply>().SumProducts();
        int part2 = instructions.SumProducts();

        Console.WriteLine("** Day 3 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static int SumProducts(this IEnumerable<Instruction> instructions)
    {
        var sum = 0;
        var @do = true;

        foreach (Instruction instruction in instructions)
        {
            if (instruction is Do) @do = true;
            else if (instruction is Dont) @do = false;
            else if (@do && instruction is Multiply mul) sum += mul.Result;
        }

        return sum;
    }

    static string ReadMemory()
    {
        var sb = new StringBuilder();

        foreach (string line in File.ReadLines(Filename))
        {
            sb.Append(line);
        }

        return sb.ToString();
    }

    static List<Instruction> GetInstructions()
    {
        string memory = ReadMemory();

        string pattern = @"(?<mul>mul)\((?<x>\d{1,3}),(?<y>\d{1,3})\)|(?<do>do)\(\)|(?<dont>don't)\(\)";
        List<Instruction> instructions = [];

        foreach (Match match in Regex.Matches(memory, pattern))
        {
            instructions.Add(Instruction.Parse(match));
        }

        return instructions;
    }

    abstract record Instruction
    {
        static public Instruction Parse(Match match) => match switch
        {
            _ when match.Groups["dont"].Success => new Dont(),
            _ when match.Groups["do"].Success => new Do(),
            _ => new Multiply(int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value))
        };
    }
    record Do : Instruction;
    record Dont : Instruction;
    record Multiply(int X, int Y) : Instruction
    {
        public int Result => X * Y;
    }
}
