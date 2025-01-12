using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day17
{
    const string Filename = "Puzzles/17/input.txt";

    public static void Run()
    {
        (Dictionary<char, long> registers, List<int> program) = ReadData();

        string part1 = program.Run(registers).Join(',');
        long part2 = program.GetLowestANeeded();

        Console.WriteLine("** Day 17 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static (Dictionary<char, long>, List<int>) ReadData()
    {
        using var file = File.OpenText(Filename);

        // Registers A, B, C
        Dictionary<char, long> registers = [];
        List<int> program = [];
        var register = 'A';

        while (file.ReadLine() is string line && !string.IsNullOrEmpty(line))
        {
            var value = int.Parse(line.Split(':')[1]);

            registers[register++] = value;
        }

        while (file.ReadLine() is string line)
        {
            program.AddRange(line.Split(':')[1].Split(',').Select(int.Parse));
        }

        return (registers, program);
    }

    static long ComboValue(this int operand, Dictionary<char, long> registers) => operand switch
    {
        >= 0 and <= 3 => operand,
        4 => registers['A'],
        5 => registers['B'],
        6 => registers['C'],
        _ => throw new InvalidDataException($"Invalid operande: {operand}")
    };

    static void ADV(int operand, Dictionary<char, long> registers) =>
        registers['A'] = registers['A'] >> (int)operand.ComboValue(registers);

    static void BXL(int operand, Dictionary<char, long> registers) =>
        registers['B'] ^= operand;

    static void BST(int operand, Dictionary<char, long> registers) =>
        registers['B'] = operand.ComboValue(registers) % 8;

    static bool JNZ(int operand, Dictionary<char, long> registers) =>
        registers['A'] is not 0;

    static void BXC(Dictionary<char, long> registers) =>
        registers['B'] ^= registers['C'];

    static int OUT(int operand, Dictionary<char, long> registers) =>
        (int)(operand.ComboValue(registers) & 7);

    static void BDV(int operand, Dictionary<char, long> registers) =>
        registers['B'] = registers['A'] >> (int)operand.ComboValue(registers);

    static void CDV(int operand, Dictionary<char, long> registers) =>
        registers['C'] = registers['A'] >> (int)operand.ComboValue(registers);

    static List<int> Run(this List<int> program, Dictionary<char, long> registers)
    {
        List<int> output = [];

        for (var instructionPointer = 0; instructionPointer < program.Count; instructionPointer += 2)
        {
            var opCode = (OpCode)program[instructionPointer];
            int operand = program[instructionPointer + 1];

            switch (opCode)
            {
                case OpCode.ADV:
                    ADV(operand, registers);
                    break;
                case OpCode.BXL:
                    BXL(operand, registers);
                    break;
                case OpCode.BST:
                    BST(operand, registers);
                    break;
                case OpCode.JNZ:
                    if(JNZ(operand, registers)) instructionPointer = operand - 2;
                    break;
                case OpCode.BXC:
                    BXC(registers);
                    break;
                case OpCode.OUT:
                    output.Add(OUT(operand, registers));
                    break;
                case OpCode.BDV:
                    BDV(operand, registers);
                    break;
                case OpCode.CDV:
                    CDV(operand, registers);
                    break;
            }
        }

        return output;
    }

    static long GetLowestANeeded(this List<int> program)
    {
        Stack<(int, long)> st = [];
        var minimumA = long.MaxValue;

        st.Push((program.Count - 1, 0));

        while (st.Count > 0)
        {
            (int currentIndex, long currentA) = st.Pop();

            if (currentIndex is -1)
            {
                minimumA = Math.Min(minimumA, currentA);
                continue;
            }

            for (var remainder = 0; remainder < 8; remainder++)
            {
                long nextA = currentA * 8 + remainder;
                List<int> output = Run(program, InitializeRegisters(nextA));

                // Try to build the output from the end
                if (program.EndsWith(output))
                {
                    st.Push((currentIndex - 1, nextA));
                }
            }
        }

        return minimumA;
    }

    static bool EndsWith(this List<int> a, List<int> b)
    {
        if (b.Count > a.Count) return false;

        for (var i = 0; i < b.Count; i++)
        {
            if (b[^(i + 1)] != a[^(i + 1)]) return false;
        }

        return true;
    }

    static Dictionary<char, long> InitializeRegisters(long aValue) =>
        new(){ { 'A', aValue }, { 'B', 0 }, { 'C', 0 } };

    enum OpCode
    {
        ADV,
        BXL,
        BST,
        JNZ,
        BXC,
        OUT,
        BDV,
        CDV
    }
}
