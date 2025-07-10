from tools.point import Point, Directions


INPUT_FILE = "inputs/01.txt"


def run() -> None:
    sequence = read_sequence()
    locations = get_locations(sequence)

    destination = locations[-1]
    hq_location = get_first_revisited(locations)

    part1 = get_distance(destination)
    part2 = get_distance(hq_location)

    print("** Day 1 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def read_sequence() -> str:
    with open(INPUT_FILE) as file:
        return "".join(file)


def get_locations(sequence: str) -> list[Point]:
    pos = Point()
    points = [pos]
    dir_idx = 0

    for instruction in sequence.split(", "):
        dir_idx = (dir_idx + (1 if instruction[0] == 'R' else -1)) % 4
        
        for _ in range(0, int(instruction[1:])):
            pos += Directions.WITHOUT_D[dir_idx]
            points.append(pos.copy())

    return points


def get_first_revisited(points: list[Point]) -> Point:
    p_set = set()

    for point in points:
        if point in p_set:
            return point  # Guaranteed to exist
        p_set.add(point)


def get_distance(destination: Point) -> int:
    return abs(destination.x) + abs(destination.y)
