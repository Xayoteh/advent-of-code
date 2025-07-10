# INPUT_FILE = "inputs/11.txt"


def run() -> None:
    floors = [8, 2, 0, 0]
    real_floors = [floors[0] + 4] + floors[1:]

    part1 = get_moves(floors)
    part2 = get_moves(real_floors)

    print("** Day 11 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

# TODO: do it by myself
def get_moves(items: list[int]) -> int:
    moves = 0

    while items[-1] != sum(items):
        curr_floor = 0

        while items[curr_floor] == 0:
            curr_floor += 1

        moves += 2 * (items[curr_floor] - 1) - 1
        items[curr_floor + 1] += items[curr_floor]
        items[curr_floor] = 0

    return moves
