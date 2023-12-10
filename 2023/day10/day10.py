from utils import getLines
from math import ceil
import numpy as np
import sys as sys

sys.setrecursionlimit(20000)

def getPipes(line):
    return [pipe for pipe in line]

def getNeighbours(pipe):
    row, column = pipe
    return [(row-1, column-1), (row-1, column), (row-1, column+1), 
            (row, column+1), 
            (row+1, column + 1), (row+1, column), (row+1, column-1), 
            (row, column-1)]
    
def getConnectedPipes(pipe, field):
    row, column = pipe
    match field[pipe]:
        case ".":
            return []
        case "|":
            return [(row-1, column), (row+1, column)]
        case "-":
            return [(row, column-1), (row, column + 1)]
        case "L":
            return [(row-1, column), (row, column + 1)]
        case "J":
            return [(row-1, column), (row, column - 1)]
        case "7":
            return [(row+1, column), (row, column - 1)]
        case "F":
            return [(row+1, column), (row, column + 1)]
        case "S":
            return getNeighbours(pipe)
            
def findCycle(start, field, stack):
    if field[start] == 'S' and len(stack) > 0:
        return stack
        
    stack = stack[:]
    stack.append(start)

    fromStart = getConnectedPipes(start, field)
    rows,columns = np.shape(field)
    for pipe in fromStart:
        row, column = pipe
        if 0 <= row < rows and 0 <= column < columns:
            pipeConnections = getConnectedPipes(pipe, field)
            if start in pipeConnections:
                if not (pipe in stack):
                    return findCycle(pipe, field, stack)
                elif field[pipe] == 'S' and len(stack) > 2:
                    return findCycle(pipe, field, stack) 
             
def part1():
    lines = getLines(__file__)
    pipes = list(map(getPipes, lines))
    field = np.array(pipes)
    row, column = np.where(field == 'S')
    cycle = findCycle((row[0], column[0]), field, [])
    return ceil(len(cycle)/2)

def test_part_1():
    assert part1() == 6951
    
