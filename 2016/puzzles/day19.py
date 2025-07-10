from collections import deque

def run():
    elf_count = 3004953
    part1 = solve(elf_count)
    part2 = solve_2(elf_count)

    print("** Day 19 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def solve(elfs: int) -> int:
    gifts = deque([i for i in range(elfs)])

    while len(gifts) > 1:
        gifts.append(gifts.popleft())
        gifts.popleft()

    return gifts[0] + 1

# TODO: understand this
def solve_2(elfs: int) -> int:
    w = 1

    for i in range(1, elfs):
        w = w % i + 1
        if w > (i + 1)/2:
            w += 1
    return w