from hashlib import md5
from tools.point import Point, Directions

GRID_SIZE = 4


def run():
    passcode = "qtetzkpl"

    paths = sorted(get_paths(passcode), key=len)

    part1 = paths[0]
    part2 = len(paths[-1])

    print("** Day 17 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def get_paths(passcode: str):
    DIRS = [Directions.UP, Directions.DOWN, Directions.LEFT, Directions.RIGHT]
    paths = []

    def find_paths(curr_path = "", curr_room = Point(0, 0)):
        if curr_room == Point(GRID_SIZE - 1, GRID_SIZE - 1):
            paths.append(curr_path)
            return

        door_states = ["b" <= c <= "f" for c in md5(
            (passcode + curr_path).encode()).hexdigest()[:4]]

        for i in range(len(DIRS)):
            next_room = curr_room + DIRS[i]

            if is_in_bounds(next_room) and door_states[i]:
                find_paths(curr_path + "UDLR"[i], next_room)

    find_paths()

    return paths


def is_in_bounds(point: Point):
    return 0 <= point.x < GRID_SIZE and 0 <= point.y < GRID_SIZE
