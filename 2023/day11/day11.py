from utils import getLines
from math import ceil
from itertools import combinations
from functools  import reduce
import numpy as np
import sys as sys

def getPoints(line):
    return [1 if point != '.' else 0 for point in line]

def getDistances(fields, expandValue):
    rowDistances = dict()
    colDistances = dict()
    for i,row in enumerate(fields):
        rowDistances[i] = 1 if np.any(row) else expandValue
    for i,column in enumerate(fields.T):
        colDistances[i] = 1 if np.any(column) else expandValue
    return rowDistances, colDistances

def getDistanceBetweenPairs(pair1, pair2, rowDistances, colDistances):
    firstRow, firstColumn = pair1
    secondRow, secondColumn = pair2
    startRow, endRow = sorted([firstRow, secondRow])
    startColumn, endColumn = sorted([firstColumn, secondColumn])
    distance = 0
    for row in range(startRow + 1, endRow + 1):
        distance += rowDistances[row]
    for column in range(startColumn + 1, endColumn + 1):
        distance += colDistances[column]
    return distance

def getFieldDistance(defaultDistance):
    lines = getLines(__file__)
    pipes = list(map(getPoints, lines))
    field = np.array(pipes)
    rowDistances, colDistances = getDistances(field, defaultDistance)
    result = np.transpose((field>0).nonzero()).tolist()
    pairs = list(combinations(result, 2))
    distances = []
    for start, end in pairs:
        distance = getDistanceBetweenPairs(start, end, rowDistances, colDistances)
        distances.append(distance)
    total = reduce(lambda x, y: x + y, distances)
    return total
            
def part1():
    return getFieldDistance(2)

def part2():
    return getFieldDistance(1000000)

def test_part_1():
    assert part1() == 9918828
    
def test_part_2():
    assert part2() == 692506533832
