from utils import getLines
from functools import reduce

def getNumbersFromLine(line):
    lineParts = line.split(':')
    numbersAsString = lineParts[1].split(' ')
    numbers = [int(n) for n in numbersAsString if n]
    return numbers

def getRaces(lines):
    times = getNumbersFromLine(lines[0])
    distances = getNumbersFromLine(lines[1])
    return list(zip(times, distances))

def getLongNumberFromLine(line):
    lineParts = line.split(':')
    numbersAsString = lineParts[1].split(' ')
    numbers = [n for n in numbersAsString if n]
    longNumber = ''.join(numbers)
    return int(longNumber)

def getLongRace(lines):
    time = getLongNumberFromLine(lines[0])
    distance = getLongNumberFromLine(lines[1])
    return (time, distance)
    
def part1():
    lines = getLines(__file__)
    races = getRaces(lines)
    results = []
    for time,distance in races:
        timesBeaten = 0
        for charge in range(1, time):
            newDistance = charge * (time - charge)
            if newDistance > distance:
                timesBeaten += 1
        results.append(timesBeaten)
    return reduce(lambda x, y: x * y, results)

def part2():
    lines = getLines(__file__)
    (time, distance) = getLongRace(lines)
    timesBeaten = 0
    for charge in range(1, time):
        newDistance = charge * (time - charge)
        if newDistance > distance:
            timesBeaten += 1    
    return timesBeaten

def test_part_1():
    assert part1() == 840336

def test_part_2():
    assert part2() == 41382569

