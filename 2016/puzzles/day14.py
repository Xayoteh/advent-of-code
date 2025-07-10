from hashlib import md5

KEYS_NEEDED = 64


def run() -> None:
    salt = "qzyelonm"

    part1 = get_keys(salt)[-1]
    part2 = get_keys(salt, True)[-1]

    print("** Day 14 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def get_keys(salt: str, stretch: bool = False) -> list:
    memo = {}
    keys = []
    idx = 0

    while len(keys) < KEYS_NEEDED:
        cand = hash(salt, idx, memo, stretch)

        if is_key(cand, salt, idx, memo, stretch):
            keys.append(idx)
            print(len(keys),idx)

        idx += 1

    return keys


def is_key(cand: str, salt, idx, memo, stretch) -> bool:
    trplt, chr = get_repeated_char(cand)

    if not trplt:
        return False

    for i in range(1, 1001):
        n = hash(salt, idx + i, memo, stretch)

        if chr * 5 in n:
            return True
    
    return False


def get_repeated_char(hash: str) -> tuple[bool, str]:
    for i in range(0, len(hash) - 2):
        if all(x == hash[i] for x in hash[i:i+3]):
            return True, hash[i]

    return False, ""


def hash(salt: str, idx: int, memo: dict, stretch: bool = False) -> str:
    if idx not in memo:
        h = md5(f"{salt}{idx}".encode()).hexdigest()

        for i in range(0, 2016 if stretch else 0):
                h = md5(h.encode()).hexdigest()

        memo[idx] = h

    return memo[idx]
