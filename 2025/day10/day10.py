from utils import getLines

from itertools import combinations


def find_min_pushes(buttons, targetValue):
    for i in range(1, len(buttons) + 1):
        for comb in combinations(buttons, i):
            result = 0
            for value in comb:
                result ^= value
            if result == targetValue:
                return i


def part1(input="input.txt"):
    lines = getLines(__file__, input)
    result = 0
    for line in lines:
        target, *buttons, __ = line.split()
        targetValue = int(
            target.strip("[]").replace(".", "0").replace("#", "1")[::-1], 2
        )
        buttonsValues = [
            list(map(int, button.strip("()").split(","))) for button in buttons
        ]
        buttonsSum = [sum(map(lambda x: pow(2, x), value)) for value in buttonsValues]
        minPushes = find_min_pushes(buttonsSum, targetValue)
        result += minPushes
    return result


def test_part1_example():
    result = part1("example.txt")
    assert result == 7


def test_part1():
    result = part1()
    assert result == 385
