import { getChunksOf } from "../utils";

type Monkey = {
  index: number,
  items: number[],
  operation: (old: number) => number,
  test: (item: number) => boolean,
  divisibleBy: number,
  trueResult: number,
  falseResult: number;
  inspectCount: number;
}

function parseMonkey(input: string[]): Monkey {
  const after = (item: string, after: string) => item.split(after)[1];
  const cleanSpaces = (item: string) => item.replace(" ", "");
  const notEmpty = (item: string) => item != "";
  const parseIntFromText = (item: string) => Number(/\d+/g.exec(item)![0]);

  const index = parseIntFromText(input[0]);
  const items = after(input[1], ":").split(",").map(cleanSpaces).map(Number);
  const operationItems = after(input[2], "=").split(" ").map(cleanSpaces).filter(notEmpty);
  const op = operationItems[1];
  const arg = operationItems[2];
  const argNumber = Number.parseInt(arg, 10);
  const operation = (old: number) => {
    if (op === "+") {
      return old + (argNumber ? argNumber : old);
    } else {
      return old * (argNumber ? argNumber : old);
    }
  }

  const divisibleBy = parseIntFromText(input[3]);
  const test = (item: number) => (item % divisibleBy) === 0;

  const trueResult = parseIntFromText(input[4]);
  const falseResult = parseIntFromText(input[5]);

  return { index, items, operation, test, divisibleBy, trueResult, falseResult, inspectCount: 0 };
}

function playRound(monkeys: Monkey[], divideBy3: boolean) {
  const moduloLimit = monkeys.map(monkey => monkey.divisibleBy).reduce((product, item) => product * item, 1);
  for (const monkey of monkeys.values()) {
    while (monkey.items.length > 0) {
      const item = monkey.items.shift()!;
      monkey.inspectCount += 1;
      const result = monkey.operation(item);
      const resultLevel = divideBy3 ? Math.floor(result / 3) : result % moduloLimit;
      if (monkey.test(resultLevel)) {
        monkeys[monkey.trueResult]?.items.push(resultLevel);
      } else {
        monkeys[monkey.falseResult]?.items.push(resultLevel);
      }
    }
  }
}

function parseMonkeys(input: string[]): Monkey[] {
  const chunks = getChunksOf(input, 7);
  var monkeys = new Map<number, Monkey>();
  for (const chunk of chunks) {
    const monkey = parseMonkey(chunk.map(item => item.replace("\r", "")));
    monkeys.set(monkey.index, monkey);
  }
  return [...monkeys.values()];
}

export function part1(input: string[]): number {
  const monkeys = parseMonkeys(input);
  for (let round = 1; round <= 20; round++) {
     playRound(monkeys, true);
  }

  const mostActiveMonkeys = monkeys.sort((a, b) => b.inspectCount - a.inspectCount).slice(0, 2);
  const monkeyBusiness = mostActiveMonkeys[0].inspectCount * mostActiveMonkeys[1].inspectCount;
  return monkeyBusiness;
}

export function part2(input: string[]): number {
  const monkeys = parseMonkeys(input);
  for (let round = 1; round <= 10000; round++) {
    playRound(monkeys, false);
  }

  const mostActiveMonkeys = [...monkeys.values()].sort((a, b) => b.inspectCount - a.inspectCount).slice(0, 2);
  const monkeyBusiness = mostActiveMonkeys[0].inspectCount * mostActiveMonkeys[1].inspectCount;
  return monkeyBusiness;
}
