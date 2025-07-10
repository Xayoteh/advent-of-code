INPUT_FILE = "inputs/09.txt"

def run() -> None:
    input_data = read_input()

    part1 = decompressed_len(input_data)
    part2 = decompressed_len(input_data, True)

    print("** Day 9 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def read_input() -> str:
    with open(INPUT_FILE) as file:
        return "".join(file).strip()
    
def decompressed_len(data: str, v2: bool = False) -> int:

    if '(' not in data:
        return len(data)
    
    length = 0

    while '(' in data:
        idx = data.find('(')
        length += idx
        data = data[idx:]

        idx = data.find(')')
        ch_count, rep = [int(x) for x in data[1:idx].split('x')]
        data = data[idx + 1:]

        section = data[:ch_count]
        if v2:
            length += decompressed_len(section, True) * rep
        else:
            length += len(section) * rep

        data = data[ch_count:]

    length += len(data)
    return length