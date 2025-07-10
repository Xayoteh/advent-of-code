from collections import deque
from tools.point import Point, Directions

def run() -> None:
    input = 1352
    target = Point(31,39)

    part1 = calc_min_steps(target, input)
    part2 = get_distinct_locations(50, input)

    print("** Day 13 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def calc_min_steps(target: Point, n: int) -> int:
    seen = set()
    q = deque()
    q.append((Point(1, 1), 0))

    while q:
        p, steps = q.popleft()

        if p == target:
            return steps
        
        if p in seen:
            continue

        seen.add(p)
        
        for dir in Directions.WITHOUT_D:
            np: Point = p + dir

            if np.x < 0 or np.y < 0:
                continue

            if not is_wall(np, n):
                q.append((np, steps + 1))

def get_distinct_locations(max_steps: int, n: int):
    seen = set()
    q = deque()
    q.append((Point(1, 1), 0))

    while q:
        p, steps = q.popleft()

        if p in seen:
            continue

        seen.add(p)

        if steps == max_steps:
            continue
        
        for dir in Directions.WITHOUT_D:
            np: Point = p + dir

            if np.x < 0 or np.y < 0:
                continue

            if not is_wall(np, n):
                q.append((np, steps + 1))

    return len(seen)


def is_wall(p: Point, n: int) -> bool:
    return sum(x == '1' for x in bin( p.x **2 + 3*p.x + 2*p.x*p.y + p.y + p.y**2 + n )[2:]) % 2 == 1