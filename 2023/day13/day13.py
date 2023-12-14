from functools import cache
from utils import getLines
import numpy as np

def getRow(line):
    row = [1 if n == '#' else 0 for n in line]
    return row

def splitLinesIntoChunks(lines):
    result = []
    chunk = []
    for line in lines:
        if line:
            chunk.append(line)
        else:
            if len(chunk) > 0:
                result.append(chunk)
            chunk = []   
            
    if len(chunk):
        result.append(chunk)
    
    return result

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
        if verticalReflection == 0 and horizontalReflection == 0:
            print(chunk)
            print()
    return total

def test_part_1():
    assert part1() == 29165
