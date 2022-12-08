import { part1, part2 } from './day4';
import { getInput } from '../utils'

describe("2022/day4/part1", () => {

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(477);
  });

});

describe("2022/day4/part2", () => {

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(830);
  });

});

