from utils import getLines
from itertools import cycle
from math import lcm

def getSteps(line):
    return [step for step in line]

def getNetwork(lines):
    result = {}
    for line in lines:
        parts = line.split('=')
        key = parts[0].strip(' ')
        leftRight = parts[1].split(',')
        left = leftRight[0].strip(' ').strip('(')
        right = leftRight[1].strip(' ').strip(')')
        result[key] = (left, right)
    return result

def findPath(steps, network, start, check):
    stepsCount = 0
    stepsRepeated = cycle(steps)
    while not check(start):
        instruction = next(stepsRepeated)
        start = network[start][0] if instruction == "L" else network[start][1]
        stepsCount += 1
    return stepsCount

def part1():
    lines = getLines(__file__)
    steps = getSteps(lines[0])
    network = getNetwork(lines[2:])
    result = findPath(steps, network, "AAA", lambda x: x == "ZZZ")
    return result

def part2():
    lines = getLines(__file__)
    steps = getSteps(lines[0])
    network = getNetwork(lines[2:])
    startNodes = list(filter(lambda node: node.endswith('A'), network.keys()))
    results = map(lambda x: findPath(steps, network, x, lambda z: z.endswith("Z")), startNodes)
    return lcm(*results)
    
def test_part_1():
    assert part1() == 16271
    
def test_part_2():
    assert part2() == 14265111103729
