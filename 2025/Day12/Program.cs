/*
Solution to Advent of Code 2025 - Day 12
https://adventofcode.com/2025/day/12
*/

const string INPUT_PATH = "../input.txt";

var (presents, trees) = ReadInput(INPUT_PATH);
int part1 = trees.Count(IsValidTree);

Console.WriteLine("** Day 12 **");
Console.WriteLine($"Part 1: {part1}");

// ??????
bool IsValidTree(Tree tree)
{
    int area = tree.Width * tree.Length;
    int presentsArea = 9 * tree.PresentsNeeded.Sum();

    return area >= presentsArea;
}


(List<int>, List<Tree>) ReadInput(string path)
{
    var lines = File.ReadAllLines(path);

    int numberOfPresents = lines[^1].Split(": ")[1].Split().Length;
    int lineIndex = 0;
    List<int> presents = [];

    while(presents.Count < numberOfPresents)
    {
        lineIndex++; //Ignore 1 line

        int presentSize = 0;

        while(true)
        {
            string line = lines[lineIndex++];

            if(string.IsNullOrWhiteSpace(line))
                break;

            presentSize += line.Count(c => c == '#');
        }

        presents.Add(presentSize);
    }

    List<Tree> trees = [];

    while(lineIndex < lines.Length)
    {
        string line = lines[lineIndex++];
        var parts = line.Split(": ");

        var sizeParts = parts[0].Split('x');
        int width = int.Parse(sizeParts[0]);
        int length = int.Parse(sizeParts[1]);

        List<int> presentsNeeded = parts[1].Split().Select(int.Parse).ToList();

        trees.Add(new(width, length, presentsNeeded));
    }

    return (presents, trees);
}


record Tree(int Width, int Length, List<int> PresentsNeeded);