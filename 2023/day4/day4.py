from utils import getLines

def getNumbers(line):
    lineParts = line.split('|')
    winningNumbersAsString = lineParts[0].split(':')[1]
    numbersAsString = lineParts[1]
    winningNumbers = set([int(n) for n in winningNumbersAsString.split(' ') if n])
    numbers = set([int(n) for n in numbersAsString.split(' ') if n])
    return (winningNumbers, numbers)
    
def part1():
    lines = getLines(__file__)
    sum = 0
    for line in lines:
        winningNumbers, numbers = getNumbers(line)
        intersection = numbers.intersection(winningNumbers)
        count = len(intersection)
        if (count > 0):
            value = int(2**(count - 1))
            sum += value
    return sum

def part2():
    lines = getLines(__file__)
    sum = 0
    counts = {}
    for number, line in enumerate(lines):
        index = number + 1
        valueSoFar = counts.get(index, 0) + 1
        counts[index] = valueSoFar
        winningNumbers, numbers = getNumbers(line)
        intersection = numbers.intersection(winningNumbers)
        count = len(intersection)
        if count > 0:
            for i in range(index + 1, index + count + 1):
                value = counts.get(i, 0)
                value += valueSoFar
                counts[i] = value
    for cards in counts.values():
        sum += cards
    return sum

def test_part_1():
    assert part1() == 21485

def test_part_2():
    assert part2() == 11024379
