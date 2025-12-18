from utils import getLines
from collections import defaultdict


def dfs(connections, start, stop):
    if start == stop:
        return 1
    result = 0
    for connection in connections[start]:
        result += dfs(connections, connection, stop)
    return result


def part1(input="input.txt"):
    lines = getLines(__file__, input)
    connections = defaultdict(list)
    for line in lines:
        input, rest = line.split(":")
        outputs = rest.split()
        connections[input].extend(outputs)
    result = dfs(connections, "you", "out")
    return result


def test_part1_example():
    result = part1("example.txt")
    assert result == 5


def test_part1():
    result = part1()
    assert result == 428
