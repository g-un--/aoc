function getCommonItemsInRucksack(input: string) {
  const firstPart = input.slice(0, input.length / 2);
  const secondPart = input.slice(input.length / 2);
  const intersection = getIntersection([firstPart, secondPart]);
  return intersection;
}

function getIntersection(input: string[]) {
  let intersection = input[0];
  for (const item of input.slice(1)) {
    const common = [...intersection].filter((char) => item.includes(char));
    intersection = [...new Set(common)].join();
  }
  return intersection;
}

function getChunksOf(input: string[], numberOfItemsInChunk: number) {
  const result: string[][] = [];
  let index = 0;
  while (index < input.length) {
    const chunk = input.slice(index, index + numberOfItemsInChunk);
    result.push(chunk);
    index += numberOfItemsInChunk;
  }
  return result;
}

function getPriority(input: string[]) {
  return input.reduce((sum, item) => {
    if (item === item.toLowerCase()) {
      // charCodeAt for 'a' is 97
      return sum + item.charCodeAt(0) - 96;
    } else {
      // charCodeAt for 'A' is 65
      return sum + item.charCodeAt(0) - 38;
    }
  }, 0);
}

export function part1(input: string[]): number {
  return input.reduce((sum, rucksack) => {
    if (rucksack === "")
      return sum;

    const intersection = getCommonItemsInRucksack(rucksack);
    const priority = getPriority([...intersection]);
    return sum + priority;
  }, 0);
}

export function part2(input: string[]): number {
  const chunks = getChunksOf(input, 3);
  return chunks.reduce((sum, chunk) => {
    const intersection = getIntersection(chunk);
    const priority = getPriority([...intersection]);
    return sum + priority;
  }, 0);
}
