from utils import getLines
from functools import reduce
from operator import add, mul


def part1(input="input.txt"):
    lines = [line.split() for line in getLines(__file__, input)]
    ops = lines[len(lines) - 1]
    index = -1
    sum = 0
    for op in ops:
        index += 1
        numbers = []
        for column in range(0, len(lines) - 1):
            numbers.append(int(lines[column][index]))
        opForColumn = add if op == "+" else mul
        sum += reduce(opForColumn, numbers)
    return sum


def getNextLeftOp(ops, start):
    for index in range(start - 1, -1, -1):
        if ops[index] == "+" or ops[index] == "*":
            return index, ops[index]


def getNumber(lines, column):
    number = 0
    for row in range(0, len(lines)):
        if lines[row][column] != " ":
            number = number * 10 + int(lines[row][column])
    return number


def part2(input="input.txt"):
    lines = getLines(__file__, input)
    ops = lines[len(lines) - 1]
    lines = lines[:-1]
    index = len(ops)
    sum = 0
    while index > 0:
        numbers = []
        column, op = getNextLeftOp(ops, index)
        for columnIndex in range(index - 1, column - 1, -1):
            number = getNumber(lines, columnIndex)
            numbers.append(number)
        opForColumn = add if op == "+" else mul
        sum += reduce(opForColumn, numbers)
        index = column - 1
    return sum


def test_part1_example():
    result = part1("example.txt")
    assert result == 4277556


def test_part1():
    result = part1()
    assert result == 4878670269096


def test_part2_example():
    result = part2("example.txt")
    assert result == 3263827


def test_part2():
    result = part2()
    assert result == 8674740488592
