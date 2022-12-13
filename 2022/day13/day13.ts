import { getChunksOf } from "../utils";

type ArrayOfArrays = (number | ArrayOfArrays)[]

export function checkPair(array1: ArrayOfArrays, array2: ArrayOfArrays): boolean {
  const [first1, ...rest1] = array1;
  const [first2, ...rest2] = array2;

  if (first1 === undefined) return true;
  if (first2 === undefined) return false;

  if (typeof first1 === "number" && typeof first2 === "number") {
    if (first1 > first2) return false;
    else if (first1 < first2) return true;
  } else if (Array.isArray(first1) && Array.isArray(first2)) {
    if(!checkPair(first1, first2)) return false;
  } else if (Array.isArray(first1) && typeof first2 === "number") {
    if(!checkPair(first1, [first2])) return false;
  } else if (typeof first1 === "number" && Array.isArray(first2)) {
    if(!checkPair([first1], first2)) return false;
  }

  return checkPair(rest1, rest2);
}

export function part1(input: string[]): number {
  const pairs = getChunksOf(input, 3);
  let index = 0;
  let sum = 0;
  for(const pair of pairs) {
    index += 1;
    const firstArray = <ArrayOfArrays>JSON.parse(pair[0]);
    const secondArray = <ArrayOfArrays>JSON.parse(pair[1]);
    if (checkPair(firstArray, secondArray)) {
      sum += index;
    }
  }
  return sum;
}

export function part2(input: string[]): number {
  return 0;
}
