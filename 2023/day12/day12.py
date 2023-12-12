from utils import getLines

def getLineInfo(line):
    lineParts = line.split(' ')
    groups = [int(n) for n in lineParts[1].split(',')]
    return (lineParts[0], groups)

def encodeDamagedSprings(springs):
    result = []
    counter = 0
    for spring in springs:
        if spring == '#':
            counter += 1
        elif counter > 0:
            result.append(counter)
            counter = 0
    if counter > 0:
        result.append(counter)
    return result

def satisfiesCondition(encoded, condition):
    for group, groupCondition in zip(encoded, condition):
        if group > groupCondition:
            return False
    return True

def matchCondition(encoded, condition):
    if len(encoded) != len(condition):
        return False
    
    for group, groupCondition in zip(encoded, condition):
        if group != groupCondition:
            return False
    return True

def fillUnknownSprings(springs, condition, position, test):
    if position == len(springs):
        encoded = encodeDamagedSprings(springs)
        if matchCondition(encoded, condition):
            if test(springs):
                yield springs
        return
    
    if springs[position] != '?':
        yield from fillUnknownSprings(springs, condition, position + 1, test)
        return
    
    copy = springs[:]
    copy[position] = '#'
    encoded = encodeDamagedSprings(copy[:position+1])
    if satisfiesCondition(encoded, condition):
        yield from fillUnknownSprings(copy, condition, position + 1, test)
        
    copy2 = springs[:]
    copy2[position] = '.'
    encoded = encodeDamagedSprings(copy2[:position+1])
    if satisfiesCondition(encoded, condition):
        yield from fillUnknownSprings(copy2, condition, position + 1, test)
    
def part1():
    lines = getLines(__file__)
    springsWithGroupCondition = list(map(getLineInfo, lines))
    total = 0
    for spring, condition in springsWithGroupCondition:
        totalForSpring = sum(1 for _ in fillUnknownSprings(list(spring), condition, 0, lambda x: True))
        total += totalForSpring
    return total

def part2():
    lines = getLines(__file__)
    springsWithGroupCondition = list(map(getLineInfo, lines))
    total = 0
    for spring, condition in springsWithGroupCondition:
        totalForSpring = sum(1 for _ in fillUnknownSprings(list(spring), condition, 0, lambda x: True))
        
        notStartsAndEndsWithDamage = lambda x: not (x[-1] == '#' and x[-1] == x[0])
        
        springExtendedFront = '?' + spring
        totalExtendedForSpringFront = sum(1 for _ in fillUnknownSprings(list(springExtendedFront), condition, 0, notStartsAndEndsWithDamage))
        
        springExtendedTail = spring + '?'
        totalExtendedForSpringTail = sum(1 for _ in fillUnknownSprings(list(springExtendedTail), condition, 0, notStartsAndEndsWithDamage))
        
        max = totalExtendedForSpringFront if totalExtendedForSpringFront > totalExtendedForSpringTail else totalExtendedForSpringTail
        totalForSpringExtended = totalForSpring * max * max * max * max
        
        total += totalForSpringExtended
    return total

def test_part_1():
    #assert part1() == 21
    assert part1() == 7344
    
def test_part_2():
    assert part2() == 525152
