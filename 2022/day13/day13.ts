import { getChunksOf } from "../utils";

export function checkPairs(left: Array<any>, right: Array<any>): boolean | undefined {
  const maxLength = Math.max(left.length, right.length);

  for (let index = 0; index < maxLength; index++) {
    const leftItem = left[index];
    const rightItem = right[index];

    if (leftItem === undefined) return true;
    if (rightItem === undefined) return false;

    if (Number.isInteger(leftItem) && Number.isInteger(rightItem)) {
      if (leftItem > rightItem) return false;
      if (leftItem < rightItem) return true;
      continue;
    }

    if (!Array.isArray(leftItem)) {
      return checkPairs([leftItem], rightItem);
    }

    if (!Array.isArray(rightItem)) {
      return checkPairs(leftItem, [rightItem]);
    }

    const result = checkPairs(leftItem, rightItem);
    if (result !== undefined) return result;
  }
};

export function part1(input: string[]): number {
  const pairs = getChunksOf(input, 3);
  let index = 0;
  let sum = 0;
  for (const pair of pairs) {
    index += 1;
    const firstArray = JSON.parse(pair[0]);
    const secondArray = JSON.parse(pair[1]);
    if (checkPairs(firstArray, secondArray)) {
      sum += index;
    }
  }
  return sum;
}

export function part2(input: string[]): number {
  const setOfAvalablePackets = new Set<Array<any>>();
  setOfAvalablePackets.add([[2]]);
  setOfAvalablePackets.add([[6]]);
  const pairs = input.map(line => line.replace("\r", "")).filter(line => line !== "");
  const items = pairs.map(item => JSON.parse(item));
  const dividerPacket1 = [[2]];
  const dividerPacket2 = [[6]];
  items.push(dividerPacket1, dividerPacket2);
  items.sort((a, b) => checkPairs(a, b) ? -1 : 1);
  const indexOfDividerPacket1 = items.indexOf(dividerPacket1) + 1;
  const indexOfDividerPacket2 = items.indexOf(dividerPacket2) + 1;
  return indexOfDividerPacket1 * indexOfDividerPacket2;
}
