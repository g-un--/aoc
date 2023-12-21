from utils import getLines, splitLinesIntoChunks
import numpy as np

def getRow(line):
    row = [1 if n == '#' else 0 for n in line]
    return row

def findDiffCount(array1, array2):
    columns = len(array1)
    sum = 0
    for column in range(0, columns):
        if array1[column] != array2[column]:
            sum += 1
    return sum

def findReflection(array):
    rows, columns = np.shape(array)
    for row in range(0, rows-1):
        if np.array_equal(array[row], array[row+1]):
            k = 1
            shouldContinueSearch = False
            while row - k >= 0 and row + 1 + k < rows:
                if np.array_equal(array[row-k], array[row + 1 + k]):
                    k += 1
                else:
                    shouldContinueSearch = True
                    break
            if not shouldContinueSearch:
                return row
    return -1

def findReflectionPart2(array):
    rows, columns = np.shape(array)
    for row in range(0, rows-1):
        diffs = findDiffCount(array[row], array[row+1])
        if diffs <= 1:
            k = 1
            shouldContinueSearch = False
            while row - k >= 0 and row + 1 + k < rows:
                diffs += findDiffCount(array[row-k], array[row + 1 + k])
                if diffs <= 1:
                    k += 1
                else:
                    shouldContinueSearch = True
                    break
            if not shouldContinueSearch and diffs == 1:
                return row
    return -1
   
def part1():
    lines = getLines(__file__)
    chunks = splitLinesIntoChunks(lines)
    total = 0
    for chunk in chunks:
        array = np.array(list(map(getRow, chunk)))
        horizontalReflection = findReflection(array)
        total += (horizontalReflection + 1) * 100 if horizontalReflection >= 0 else 0
        arrayRotated = np.fliplr(array.T)
        verticalReflection = findReflection(arrayRotated)
        total += verticalReflection + 1 if verticalReflection >= 0 else 0
    return total

def part2():
    lines = getLines(__file__)
    chunks = splitLinesIntoChunks(lines)
    total = 0
    for chunk in chunks:
        array = np.array(list(map(getRow, chunk)))
        horizontalReflection = findReflectionPart2(array)
        total += (horizontalReflection + 1) * 100 if horizontalReflection >= 0 else 0
        arrayRotated = np.fliplr(array.T)
        verticalReflection = findReflectionPart2(arrayRotated)
        total += verticalReflection + 1 if verticalReflection >= 0 else 0
    return total

def test_part_1():
    assert part1() == 29165
    
def test_part_2():
    assert part2() == 32192