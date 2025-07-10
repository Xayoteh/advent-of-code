from collections import Counter


INPUT_FILE = "inputs/06.txt"


def run() -> None:
    signal = read_message_signal()
    letters_count = [Counter(col) for col in zip(*signal)]

    part1 = "".join(c.most_common()[0][0] for c in letters_count)
    part2 = "".join(c.most_common()[-1][0] for c in letters_count)

    print("** Day 6 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def read_message_signal() -> list[str]:
    with open(INPUT_FILE) as file:
        return [line.strip() for line in file]
