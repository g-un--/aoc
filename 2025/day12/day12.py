from utils import getLines, splitLinesIntoChunks


def part1(input="input.txt"):
    lines = getLines(__file__, input)
    chunks = splitLinesIntoChunks(lines)
    shapes = []
    total = 0
    for chunk in chunks[:-1]:
        area = sum(line.count("#") for line in chunk[1:])
        shapes.append(area)
    print(shapes)
    for line in chunks[-1]:
        parts = line.split(":")
        width, height = map(int, parts[0].split("x"))
        requirements = map(int, parts[1].split())
        requiredArea = sum(
            shapes[index] * count for index, count in enumerate(requirements)
        )
        print(requiredArea)
        if requiredArea < width * height:
            total += 1
    return total

def test_part1():
    result = part1()
    assert result == 555
