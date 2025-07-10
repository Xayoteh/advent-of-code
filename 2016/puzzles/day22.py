import re
from collections import deque
from tools.point import Point, Directions

INPUT_FILE = "inputs/22.txt"

# Solved making some assumptions
# 1 - An empty node always exist (and only one)
# 2 - The shortest path will always be moving the target data only to the left

def run():
    nodes = read_storage()

    empty_node = find_empty_node(nodes)

    part1 = count_pairs(nodes)
    part2 = count_steps(nodes, empty_node)

    print("** Day 22 **")
    print(f"Part 1: {part1}")
    print(f"Part 2: {part2}")


def count_steps(nodes: dict[Point, dict[str, int]], empty_node: Point):
    steps = 0
    target = Point(max([node.x for node in nodes.keys()]), 0)

    # bfs
    while target != Point(0, 0):
        seen = set()
        q = deque([(empty_node, 0)])

        while q:
            curr_node, curr_steps = q.popleft()

            if curr_node == target + Directions.LEFT:
                steps += curr_steps
                break

            if curr_node == target or curr_node in seen:
                continue

            seen.add(curr_node)

            for dir in Directions.WITHOUT_D:
                next = curr_node + dir

                if next in nodes and nodes[next]['used'] <= nodes[curr_node]['size']:
                    q.append((next, curr_steps + 1))

        empty_node = target.copy()
        target += Directions.LEFT
        steps += 1

    return steps


def find_empty_node(nodes: dict[Point, dict[str, int]]):
    for node, data in nodes.items():
        if data['used'] == 0:
            return node


def count_pairs(nodes: dict[Point, dict[str, int]]):
    seen = set()
    count = 0
    
    for nodeA, dataA in nodes.items():
        for nodeB, dataB in nodes.items():
            if nodeB in seen:
                continue

            if dataA['used'] != 0 and dataA['used'] <= dataB['avail']:
                count += 1
            elif dataB['used'] != 0 and dataB['used'] <= dataA['avail']:
                count += 1

        seen.add(nodeA)

    return count


def read_storage():
    with open(INPUT_FILE) as file:
        nodes = {}

        for line in file:
            if line[0] != '/':
                continue

            tokens = re.sub('\\s+', " ", line.strip()).split(' ')
            node = tokens[0].split('-')[-2:]

            x, y = int(node[0][1:]), int(node[1][1:])
            size = int(tokens[1][:-1])
            used = int(tokens[2][:-1])
            avail = int(tokens[3][:-1])

            nodes[Point(x, y)] = {'size': size, 'used': used, 'avail': avail}

        return nodes
