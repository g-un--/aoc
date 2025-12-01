import os

def getLines(path):
    fileDir = os.path.dirname(os.path.abspath(path))
    inputFile = os.path.join(fileDir, "input.txt")
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
