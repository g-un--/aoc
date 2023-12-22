from utils import getLines
import numpy as np

def getline(line):
    return [c for c in line]

def visit(array, visited):
    result = []
    for itemRow, itemColumn in visited:
        for dRow, dColumn in [(1, 0), (-1, 0), (0, 1), (0,-1)]:
            newPosition = (itemRow + dRow, itemColumn + dColumn)
            if array[newPosition] == '.' or array[newPosition] == 'S' and newPosition not in visited:
                result.append(newPosition)
    return set(result)

def part1():
    lines = getLines(__file__)
    array = np.array(list(map(getline, lines)))
    row, column = np.where(array == 'S')
    start = (row.item(), column.item())
    visited = set([start])
    for _ in range(0, 64):
        result = visit(array, visited)
        visited = set(result)
    return len(result)
        
def test_part_1():
    assert part1() == 3716