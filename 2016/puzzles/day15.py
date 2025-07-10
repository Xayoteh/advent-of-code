INPUT_FILE = "inputs/15.txt"

def run():
    discs_info = read_discs_info()
    new_disc = [(11, 0)]

    part1 = get_time_to_press(discs_info)
    part2 = get_time_to_press(discs_info + new_disc)

    print("** Day 15 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def read_discs_info() -> list[tuple[int, int]]:
    with open(INPUT_FILE) as file:
        discs_info = []

        for line in file:
            info = line.split(' ')
            discs_info.append((int(info[3]),int(info[-1][:-2])))

        return discs_info
    
def get_time_to_press(discs_info: list[tuple[int, int]]) -> int:
    i = 0

    while not can_press(discs_info, i):
        i += 1

    return i

def can_press(discs_info: list[tuple[int, int]], time: int) -> bool:
    pos = []

    for i in range(len(discs_info)):
        p, s = discs_info[i]
        pos.append((s + time + i + 1) % p)

    return all(x == pos[0] for x in pos)
