import re
import string
from collections import Counter


class Room():
    def __init__(self, name: str = "", id: int = 0, checksum: str = ""):
        self.name = name
        self.id = id
        self.checksum = checksum

    def __str__(self):
        return f"{self.name}, {self.id}, {self.checksum}"


INPUT_FILE = "inputs/04.txt"


def run() -> None:
    rooms = read_rooms()

    valid_rooms = [room for room in rooms if is_valid(room)]
    storage_room = find_storage_room(valid_rooms)

    part1 = sum(room.id for room in valid_rooms)
    part2 = storage_room.id

    print("** Day 4 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def read_rooms() -> list[Room]:
    regex = r"([a-z-]+)(\d+)\[(\w+)\]"
    with open(INPUT_FILE) as file:
        return [Room(name, int(id), checksum) for name, id, checksum in re.findall(regex, file.read())]


def is_valid(room: Room) -> bool:
    letters = (c for c in room.name if c in string.ascii_lowercase)
    commons = sorted(Counter(letters).most_common(), key=lambda x: (-x[1], x[0]))
    ordered = "".join(c for c, _ in commons)
    return ordered.startswith(room.checksum)


def cipher(n: int) -> dict[int, int]:
    letters = string.ascii_lowercase
    x = n % len(letters)
    return str.maketrans(letters, letters[x:] + letters[:x])


def find_storage_room(rooms: list[Room]) -> Room:
    for room in rooms:
        name = room.name.translate(cipher(room.id))

        if "north" in name:
            return room
