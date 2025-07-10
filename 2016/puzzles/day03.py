INPUT_FILE = "inputs/03.txt"


def run() -> None:
    sides_list = read_triangles_sides()
    real_sides_list = get_real_triangle_sides(sides_list)

    part1 = count_valid_triangles(sides_list)
    part2 = count_valid_triangles(real_sides_list)

    print("** Day 3 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def read_triangles_sides() -> list[list[int]]:
    with open(INPUT_FILE) as file:
        return [[int(x) for x in line.split()] for line in file]


def get_real_triangle_sides(side_list: list[list[int]]) -> list[list[int]]:
    # for line in zip(*side_list):
    #     for i in range(0, len(line) - 3, 3):
    #         real_sides_list.append(line[i:i+3])
    # return real_sides_list

    return [line[i:i+3] for line in zip(*side_list) for i in range(0, len(line) - 3, 3)]


def count_valid_triangles(sides_list: list[list[int]]) -> list[list[int]]:
    # valid_triangles = [sides for sides in sides_list if is_valid_triangle(sides)]

    # for sides in sides_list:
    #     if is_valid_triangle(sides):
    #         valid_triangles.append(sides)

    return len([sides for sides in sides_list if is_valid_triangle(sides)])


def is_valid_triangle(sides: list[int]) -> bool:
    return sides[0] + sides[1] > sides[2] \
        and sides[1] + sides[2] > sides[0] \
        and sides[0] + sides[2] > sides[1]
