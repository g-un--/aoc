import { part1, part2 } from './day8';
import { getInput } from '../utils'

describe("2022/day8/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(21);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(1693);
  });

});

describe("2022/day8/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(8);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(422059);
  });

});
