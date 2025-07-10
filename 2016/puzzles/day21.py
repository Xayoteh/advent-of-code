INPUT_FILE = "inputs/21.txt"


def run():
    instructions = read_instructions()

    part1 = scramble("abcdefgh", instructions)
    part2 = unscramble("fbgdceah", instructions)

    print("** Day 21 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def swap_positions(pw: list[str], x: int, y: int):
    pw[x], pw[y] = pw[y], pw[x]
    return pw


def swap_letters(pw: list[str], x: str, y: str):
    for i in range(len(pw)):
        if pw[i] == x:
            pw[i] = y
        elif pw[i] == y:
            pw[i] = x
    return pw


def rotate_steps(pw: list[str], right: bool, steps: int):
    steps = steps % len(pw)

    if right:
        steps = -steps

    return pw[steps:] + pw[:steps]


def rotate_position_reverse(pw: list[str], letter: str):
    idx = pw.index(letter)
    table = {1: 1, 3: 2, 5: 3, 7: 4, 2: 6, 4: 7, 6: 0, 0: 1}
    return rotate_steps(pw, False, table[idx])


def rotate_position(pw: list[str], letter: str):
    idx = pw.index(letter)
    steps = 1 + idx + (1 if idx >= 4 else 0)
    return rotate_steps(pw, True, steps)


def reverse(pw: list[str], x: int, y: int):
    temp = pw[x: y + 1]
    return pw[:x] + temp[::-1] + pw[y + 1:]


def move(pw: list[str], x: int, y: int):
    x_val = pw.pop(x)
    pw.insert(y, x_val)
    return pw


def scramble(word: list[str], instructions: list[list[str]]):
    pw = list(word)

    for ins in instructions:
        if ins[0] == "swap":
            if ins[1] == "position":
                pw = swap_positions(pw, int(ins[2]), int(ins[-1]))
            else:
                pw = swap_letters(pw, ins[2], ins[-1])
        elif ins[0] == "rotate":
            if ins[-1] == "steps" or ins[-1] == "step":
                pw = rotate_steps(pw, ins[1] == "right", int(ins[-2]))
            else:
                pw = rotate_position(pw, ins[-1])
        elif ins[0] == "reverse":
            pw = reverse(pw, int(ins[-3]), int(ins[-1]))
        else:
            pw = move(pw, int(ins[-4]), int(ins[-1]))

    return ''.join(pw)


def unscramble(word: list[str], instructions: list[list[str]]):
    pw = list(word)

    for ins in instructions[::-1]:
        if ins[0] == "swap":
            if ins[1] == "position":
                pw = swap_positions(pw, int(ins[2]), int(ins[-1]))
            else:
                pw = swap_letters(pw, ins[2], ins[-1])
        elif ins[0] == "rotate":
            if ins[-1] == "steps" or ins[-1] == "step":
                pw = rotate_steps(pw, ins[1] == "left", int(ins[-2]))
            else:
                pw = rotate_position_reverse(pw, ins[-1])
        elif ins[0] == "reverse":
            pw = reverse(pw, int(ins[-3]), int(ins[-1]))
        else:
            pw = move(pw, int(ins[-1]), int(ins[-4]))

    return ''.join(pw)


def read_instructions():
    with open(INPUT_FILE) as file:
        return [line.strip().split(' ') for line in file]
