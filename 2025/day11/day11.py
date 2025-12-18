from utils import getLines
from collections import defaultdict
from functools import cache


def dfs(start, stop, connections):
    if start == stop:
        return 1
    result = 0
    for connection in connections[start]:
        result += dfs(connection, stop, connections)
    return result


def part1(input="input.txt"):
    lines = getLines(__file__, input)
    connections = defaultdict(list)
    for line in lines:
        input, rest = line.split(":")
        outputs = rest.split()
        connections[input].extend(outputs)
    result = dfs("you", "out", connections)
    return result


def part2(input="input.txt"):
    lines = getLines(__file__, input)
    connections = defaultdict(list)
    for line in lines:
        input, rest = line.split(":")
        outputs = rest.split()
        connections[input].extend(outputs)

    @cache
    def dfs(start, bits):
        if start == "out":
            return 1 if bits == 3 else 0
        if start == "dac":
            bits |= 2
        if start == "fft":
            bits |= 1
        result = 0
        for connection in connections[start]:
            result += dfs(connection, bits)
        return result

    result = dfs("svr", 0)
    return result


def test_part1_example():
    result = part1("example.txt")
    assert result == 5


def test_part1():
    result = part1()
    assert result == 428


def test_part2_example():
    result = part2("example2.txt")
    assert result == 2


def test_part2():
    result = part2()
    assert result == 331468292364745
