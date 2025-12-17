from utils import getLines
from itertools import combinations
from scipy.optimize import LinearConstraint, milp
import numpy as np


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


def part2(input="input.txt"):
    lines = getLines(__file__, input)
    result = 0
    for line in lines:
        _, *buttons, joltages = line.split()
        buttonValues = [tuple(map(int, button[1:-1].split(","))) for button in buttons]
        targetValues = list(map(int, joltages[1:-1].split(",")))
        c = np.ones(len(buttonValues), dtype=int)
        b = np.array(targetValues, dtype=int)
        A = np.zeros((len(targetValues), len(buttonValues)))
        for index, button in enumerate(buttonValues):
            for counter in button:
                A[counter, index] = 1
        constraints = LinearConstraint(A, b, b)
        integrality = np.ones_like(c)
        res = milp(c=c, constraints=constraints, integrality=integrality)
        result += int(sum(res.x))
    return result


def test_part1_example():
    result = part1("example.txt")
    assert result == 7


def test_part1():
    result = part1()
    assert result == 385


def test_part2_example():
    result = part2("example.txt")
    assert result == 33


def test_part2():
    result = part2()
    assert result == 16757
