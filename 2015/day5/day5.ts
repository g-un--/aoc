function matches3Wovels(input: string) {
    return (input.match(/[aeiou]/g)?.length ?? 0) >= 3;
}

function matchesDuplicate(input: string): boolean {
    return input.match(/(.)\1/) !== null;
}

function matchesForbidden(input: string): boolean {
    return input.match(/ab|cd|pq|xy/) !== null;
}


function matches2Pairs(input: string) {
    return input.match(/(.{2}).*\1/) !== null;
}

function matches2LetterWithMiddle(input: string) {
    return input.match(/(.).\1/) !== null;
}

export function part1(input: string[]): string {
    let nice = 0;
    for(const item of input) {
        if(matches3Wovels(item) && matchesDuplicate(item) && !matchesForbidden(item)) {
            nice += 1;
        }
    }

    return nice.toString();
}

export function part2(input: string[]): string {
    let nice = 0;
    for(const item of input) {
        if(matches2Pairs(item) && matches2LetterWithMiddle(item)) {
            nice += 1;
        }
    }

    return nice.toString();
}
