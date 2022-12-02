import { part1, part2 } from './day1';
import { getInput } from '../utils'

describe("2022/day1/part1", () => {

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(67633);
  });

});

describe("2022/day1/part2", () => {

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(199628);
  });

});
