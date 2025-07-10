INPUT_FILE = "inputs/12.txt"


def run() -> None:
    ins = read_instructions()

    part1 = execute(ins)
    part2 = execute(ins, 1)

    print("** Day 12 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def execute(ins: list[list[str]], c: int = 0) -> int:
    regs = {x: 0 for x in 'abcd'}
    regs['c'] = c
    i = 0

    while i < len(ins):
        curr_ins = ins[i]

        if curr_ins[0] == "cpy":
            cpy(curr_ins[1], curr_ins[2], regs)
        elif curr_ins[0] == "inc":
            inc(curr_ins[1], regs)
        elif curr_ins[0] == "dec":
            dec(curr_ins[1], regs)
        else:
            i += jnz(curr_ins[1], curr_ins[2], regs) - 1

        i += 1

    return regs['a']


def cpy(x: str, y: str, regs: dict) -> None:
    regs[y] = int(x) if x not in regs else regs[x]


def inc(x: str, regs: dict) -> None:
    regs[x] += 1


def dec(x: str, regs: dict) -> None:
    regs[x] -= 1


def jnz(x: str, y: str, regs: dict) -> None:
    x = int(x) if x not in regs else regs[x]
    return int(y) if x != 0 else 1


def read_instructions() -> list[list[str]]:
    with open(INPUT_FILE) as file:
        return [line.strip().split(" ") for line in file]
