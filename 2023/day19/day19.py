from utils import getLines, splitLinesIntoChunks
import re
import portion as P

def executePipeline(intervals, object):
    for interval in intervals:
        variable, intervalRange, nextInstr = interval
        result = object[variable] in intervalRange
        if result:
            return nextInstr
        
def yieldObjects(intervals, object):
    for interval in intervals:
        variable, intervalRange, nextInstr = interval
        result = object[variable] & intervalRange
        if not result.empty:
            newObject = dict(object)
            newObject[variable] = result
            yield (newObject, nextInstr)
            
            toContinue = object[variable] - result
            if toContinue.empty:
                break
            object[variable] = toContinue
            
def yieldProgramObjects(program):
    object = {
        "x": P.closed(1, 4000),
        "m": P.closed(1, 4000),
        "a": P.closed(1, 4000),
        "s": P.closed(1, 4000)
    }
    queue = [(object, program["in"])]
    result = []
    while queue:
        obj, instrs = queue.pop(0)
        for newObj, instr in yieldObjects(instrs, obj):
            if instr == 'A':
                result.append(newObj)
            elif instr != 'R':
                queue.append((newObj, program[instr]))
    return result
        
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
        name, intervals = parseInstr(instr)
        program[name] = intervals
        
    values = chunks[1]
    objects = list(map(parseObject, values))
    
    total = 0
    for object in objects:
        result = executeProgram(program, object)
        if result == 'A':
            total += sum(object.values())
    # x m a s
    return total

def part2():
    lines = getLines(__file__)
    chunks = splitLinesIntoChunks(lines)
    
    instructions = chunks[0]
    program = {}
    for instr in instructions:
        name, intervals = parseInstr(instr)
        program[name] = intervals
        
    result = list(yieldProgramObjects(program))
    total = 0
    for obj in result:
        x = sum(1 for _ in P.iterate(obj["x"], step=1))
        m = sum(1 for _ in P.iterate(obj["m"], step=1))
        a = sum(1 for _ in P.iterate(obj["a"], step=1))
        s = sum(1 for _ in P.iterate(obj["s"], step=1))
        total += x * m * a * s
    return total

def test_part_1():
    assert part1() == 352052
    
def test_part_2():
    assert part2() == 116606738659695
