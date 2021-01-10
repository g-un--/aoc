import { dir } from 'console';
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
