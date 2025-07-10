from collections import deque
from tools.point import Point, Directions

INPUT_FILE = "inputs/24.txt"


def run():
    start, points, targets = read_input()

    part1 = find_shortest_path(start, points, targets)
    part2 = find_shortest_path(start, points, targets, True)

    print("** Day 24 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def find_shortest_path(start0, points, targets, to_start = False):
    shortest_len = [float("inf")]

    def shortest_path_from(start: Point, points: set[Point], targets: set[Point], to_start: bool, curr_len: int = 0):
        if not targets:
            if to_start:
                targets = {start0}
                to_start = False
            else:
                shortest_len[0] = min(shortest_len[0], curr_len)
                return 
        
        q = deque([(start, curr_len)])
        seen = set()

        while q:
            curr_point, steps = q.popleft()

            if curr_point in seen or steps > shortest_len[0]:
                continue

            seen.add(curr_point)

            if curr_point in targets:
                shortest_path_from(curr_point, points, targets - {curr_point}, to_start, steps)

            for dir in Directions.WITHOUT_D:
                next_point = curr_point + dir

                if next_point in points:
                    q.append((next_point, steps + 1))


    shortest_path_from(start0, points, targets, to_start)    

    return shortest_len[0]



def read_input():
    with open(INPUT_FILE) as file:
        start = None
        targets = set()
        points = set()
        y = 0

        for line in file:
            x = 0

            for char in line.strip():
                if char != '#':
                    point = Point(x, y)

                    points.add(point)

                    if char == '0':
                        start = point

                    if '1' <= char <= '9':
                        targets.add(point)
                x += 1
            y += 1

        return start, points, targets
