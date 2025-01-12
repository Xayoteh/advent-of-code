using AdventOfCode2024.Tools;

namespace AdventOfCode2024.Puzzles;

static class Day13
{
    const string Filename = "Puzzles/13/input.txt";
    const int CostA = 3;
    const int CostB = 1;

    public static void Run()
    {
        List<Machine> machines = GetMachines();
        var offset = 10_000_000_000_000;

        var part1 = machines.Sum(machine => machine.GetMinimumCost());
        var part2 = machines.Sum(machine => machine.GetMinimumCost(offset));

        Console.WriteLine("** Day 13 **");
        Console.WriteLine($"Part 1: {part1}");
        Console.WriteLine($"Part 2: {part2}");
    }

    static List<Machine> GetMachines()
    {
        List<Machine> machines = [];

        foreach (string machineData in File.ReadAllText(Filename).Split("\n\n"))
        {
            machines.Add(Machine.Parse(machineData));
        }

        return machines;
    }

    record Machine((Point a, Point b) Buttons, Point Prize)
    {
        public static Machine Parse(string rawData)
        {
            string[] machineData = rawData.Split('\n');

            string[] buttonAData = machineData[0].Split(": ")[1].Split(", ");
            string[] buttonBData = machineData[1].Split(": ")[1].Split(", ");
            string[] prizeData = machineData[2].Split(": ")[1].Split(", ");

            Point buttonA = new(int.Parse(buttonAData[0][2..]), int.Parse(buttonAData[1][2..]));
            Point buttonB = new(int.Parse(buttonBData[0][2..]), int.Parse(buttonBData[1][2..]));
            Point prize = new(int.Parse(prizeData[0][2..]), int.Parse(prizeData[1][2..]));

            return new((buttonA, buttonB), prize);
        }

        public long GetMinimumCost(long offset = 0)
        {
            /*  
                Equation system:
                x * buttonA.X + y * buttonB.X = prize.X
                x * buttonA.Y + y * buttonB.Y = prize.Y

                Solve by elimination
            */
            double x1 = Buttons.a.X, y1 = Buttons.b.X, z1 = Prize.X + offset;
            double x2 = Buttons.a.Y, y2 = Buttons.b.Y, z2 = Prize.Y + offset;
            double x = (y2 * z1 / y1 - z2) / (y2 * x1 / y1 - x2);
            double y = (z1 - x1 * x) / y1;

            var pushesToA = (long)Math.Round(x);
            var pushesToB = (long)Math.Round(y);

            long totalCostA = pushesToA * CostA;
            long totalCostB = pushesToB * CostB;

            double finalX = pushesToA * Buttons.a.X + pushesToB * Buttons.b.X;
            double finalY = pushesToA * Buttons.a.Y + pushesToB * Buttons.b.Y;

            // if the final position (after rounding)
            // is equal to the prize position, then return the total cost
            // else it's not winnable so return 0
            return (finalX, finalY) == (z1, z2) ? totalCostA + totalCostB : 0;
        }
    }
}
