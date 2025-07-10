from typing import Any
from tools.point import Point, Directions


INPUT_FILE = "inputs/02.txt"

DIR_IDX = {"U": 0, "R": 1, "D": 2, "L": 3}

KEYPAD_1 = [
    ["1", "2", "3"],
    ["4", "5", "6"],
    ["7", "8", "9"]
]

KEYPAD_2 = [
    [None, None, "1", None, None],
    [None, "2", "3", "4", None],
    ["5", "6", "7", "8", "9"],
    [None, "A", "B", "C", None],
    [None, None, "D", None, None],
]


def run() -> None:
    instructions = read_instructions()

    part1 = get_code(instructions, KEYPAD_1)
    part2 = get_code(instructions, KEYPAD_2)

    print("** Day 2 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def read_instructions() -> list[str]:
    with open(INPUT_FILE) as file:
        return [line.strip() for line in file]


def get_code(instructions: list[str], keypad: list[list[Any]]) -> str:
    code = ""
    pos = get_start_position(keypad)

    for instruction in instructions:
        for dir in instruction:
            new_pos = pos + Directions.WITHOUT_D[DIR_IDX[dir]]

            if is_in_bounds(keypad, new_pos) and keypad[new_pos.y][new_pos.x] is not None:
                pos = new_pos

        code += keypad[pos.y][pos.x]
    return code


def get_start_position(arr: list[list[Any]]) -> Point:
    for y in range(0, len(arr)):
        for x in range(0, len(arr[0])):
            if arr[y][x] == "5":
                return Point(x, y)


def is_in_bounds(arr: list[list[Any]], point: Point) -> bool:
    return 0 <= point.y < len(arr) and 0 <= point.x < len(arr[0])
