INPUT_FILE = "inputs/07.txt"


def run() -> None:
    ip_addresses = read_ip_addresses()

    tls_ips = [ip for ip in ip_addresses if supports_tls(ip)]
    ssl_ips = [ip for ip in ip_addresses if supports_ssl(ip)]

    part1 = len(tls_ips)
    part2 = len(ssl_ips)

    print("** Day 7 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def read_ip_addresses() -> list[str]:
    with open(INPUT_FILE) as file:
        return [line.strip() for line in file]


def supports_tls(ip: str) -> bool:
    abba = False
    in_hypernet = False
    for i in range(len(ip) - 3):
        if ip[i] in ['[', ']']:
            in_hypernet = ip[i] == '['
            continue

        is_abba = ip[i] != ip[i + 1] and ip[i] == ip[i + 3] and ip[i + 1] == ip[i + 2]
        if is_abba and in_hypernet:
            return False
        
        abba = abba or is_abba

    return abba

def supports_ssl(ip: str) -> bool:
    aba, bab = [], []
    in_hypernet = False
    for i in range(len(ip) - 2):
        if ip[i] in ['[', ']']:
            in_hypernet = ip[i] == '['
            continue

        if ip[i] != ip[i + 2]:
            continue

        bab.append(ip[i:i+3]) if in_hypernet else aba.append(ip[i:i+3])

    return any((x[1] + x[0] + x[1]) in bab for x in aba) 

