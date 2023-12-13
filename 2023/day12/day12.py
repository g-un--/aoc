from functools import cache
from utils import getLines

def getLineInfo(line):
    lineParts = line.split(' ')
    groups = [int(n) for n in lineParts[1].split(',')]
    return (lineParts[0], tuple(groups))

@cache
def countVariants(springs, groups):
    springs = springs.lstrip('.')
    
    if springs == '':
        return int(groups == ())
    
    if groups == ():
        return int(springs.find('#') == -1) 
    
    if springs[0] == "#":
        if len(springs) < groups[0] or '.' in springs[:groups[0]]:
            return 0
        elif len(springs) > groups[0] and springs[groups[0]] == '#':
            return 0
        else:
            return countVariants(springs[groups[0]+1:], groups[1:])
    
    return countVariants('#' + springs[1:], groups) + countVariants('.' + springs[1:], groups)
    
def part1():
    lines = getLines(__file__)
    springsWithGroupCondition = list(map(getLineInfo, lines))
    total = 0
    for springs, condition in springsWithGroupCondition:
        totalForSpring = countVariants(springs, condition)
        total += totalForSpring
    return total

def part2():
    lines = getLines(__file__)
    springsWithGroupCondition = list(map(getLineInfo, lines))
    total = 0
    for springs, condition in springsWithGroupCondition:
        springs = (springs+'?')*4 + springs
        condition = condition * 5
        totalForSpring = countVariants(springs, condition)
        total += totalForSpring
    return total

def test_part_1():
    assert part1() == 7344
    
def test_part_2():
    assert part2() == 1088006519007
