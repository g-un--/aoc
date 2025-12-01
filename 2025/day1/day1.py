from utils import getLines

def part1(input="input.txt"):
    lines = getLines(__file__, input)
    ops = [(line[0], int(line[1:])) for line in lines]
    current = 50
    password = 0
    for (direction, steps) in ops:
        current += steps if direction == 'R' else (-steps)
        current %= 100
        password += 1 if current == 0 else 0
    return password

def part2(input="input.txt"):
    lines = getLines(__file__, input)
    ops = [(line[0], int(line[1:])) for line in lines]
    current = 50
    password = 0
    for (direction, steps) in ops:
        password += steps // 100
        steps = steps % 100
        newCurrent = current + (steps if direction == 'R' else (-steps))
        if (newCurrent <= 0 and current > 0) or newCurrent >= 100:
            password += 1
        current = newCurrent % 100
    return password

def test_part1_example():
    result = part1("example.txt")
    assert result == 3

def test_part2_example():
    result = part2("example.txt")
    assert result == 6

def test_part1():
    result = part1()
    assert result == 1147

def test_part2():
    result = part2()
    assert result == 6789

