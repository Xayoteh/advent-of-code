INPUT_FILE = "inputs/18.txt"

def run():
    first_row = read_input()

    part1 = count_safe_tiles(first_row, 40)
    part2 = count_safe_tiles(first_row, 400000)

    print("** Day 18 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def count_safe_tiles(first_row: str, rows: int) -> int:
    row_len = len(first_row)
    safe_tiles = sum(tile == '.' for tile in first_row)
    row_count = 1
    prev_row = first_row

    while row_count < rows:
        row = ''.join(['^' if is_trap(i, prev_row) else '.' for i in range(row_len)])
        safe_tiles += sum(tile == '.' for tile in row)
        prev_row = row
        row_count += 1

    return safe_tiles


def is_trap(idx: int, prev_row: str) -> bool:
    left = idx > 0 and prev_row[idx - 1] == '^'
    center = prev_row[idx] == '^'
    right = idx < len(prev_row) - 1 and prev_row[idx + 1] == '^'

    if left and center and not right: return True
    if center and right and not left: return True
    if left and not (center or right): return True
    if right and not (center or left): return True

    return False


def read_input():
    with open(INPUT_FILE) as file:
        return file.readline().strip()