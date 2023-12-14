from utils import getLines
import numpy as np

def getLine(line):
    return [char for char in line]

def moveRocksNorth(array, column):
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
            
def moveRocksNorthForAllColumns(array):
    _, columns = np.shape(array)
    for column in range(0, columns):
        moveRocksNorth(array, column)
        
def cycle(array):
    moveRocksNorthForAllColumns(array)
    array = np.rot90(array, axes=(1, 0))
    moveRocksNorthForAllColumns(array)
    array = np.rot90(array, axes=(1, 0))
    moveRocksNorthForAllColumns(array)
    array = np.rot90(array, axes=(1, 0))
    moveRocksNorthForAllColumns(array)
    array = np.rot90(array, axes=(1, 0))
        
def part1():
    lines = getLines(__file__)
    array = np.array(list(map(getLine, lines)))
    rows, _ = np.shape(array)
    moveRocksNorthForAllColumns(array)
    sum = 0
    for row in range(0, rows):
        count = np.sum(array[row] == 'O')
        sum += (rows - row) * count
    return sum

def part2():
    lines = getLines(__file__)
    array = np.array(list(map(getLine, lines)))
    rows, _ = np.shape(array)
    
    arrays = []
    cycles = 0
    indexOfFound = -1
    while indexOfFound == -1:
        cycle(array)
        cycles += 1
        
        for foundIndex, foundArray in enumerate(arrays):
            if np.array_equal(array, foundArray):
                indexOfFound = foundIndex
                break
        
        arrays.append(np.copy(array))
        
    cycleLength = cycles - indexOfFound - 1
    remainder = (1000000000 - len(arrays)) % cycleLength
    array = arrays[-1]

    for _ in range(0, remainder):
        cycle(array)
    
    sum = 0
    for row in range(0, rows):
        count = np.sum(array[row] == 'O')
        sum += (rows - row) * count
    return sum

def test_part_1():
    assert part1() == 109345
    
def test_part_2():
    assert part2() == 112452