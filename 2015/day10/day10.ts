type Group = {key: string, occurences: number};

function getGroups(input: string[]): Group[] {
    const groups: Group[] = [];

    let previousGroup: Group = {key: input[0][0], occurences: 0};
    for(const item of input) {
        for(const char of item) {
            if(previousGroup.key === char) {
                previousGroup.occurences += 1;
            } else {
                groups.push(previousGroup);
                previousGroup = {key: char, occurences: 1};
            }
        }
    }
    groups.push(previousGroup);
    
    return groups;
}

function transformString(input: string[]): string[] {
    const groups = getGroups(input);
    const result = groups.map((group) => `${group.occurences}${group.key}`);
    return result;
}

export function getTransformedLength(input: string, times: number): number {
    let result = [input]; 
    for(let round = 0; round < times; round++) {
        result = transformString(result);
    }
    return result.reduce((acc, item) => (acc + item.length), 0);
}