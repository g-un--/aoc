function reducer(acc: number, current: string): number {
  if (current === "(") {
    return acc + 1;
  } else if(current === ")") {
    return acc - 1;
  } else {
    throw `Invalid character in input: ${current}`;
  }
}

export function part1(input: string): string  {
    const floor = [...input].reduce((acc, current) => reducer(acc, current), 0);
    return floor.toString();
}

export function part2(input: string): string  {
    let floor = 0;
    let index = 1;
    for(const current of input) {
       floor = reducer(floor, current);
       if (floor === -1) {
           break;
       }
       index += 1;
    }
    return index.toString();
}
