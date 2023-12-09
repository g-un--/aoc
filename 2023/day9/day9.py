from utils import getLines
from functools import reduce
import numpy as np

def getSequence(line):
    return [int(n) for n in line.split(' ')]

def getDiffs(numbers):
    arrays = []
    diffs = np.array(numbers)
    arrays.append(diffs)
    while diffs.any():
        diffs = np.diff(diffs)
        arrays.append(diffs)
    return arrays

def getNextValue(numbers):
    arrays = getDiffs(numbers)
    result = 0
    for items in reversed(arrays):
        result = items[-1] + result
    return result

def getPreviousValue(numbers):
    arrays = getDiffs(numbers)
    result = 0
    for items in reversed(arrays):
        result = items[0] - result
    return result
        
def part1():
    lines = getLines(__file__)
    sequences = map(getSequence, lines)
    results = map(getNextValue, sequences)
    return reduce(lambda x, y: x + y, results)

def part2():
    lines = getLines(__file__)
    sequences = map(getSequence, lines)
    results = map(getPreviousValue, sequences)
    return reduce(lambda x, y: x + y, results)
    
def test_part_1():
    assert part1() == 1743490457
    
def test_part_2():
    assert part2() == 1053
