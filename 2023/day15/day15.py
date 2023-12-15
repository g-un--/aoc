from utils import getLines
import re

def getTokens(line):
    return [token for token in line.split(',')]

def getHash(token):
    result = 0
    for char in token:
        asciiValue = ord(char)
        value = result + asciiValue
        result = (value * 17) % 256
    return result
        
def part1():
    lines = getLines(__file__)
    tokens = getTokens(lines[0])
    sum = 0
    for token in tokens:
        hash = getHash(token)
        sum += hash
    return sum

def getMap():
    result = {}
    for i in range(0, 256):
        result[i] = []
    return result

def getIndexLabelInList(listOfLens, labelToFind):
    for i, labelWithFocalLength in enumerate(listOfLens):
        label, focalLength = labelWithFocalLength
        if label == labelToFind:
            return i
    return -1
    
def populateMap(hashmap, token):
    tokenParts = re.split('-|=', token)
    label = tokenParts[0]
    hash = getHash(label)
    bucket = hashmap[hash]
    indexOfLabel = getIndexLabelInList(bucket, label)
    
    if token.find('=') >= 0:    
        focalLength = int(tokenParts[1])
        if indexOfLabel >= 0:
            bucket[indexOfLabel] = (label, focalLength)
        else:
            bucket.append((label, focalLength))
    else:
        if indexOfLabel >= 0:
            del bucket[indexOfLabel]
            
def part2():
    lines = getLines(__file__)
    tokens = getTokens(lines[0])
    hashmap = getMap()
    for token in tokens:
        populateMap(hashmap, token)
    sum = 0
    for box, value in hashmap.items():
        for index,item in enumerate(value):
            sum += (box + 1) * (index + 1) * item[1]
    return sum
        
def test_part_1():
    assert part1() == 495972
    
def test_part_2():
    assert part2() == 245223
