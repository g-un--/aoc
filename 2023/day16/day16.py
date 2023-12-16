from utils import getLines
import numpy as np

def getline(line):
    return [c for c in line]

def getDirections(cell, direction):    
    if cell == ".":
        return [direction]
    
    if cell == "\\":
        match direction:
            case "right": 
                return ["down"]
            case "left":
                return ["up"]
            case "up":
                return ["left"]
            case "down":
                return ["right"]
    
    if cell == "/":
        match direction:
            case "right":
                return ["up"]
            case "left":
                return ["down"]
            case "up":
                return ["right"]
            case "down":
                return ["left"]

    if cell == "-":
        match direction:
            case "right" | "left":
                return [direction]
            case "up" | "down":
                return ["left", "right"]
            
    if cell == "|":
        match direction:
            case "up" | "down":
                return [direction]
            case "left" | "right":
                return ["up", "down"]  
            
def moveBeam(array, startRow, startColumn, startDirection, result, seen):
    queue = [(startRow, startColumn, startDirection)]
    rows, columns = np.shape(array)
    
    while len(queue) > 0:
        row, column, direction = queue.pop(0)    
        if (row, column, direction) in seen:
            continue
        
        result.add((row, column))
        seen.add((row, column, direction))
        
        match direction:  
            case "right":
                column = column + 1
            case "left":
                column = column - 1
            case "up":
                row = row - 1
            case "down":
                row = row + 1
    
        if not (0 <= row < rows and 0 <= column < columns):
            continue
        
        nextCellValue = array[row, column]
    
        directions = getDirections(nextCellValue, direction)
        for newDirection in directions:    
            queue.append((row, column, newDirection))  
        
def part1():
    lines = getLines(__file__)
    array = np.array(list(map(getline, lines)))
    result = set()
    seen = set()
    moveBeam(array, 0, -1, "right", result, seen)
    return len(result) - 1
        
def test_part_1():
    assert part1() == 46
