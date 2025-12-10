from utils import getLines


def getArea(a, b):
    ax, ay = a
    bx, by = b
    return (abs(ax - bx) + 1) * (abs(ay - by) + 1)


def scanAreas(points, checkRect):
    maxArea = 0
    for index1 in range(0, len(points) - 1):
        for index2 in range(index1, len(points)):
            if index1 < index2:
                area = getArea(points[index1], points[index2])
                if area > maxArea and checkRect(points[index1], points[index2]):
                    maxArea = area
    return maxArea


def edge_intersects_rectangle(edge1, edge2, rect1, rect2):
    x1, y1, x2, y2, rx1, ry1, rx2, ry2 = *edge1, *edge2, *rect1, *rect2
    if y1 == y2:
        if ry1 < y1 < ry2:
            if max(x1, x2) > rx1 and min(x1, x2) < rx2:
                return True
    if x1 == x2:
        if rx1 < x1 < rx2:
            if max(y1, y2) > ry1 and min(y1, y2) < ry2:
                return True

    return False


def point_in_poly(points, x, y):
    inside = False

    for (x1, y1), (x2, y2) in zip(points, points[1:] + points[:1]):
        if (
            x == x1 == x2
            and min(y1, y2) <= y <= max(y1, y2)
            or y == y1 == y1
            and min(x1, x2) <= x <= max(x1, x2)
        ):
            return True
        if (y2 > y) != (y1 > y) and (x < (x2 - x1) * (y - y1) / (y2 - y1) + x1):
            inside = not inside

    return inside


def square_valid(points, point1, point2):
    x1, y1, x2, y2 = *point1, *point2
    x1, x2 = sorted([x1, x2])
    y1, y2 = sorted([y1, y2])
    for x, y in [(x1, y1), (x1, y2), (x2, y1), (x2, y2)]:
        if not point_in_poly(points, x, y):
            return False
    for edge1, edge2 in zip(points, points[1:] + points[:1]):
        if edge_intersects_rectangle(edge1, edge2, (x1, y1), (x2, y2)):
            return False
    return True


def part1(input="input.txt"):
    lines = [line.split(",") for line in getLines(__file__, input)]
    points = [(int(part[0]), int(part[1])) for part in lines]
    maxArea = scanAreas(points, lambda point1, point2: True)
    return maxArea


def part2(input="input.txt"):
    lines = [line.split(",") for line in getLines(__file__, input)]
    points = [(int(part[0]), int(part[1])) for part in lines]
    maxArea = scanAreas(points, lambda rect1, rect2: square_valid(points, rect1, rect2))
    return maxArea


def test_part1_example():
    result = part1("example.txt")
    assert result == 50


def test_part1():
    result = part1()
    assert result == 4752484112


def test_part2_example():
    result = part2("example.txt")
    assert result == 24


def test_part2():
    result = part2()
    assert result == 1465767840
