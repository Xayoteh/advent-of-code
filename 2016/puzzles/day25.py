from itertools import count

INPUT_FILE = "inputs/25.txt"

def run():
    ins = read_instructions()
    part1 = find_lowest(ins)

    print("** Day 25 **")
    print(f"Part 1: {part1}")

def find_lowest(ins: list[list[str]]):
    for i in count(0):
        if execute([x for x in ins], i):
            return i

def execute(ins: list[list[str]], a: int) -> int:
    regs = {x: 0 for x in 'abcd'}
    regs['a'] = a
    i, last, steps = 0, 1, 0
    while i < len(ins):
        curr_ins = ins[i]

        if curr_ins[0] == "cpy":
            cpy(curr_ins[1], curr_ins[2], regs)
        elif curr_ins[0] == "inc":
            inc(curr_ins[1], regs)
        elif curr_ins[0] == "dec":
            dec(curr_ins[1], regs)
        elif curr_ins[0] == "jnz":
            i += jnz(curr_ins[1], curr_ins[2], regs) - 1
        elif curr_ins[0] == "out":
            val = regs[curr_ins[1]] if curr_ins[1] in regs else int(curr_ins[1])

            if val == last:
                return False
            
            last = val
            steps += 1
            
            # The problem states we need to "send the stars"
            # there are max 50 stars so...
            if steps == 50:
                break
        else:
            off = int(regs[curr_ins[1]])
            idx = i + off

            if idx < len(ins):
                og = ins[idx].copy()

                if len(ins[idx]) == 2:
                    ins[idx][0] = "dec" if ins[idx][0] == "inc" else "inc"
                else:
                    ins[idx][0] = "cpy" if ins[idx][0] == "jnz" else "jnz"

        i += 1

    return a


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