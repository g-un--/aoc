import re
from utils import getLines

def updateMap(map, cubes):
    nrOfCubes = int(cubes.split(' ')[0])
    color = cubes.split(' ')[1]
    maxForColor = map.get(color, None)
    if (maxForColor == None or nrOfCubes > maxForColor):
        map[color] = nrOfCubes
        
def getMapsOfColors(lines):
    maps = []
    for line in lines:
        line = line.strip()
        if line:  
            lineParts = line.split(':')
            id = int(lineParts[0].split(' ')[1])
            sets = line.split(':')[1]
            map = {"id": id}
            setsParts = re.split('[,;]', sets)
            for cubes in setsParts:
                cubesClean = cubes.strip()
                updateMap(map, cubesClean)
            maps.append(map)
    return maps

def checkMap(map):
    red = map['red']
    blue = map['blue']
    green = map['green']
    return red <= 12 and green <= 13 and blue <= 14

def part1():
    lines = getLines(__file__)
    maps = getMapsOfColors(lines)
    validGames = list(filter(checkMap, maps))
    sum = 0
    for game in validGames:
        sum += game['id']
    return sum

def part2():
    lines = getLines(__file__)
    maps = getMapsOfColors(lines)
    sum = 0
    for game in maps:
        power = game['red'] * game['green'] * game['blue']
        sum += power
    return sum
                     
def test_part_1():
    result = part1()
    assert result == 2563
    
def test_part_2():
    result = part2()
    assert result == 70768