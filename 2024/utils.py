import os
from pathlib import Path

def getLines(path, input="input.txt"):
    target = Path(path)
    inputFile = target.parent / input
    return open(inputFile, "r").read().splitlines()

def splitLinesIntoChunks(lines):
    result = []
    chunk = []
    for line in lines:
        if line:
            chunk.append(line)
        else:
            if len(chunk) > 0:
                result.append(chunk)
            chunk = []   
            
    if len(chunk):
        result.append(chunk)
    
    return result
