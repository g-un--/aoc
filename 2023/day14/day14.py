from utils import getLines
import numpy as np

def getLine(line):
    return [char for char in line]

def moveRocks(array, column):
    rows, _ = np.shape(array)
    for row in range(0, rows):
        if array[row, column] == '.' or array[row, column] == '#':
            continue
        k = 1
        while row - k >= 0 and array[row - k, column] == '.':
            k += 1
        if k > 1:
            array[row-k+1, column] = array[row, column]
            array[row, column] = '.'
        
def part1():
    lines = getLines(__file__)
    array = np.array(list(map(getLine, lines)))
    rows, columns = np.shape(array)
    for column in range(0, columns):
        moveRocks(array, column)
    sum = 0
    for row in range(0, rows):
        count = np.sum(array[row] == 'O')
        sum += (rows - row) * count
    return sum

def test_part_1():
    assert part1() == 109345
