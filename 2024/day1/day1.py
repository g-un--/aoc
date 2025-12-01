from utils import getLines
from collections import Counter

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    items = [line.split() for line in lines]
    lists = list(zip(*items))
    first = sorted([int(item) for item in lists[0]])
    second = sorted([int(item) for item in lists[1]])
    distances = [abs(a-b) for a,b in zip(first, second)]
    return sum(distances)

def part2(input="input.txt"):
    lines = getLines(__file__, input)
    items = [line.split() for line in lines]
    lists = list(zip(*items))
    counter = Counter(lists[1])
    similarities = [int(item) * counter[item] for item in lists[0]]
    return sum(similarities)

def test_part1_example():
    result = part1("example.txt")
    assert result == 11


def test_part2_example():
    result = part2("example.txt")
    assert result == 31

def test_part1():
    result = part1()
    assert result == 1830467


def test_part2():
    result = part2()
    assert result == 26674158
