import fs from 'fs'
import path from 'path'

export function getInput(dirname: string): string[] {
    const input = fs.readFileSync(path.resolve(dirname, "./input.txt"), "utf-8").split("\n");
    return input;
}

export function *repeat<T>(item: T, count: number) {
    for(let index=0;index<count;index++) {
        yield item;
    }
}

export function *range<T>(start: number, end: number) {
    for(let index=start;index<end;index++) {
        yield index;
    }
}

export function *getPermutations<T>(input: Set<T>): Iterable<T[]> {
    if(input.size === 0) {
        yield [];
    }
    for(const item of input) {
        const remaining = new Set(input);
        remaining.delete(item);
        for(const items of getPermutations(remaining)) {
            const permutation: T[] = [item];
            permutation.push(...items);
            yield permutation;
        }
    }
}

export function notEmpty<TValue>(value: TValue | null | undefined): value is TValue {
    return value !== null && value !== undefined;
}