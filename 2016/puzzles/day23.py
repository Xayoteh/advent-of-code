INPUT_FILE = "inputs/23.txt"


def run() -> None:
    ins = read_instructions()

    part1 = execute(list(ins), 7)
    # TODO: Optimize (brute force takes 2 hours)
    part2 = execute(list(ins), 12)

    print("** Day 23 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def execute(ins: list[list[str]], a: int) -> int:
    regs = {x: 0 for x in 'abcd'}
    regs['a'] = a
    i = 0
    step = 0
    # print(regs)
    while i < len(ins):
        curr_ins = ins[i]
        # print(step, i, curr_ins)
        if curr_ins[0] == "cpy":
            cpy(curr_ins[1], curr_ins[2], regs)
        elif curr_ins[0] == "inc":
            inc(curr_ins[1], regs)
        elif curr_ins[0] == "dec":
            dec(curr_ins[1], regs)
        elif curr_ins[0] == "jnz":
            i += jnz(curr_ins[1], curr_ins[2], regs) - 1
        else:
            off = int(regs[curr_ins[1]])
            idx = i + off

            if idx < len(ins):
                og = ins[idx].copy()

                if len(ins[idx]) == 2:
                    ins[idx][0] = "dec" if ins[idx][0] == "inc" else "inc"
                else:
                    ins[idx][0] = "cpy" if ins[idx][0] == "jnz" else "jnz"

                # print(f"{og} toggled to {ins[idx]}")
        
        # print(regs)
        i += 1
        step += 1

    return regs['a']


def cpy(x: str, y: str, regs: dict) -> None:
    if y in regs:
        regs[y] = int(x) if x not in regs else regs[x]


def inc(x: str, regs: dict) -> None:
    if x in regs:
        regs[x] += 1


def dec(x: str, regs: dict) -> None:
    if x in regs:
        regs[x] -= 1


def jnz(x: str, y: str, regs: dict) -> None:
    x = int(x) if x not in regs else regs[x]
    y = int(y) if y not in regs else regs[y]
    return y if x != 0 else 1


def read_instructions() -> list[list[str]]:
    with open(INPUT_FILE) as file:
        return [line.strip().split(" ") for line in file]