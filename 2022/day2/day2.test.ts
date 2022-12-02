import { part1, part2 } from './day2';
import { getInput } from '../utils'

describe("2022/day2/part1", () => {

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(10595);
  });

});

describe("2022/day2/part2", () => {

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(9541);
  });

});
