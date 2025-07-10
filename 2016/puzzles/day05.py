from hashlib import md5

PASS_LEN = 8
PREFIX = "0" * 5


def run() -> None:
    door_id = "abbhdwsy"

    part1 = decrypt_password(door_id)
    part2 = decrypt_password_v2(door_id)

    print("** Day 5 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def decrypt_password(door_id: str) -> str:
    password = []
    n = 0
    idx = len(PREFIX)

    while len(password) < PASS_LEN:
        data = door_id + str(n)
        hash = md5(data.encode()).hexdigest()

        if hash.startswith(PREFIX):
            password.append(hash[idx])

        n += 1

    return "".join(password)


def decrypt_password_v2(door_id: str) -> str:
    password = {}
    idx = len(PREFIX)
    n = 0

    while len(password) < 8:
        data = door_id + str(n)
        hash = md5(data.encode()).hexdigest()

        if hash.startswith(PREFIX) and hash[idx] in "01234567" and hash[idx] not in password:
            password[hash[idx]] = hash[idx + 1]

        n += 1

    return "".join(v for k, v in sorted(password.items()))
