from utils import getLines
import numpy as np
import re
from functools import partial

def getMap():
    return {
        "x": 0,
        "m": 1,
        "a": 2,
        "s": 3
    }
    
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

def executePipeline(lambdas, value):
    for lambdaInstr in lambdas:
        result = lambdaInstr(value)
        if result:
            return result
        
def executeProgram(program, value):
    result = "in"
    while result != 'A' and result != 'R':
        result = executePipeline(program[result], value)
    return result

def buildLambda(variable, value, comparison, nextInstr):
    mapOfVariables = getMap()
    match comparison:
        case "<":
            return lambda x: nextInstr if x[mapOfVariables[variable]] < value else None
        case ">":
            return lambda x: nextInstr if x[mapOfVariables[variable]] > value else None
        
def parseInstr(line):
    name = re.search('[^{]*', line).group()
    instrsString = re.search('{[^}]*', line).group()[1:]
    instrs = instrsString.split(",")
    lambdas = []
    
    for instr in instrs[:-1]:
        parts = instr.split(":")
        condition = parts[0]
        nextInstr = parts[1]
        variable = re.search("[xmas]", condition).group()
        value = int(re.search(r"\d+", condition).group())
        conditionText = re.search("[<>]", condition).group()
        lambdas.append(buildLambda(variable, value, conditionText, nextInstr))
    lambdas.append(lambda x: instrs[-1])
    return name, lambdas

def parseObject(line):
    values = re.findall(r"\d+", line)
    return tuple(list(map(int, values)))
        
def part1():
    lines = getLines(__file__)
    chunks = splitLinesIntoChunks(lines)
    
    instructions = chunks[0]
    program = {}
    for instr in instructions:
        name, lambdas = parseInstr(instr)
        program[name] = lambdas
        
    values = chunks[1]
    objects = list(map(parseObject, values))
    
    total = 0
    for object in objects:
        result = executeProgram(program, object)
        if result == 'A':
            total += sum(object)
    # x m a s
    return total

def test_part_1():
    assert part1() == 352052