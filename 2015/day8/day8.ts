type matchEscapeResult = {chars: number, skip: number};
function matcheEscape(input: string, start: number): matchEscapeResult {
    const target1 = input.slice(start, start + 2);
    const target2 = input.slice(start, start + 4);
    const target3 = input.slice(start, start + 1);
    if(target1.match(/\\\\/)) {
        return {chars: 1, skip: 2};
    }
    if(target1.match(/\\"/)) {
        return {chars: 1, skip: 2};
    }
    if(target2.match(/\\x[0-9a-f][0-9a-f]/)) {
        return {chars: 1, skip: 4};
    }
    if(target3.match(/"/)) {
        return {chars: 0, skip: 1};
    }

    return {chars: 1, skip: 1};
}

function getNumberOfChars(target: string): number {
    let index = 0;
    let charsCount = 0;
    while(index < target.length) {
        const {chars, skip} = matcheEscape(target, index);
        charsCount += chars;
        index += skip;
    }
    return charsCount;
}

export function part1(input: string[]): number {
    let totalChars = 0;
    let totalLength = 0;
    for(const target of input) {
        if(target.length === 0) {
            continue;
        }
        totalChars += getNumberOfChars(target);
        totalLength += target.length; 
    }
    return totalLength - totalChars;
}

export function part2(input: string[]): number {
    let totalChars = 0;
    let totalLength = 0;
    for(const target of input) {
        if(target.length === 0) {
            continue;
        }
        const encoded = "\"" + target.replace(/\\/g, "\\\\").replace(/"/g, "\\\"") + "\"";
        totalLength += target.length; 
        totalChars += encoded.length;
    }
    return totalChars - totalLength;
}