from utils import getLines, splitLinesIntoChunks
import numpy as np
import re
import portion as P

def getMap():
    return {
        "x": 0,
        "m": 1,
        "a": 2,
        "s": 3
    }

def executePipeline(intervals, object):
    for interval in intervals:
        variable, intervalRange, nextInstr = interval
        result = object[variable] in intervalRange
        if result:
            return nextInstr
        
def executeProgram(program, object):
    result = "in"
    while result != 'A' and result != 'R':
        result = executePipeline(program[result], object)
    return result

def buildIntervals(variable, value, comparison, nextInstr):
    match comparison:
        case "<":
            return (variable, P.closed(1, value-1), nextInstr)
        case ">":
            return (variable, P.closed(value+1, 4000), nextInstr)

def buildDefaultIntervals(nextInstr):
    return [
        ("x", P.closed(1, 4000), nextInstr),
        ("m", P.closed(1, 4000), nextInstr),
        ("a", P.closed(1, 4000), nextInstr),
        ("s", P.closed(1, 4000), nextInstr)
    ]    
                
def parseInstr(line):
    name = re.search('[^{]*', line).group()
    instrsString = re.search('{[^}]*', line).group()[1:]
    instrs = instrsString.split(",")
    intervals = []
    
    for instr in instrs[:-1]:
        parts = instr.split(":")
        condition = parts[0]
        nextInstr = parts[1]
        variable = re.search("[xmas]", condition).group()
        value = int(re.search(r"\d+", condition).group())
        conditionText = re.search("[<>]", condition).group()
        intervals.append(buildIntervals(variable, value, conditionText, nextInstr))
    intervals.extend(buildDefaultIntervals(instrs[-1]))
    return name, intervals

def parseObject(line):
    values = re.findall(r"[xmas]=\d+", line)
    result = {}
    for value in values:
        parts = value.split("=")
        result[parts[0]] = int(parts[1])
    return result
        
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
            total += sum(object.values())
    # x m a s
    return total

def test_part_1():
    assert part1() == 352052
