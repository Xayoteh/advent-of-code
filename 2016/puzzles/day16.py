# INPUT_FILE = "inputs/16.txt"

def run():
    input_data = "01111010110010011"

    part1 = get_checksum(fill(input_data, 272))
    part2 = get_checksum(fill(input_data, 35651584))

    print("** Day 16 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")

def get_checksum(data: str):
    t_data = data
    chksum = []

    while len(chksum) % 2 == 0:
        chksum = []

        for i in range(0, len(t_data) - 1, 2):
            chksum.append("1" if t_data[i] == t_data[i + 1] else "0")

        t_data = chksum

    return "".join(chksum)

def fill(data: str, length: int):
    d = data

    while len(d) < length:
        d = d + "0" + "".join(["0" if c == "1" else "1" for c in d][::-1])

    return d[:length]
