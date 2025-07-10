from collections import defaultdict as ddict, deque

INPUT_FILE = "inputs/10.txt"

def run() -> None:
    targets = set([61, 17])
    bots, instructions = read_instructions()
    
    ans, outputs = find(targets, bots, instructions)

    part1 = ans
    part2 = outputs["0"][0] * outputs["1"][0] * outputs["2"][0]

    print("** Day 10 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def read_instructions() -> tuple[ddict[str, list[int]], deque[list]]:
    with open(INPUT_FILE) as file:
        bots = ddict(list[int])
        instructions = deque()

        for line in file:
            tokens = line.strip().split()

            if tokens[0] == "value":
                bots[tokens[-1]].append(int(tokens[1]))
            elif tokens[0] == "bot":
                instructions.append([tokens[1], tokens[5] == "bot", tokens[6], tokens[-2] == "bot", tokens[-1]])

        return bots, instructions
    
def find(targets: set, bots: ddict[str, list[int]], instructions: deque[list[str]]) -> tuple[str, ddict[str, list[int]]]:
    outputs = ddict(list[int]) 
    ans = "-1"

    while instructions:
        ins = instructions.popleft()
        bot, l_is_bot, l_out, h_is_bot, h_out = ins

        if len(bots[bot]) < 2:
            instructions.append(ins)
        else:
            if ans == "-1" and targets.issubset(bots[bot]):
                ans = bot
            
            l_val, h_val = min(bots[bot]), max(bots[bot])
            bots[bot] = []

            bots[l_out].append(l_val) if l_is_bot else outputs[l_out].append(l_val)
            bots[h_out].append(h_val) if h_is_bot else outputs[h_out].append(h_val)
        
    return ans, outputs


