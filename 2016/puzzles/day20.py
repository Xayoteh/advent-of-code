INPUT_FILE = "inputs/20.txt"

def run():
    ranges = read_input()
    merged = merge_ranges(ranges)

    part1 = merged[0][1] + 1
    part2 = get_allowed_ips(merged)

    print("** Day 20 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def get_allowed_ips(ranges: list[list[int]]):
    allowed = 0

    prev_h = ranges[0][1]

    for l, h in ranges[1:]:
        allowed += l - prev_h - 1
        prev_h = h

    return allowed

def merge_ranges(ranges: list[list[int]]):
    ranges.sort()
    merged = []
    cl, ch = ranges[0]

    for l, h in ranges:
        if l > ch + 1:
            merged.append([cl, ch])
            cl, ch = l, h
            continue

        ch = max(h, ch)

    merged.append([cl, ch])

    return merged


def read_input():
    with open(INPUT_FILE) as file:
        return [[int(x) for x in line.strip().split('-')] for line in file]