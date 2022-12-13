import { checkPair, part1, part2 } from './day13';
import { getInput } from '../utils'

describe("2022/day13/checkPairs", () => {

  test('[1,1,3,1,1] vs [1,1,5,1,1]', () => {
    const result = checkPair([1,1,3,1,1], [1,1,5,1,1]);
    expect(result).toBe(true);
  });

  test('[[1],[2,3,4]] vs [[1],4]', () => {
    const result = checkPair([[1],[2,3,4]], [[1],4]);
    expect(result).toBe(true);
  });

  test('[9] vs [[8,7,6]]', () => {
    const result = checkPair([9], [[8,7,6]]);
    expect(result).toBe(false);
  });

  test('[[4,4],4,4] vs [[4,4],4,4,4]', () =>{
    const result = checkPair([[4,4],4,4], [[4,4],4,4,4]);
    expect(result).toBe(true);
  })

  test('[7,7,7,7] vs [7,7,7]', () => {
    const result = checkPair([7,7,7,7], [7,7,7]);
    expect(result).toBe(false);
  });

  test('[] vs [3]', () => {
    const result = checkPair([], [3]);
    expect(result).toBe(true);
  });

  test('[[[]]] vs [[]]', () => {
    const result = checkPair([[[]]], [[]]);
    expect(result).toBe(false);
  });

  test('[1,[2,[3,[4,[5,6,7]]]],8,9] vs [1,[2,[3,[4,[5,6,0]]]],8,9]', () =>
  {
    const result = checkPair([1,[2,[3,[4,[5,6,7]]]],8,9], [1,[2,[3,[4,[5,6,0]]]],8,9]);
    expect(result).toBe(false);
  });
});


describe("2022/day13/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(13);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(447);
  });

});
/*
describe("2022/day13/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(29);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(446);
  });

});*/
