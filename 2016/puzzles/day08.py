INPUT_FILE = "inputs/08.txt"


def run() -> None:
    screen_width, screen_height = 50, 6
    screen = [[False for _ in range(0, screen_width)]
              for _ in range(0, screen_height)]

    for op in read_operations():
        execute(op, screen)

    part1 = sum(sum(row) for row in screen)

    print("** Day 8 **")
    print(f"Part 1: {part1}")
    print(f"Part 2:")
    printscr(screen)


def read_operations() -> list[str]:
    with open(INPUT_FILE) as file:
        return [line[:-1] for line in file]


def execute(line: str, screen: list[list[bool]]) -> None:
    tokens = line.split()

    if tokens[0] == "rect":
        x, y = [int(t) for t in tokens[1].split('x')]
        fill_rect(x, y, screen)
        return

    idx = int(tokens[2].split('=')[1])
    offset = int(tokens[-1])

    if tokens[1] == "row":
        rotate_row(idx, offset, screen)
    else:
        rotate_col(idx, offset, screen)


def fill_rect(x: int, y: int, screen: list[list[bool]]) -> None:
    for i in range(0, x):
        for j in range(0, y):
            screen[j][i] = True


def rotate_col(col: int, offset: int, screen: list[list[bool]]) -> None:
    n_col = [False] * len(screen)

    for i in range(0, len(screen)):
        ni = (i + offset) % len(screen)
        n_col[ni] = screen[i][col]

    for i in range(0, len(screen)):
        screen[i][col] = n_col[i]


def rotate_row(row: int, offset: int, screen: list[list[bool]]):
    n_row = [False] * len(screen[0])

    for i in range(0, len(screen[0])):
        ni = (i + offset) % len(screen[0])
        n_row[ni] = screen[row][i]

    screen[row] = n_row


def printscr(scr: list[list[bool]]) -> None:
    for row in scr:
        for x in row:
            print('#' if x else '.', end="")
        print()
    print()
