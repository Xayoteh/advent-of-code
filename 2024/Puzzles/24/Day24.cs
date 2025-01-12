using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day24
{
    const string Filename = "Puzzles/24/input.txt";

    public static void Run()
    {
        (Dictionary<string, Gate> gates, Dictionary<string, int> values) = ReadInput();

        long part1 = gates.GetOutput(values);
        string part2 = gates.FindSwappedWires();

        Console.WriteLine("** Day 24 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    /*static string PPrint(string wire, Dictionary<string, Gate> gates, int depth = 0)
    {
        string pad = new string(' ', 3 * depth);
        if ("xy".Contains(wire[0])) return $"{pad}{wire}";
        Gate g = gates[wire];
        return $"{pad}{g.Operation} ({wire})\n" 
            + PPrint(g.InputA, gates, depth + 1) + "\n"
            + PPrint(g.InputB, gates, depth + 1);
    }*/

    static (Dictionary<string, Gate>, Dictionary<string, int>) ReadInput()
    {
        using var file = File.OpenText(Filename);

        Dictionary<string, int> values = [];
        Dictionary<string, Gate> gates = [];

        while (file.ReadLine() is string input && !string.IsNullOrEmpty(input))
        {
            string[] data = input.Split(": ");

            values.Add(data[0], int.Parse(data[1]));
        }

        while (file.ReadLine() is string input && !string.IsNullOrEmpty(input))
        {
            string[] data = input.Split(' ');

            //InputA Operation InputB -> Output
            gates[data[4]] = new(data[0], data[2], data[1]);
        }

        return (gates, values);
    }

    static long GetOutput(this Dictionary<string, Gate> gates, Dictionary<string, int> values)
    {
        long output = 0;
        var bitPosition = 0;
        var wire = "z00";

        while (gates.ContainsKey(wire))
        {
            long bitValue = GetWireValue(wire, gates, values);
            long realValue = bitValue << bitPosition++;

            output += realValue;
            wire = MakeWire('z', bitPosition);
        }

        return output;
    }

    static int GetWireValue(string wire, Dictionary<string, Gate> gates, Dictionary<string, int> values) =>
        values.TryGetValue(wire, out int value) ? value
        : values[wire] = gates[wire].GetOutputValue(gates, values);

    static string FindSwappedWires(this Dictionary<string, Gate> gates)
    {
        HashSet<string> wiresChanged = [];

        for (var i = 0; i < 4; i++)
        {
            int currentScore = MaxCorrectBit(gates);

            foreach ((string wireA, string wireB) in gates.Keys.Pairs())
            {
                // Avoid changing a wire again
                if(wiresChanged.Contains(wireA) || wiresChanged.Contains(wireB)) continue;

                (gates[wireA], gates[wireB]) = (gates[wireB], gates[wireA]);               

                if(MaxCorrectBit(gates) > currentScore)
                {
                    wiresChanged.Add(wireA);
                    wiresChanged.Add(wireB);
                    break;
                }

                (gates[wireA], gates[wireB]) = (gates[wireB], gates[wireA]);
            }
        }

        return wiresChanged.Order().Join(',');
    }

    static int MaxCorrectBit(Dictionary<string, Gate> gates)
    {
        var bit = 0;

        while (VerifyBit(bit, gates)) bit++;

        return bit;
    }

    static bool VerifyBit(int number, Dictionary<string, Gate> gates) => 
        VerifyZ(MakeWire('z', number), number, gates);

    static bool VerifyZ(string wire, int number, Dictionary<string, Gate> gates)
    {
        if (!gates.TryGetValue(wire, out var gate) || gate.Operator is not "XOR") return false;
        if (number is 0) return new HashSet<string> { gate.InputA, gate.InputB }.SetEquals(["x00", "y00"]);

        return VerifyXOR(gate.InputA, number, gates) && VerifyBitCarry(gate.InputB, number, gates)
            || VerifyXOR(gate.InputB, number, gates) && VerifyBitCarry(gate.InputA, number, gates);
    }

    static bool VerifyXOR(string wire, int number, Dictionary<string, Gate> gates) =>
        gates.TryGetValue(wire, out var gate) && gate.Operator is "XOR" && 
        new HashSet<string> { gate.InputA, gate.InputB }.SetEquals([MakeWire('x', number), MakeWire('y', number)]);

    static bool VerifyBitCarry(string wire, int number, Dictionary<string, Gate> gates)
    {
        if (!gates.TryGetValue(wire, out var gate)) return false;

        if (gate.Operator is not "OR")
        {
            return number is 1 && gate.Operator is "AND" &&
                new HashSet<string> { gate.InputA, gate.InputB }.SetEquals(["x00", "y00"]);
        }

        return VerifyDirectCarry(gate.InputA, number - 1, gates) && VerifyReCarry(gate.InputB, number - 1, gates)
            || VerifyDirectCarry(gate.InputB, number - 1, gates) && VerifyReCarry(gate.InputA, number - 1, gates);
    }

    static bool VerifyDirectCarry(string wire, int number, Dictionary<string, Gate> gates) =>
        gates.TryGetValue(wire, out var gate) && gate.Operator is "AND" &&
        new HashSet<string> { gate.InputA, gate.InputB }.SetEquals([MakeWire('x', number), MakeWire('y', number)]);

    static bool VerifyReCarry(string wire, int number, Dictionary<string, Gate> gates)
    {
        if (!gates.TryGetValue(wire, out var gate) || gate.Operator is not "AND") return false;

        return VerifyXOR(gate.InputA, number, gates) && VerifyBitCarry(gate.InputB, number, gates)
            || VerifyXOR(gate.InputB, number, gates) && VerifyBitCarry(gate.InputA, number, gates);
    }

    static string MakeWire(char character, int number) => 
        character + $"{number:D2}";

    record Gate(string InputA, string InputB, string Operator)
    {
        public int GetOutputValue(Dictionary<string, Gate> gates, Dictionary<string, int> values) => Operator switch
        {
            "AND" => GetWireValue(InputA, gates, values) & GetWireValue(InputB, gates, values),
            "OR" => GetWireValue(InputA, gates, values) | GetWireValue(InputB, gates, values),
            "XOR" => GetWireValue(InputA, gates, values) ^ GetWireValue(InputB, gates, values),
            _ => throw new ArgumentException($"Wrong operator {Operator}")
        };
    };
}
