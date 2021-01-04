export function part1(input: string): string  {
    const floor = [...input].reduce((acc, current) => {
        if (current === "(") {
            return acc + 1;
        } else if(current === ")") {
            return acc - 1;
        } else {
            throw `Invalid character in input: ${current}`;
        }
    }, 0);

    return floor.toString();
}