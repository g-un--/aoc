from utils import getLines
from collections import Counter

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    linesList = [line.split() for line in lines]
    safeReports = 0
    for line in linesList:
        list = [int(item) for item in line]
        shifted = list[1:]
        diffs = [a-b for a,b in zip(list, shifted)]
        diffsAbs = [abs(diff) for diff in diffs]
        allPositive = all(diff > 0 for diff in diffs)
        allNegative = all(diff < 0 for diff in diffs)
        allInInterval = all(diff >= 1 and diff <= 3 for diff in diffsAbs)
        if (allPositive or allNegative) and allInInterval:
            safeReports += 1
    return safeReports

def part2(input="input.txt"):
    lines = getLines(__file__, input)
    linesList = [line.split() for line in lines]
    safeReports = 0
    for line in linesList:
        list = [int(item) for item in line]
        for index in range(0, len(line)):
            popedItem = list.pop(index)
            shifted = list[1:]
            diffs = [a-b for a,b in zip(list, shifted)]
            diffsAbs = [abs(diff) for diff in diffs]
            allPositive = all(diff > 0 for diff in diffs)
            allNegative = all(diff < 0 for diff in diffs)
            allInInterval = all(diff >= 1 and diff <= 3 for diff in diffsAbs)
            if (allPositive or allNegative) and allInInterval:
                safeReports += 1
                break
            list.insert(index, popedItem)
    return safeReports

def test_part1_example():
    result = part1("example.txt")
    assert result == 2

def test_part2_example():
    result = part2("example.txt")
    assert result == 4

def test_part1():
    result = part1()
    assert result == 359


def test_part2():
    result = part2()
    assert result == 418