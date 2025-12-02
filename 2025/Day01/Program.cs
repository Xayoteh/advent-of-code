/*
Solution to Advent of Code 2025 - Day 1
https://adventofcode.com/2025/day/1
*/

const string INPUT_PATH = "../input.txt";

IEnumerable<string> rotations = ReadInput(INPUT_PATH);

int password = GetPassword(rotations);
int realPassword = GetPassword(rotations, true);

Console.WriteLine("** Day 1 **");
Console.WriteLine($"Part 1: {password}");
Console.WriteLine($"Part 2: {realPassword}");

IEnumerable<string> ReadInput(string path) =>
    File.ReadLines(path).Select(x => x.Trim());

int GetPassword(IEnumerable<string> rotations, bool useMethod2 = false)
{
    int password = 0;
    int current = 50;

    foreach (string rotation in rotations)
    {
        char direction = rotation[0];
        int distance = int.Parse(rotation[1..]);

        if(useMethod2)
            password += distance / 100;
            
        distance %= 100;

        switch (direction)
        {
            case 'L' when distance > current:
                if(useMethod2 && current != 0)
                    ++password;
                current += 99 - distance + 1;
                break;
            case 'L':
                current -= distance;
                break;
            default:
                current += distance;
                if(useMethod2 && current > 100)
                    ++password;
                current %= 100;
                break;
        }

        if(current == 0 && distance != 0)
            ++password;
    }

    return password;
}