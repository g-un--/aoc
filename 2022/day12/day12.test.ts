import { part1, part2 } from './day12';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2022/day12/part1", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part1(input)).toBe(31);
  });

  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part1(input)).toBe(447);
  });

});

describe("2022/day12/part2", () => {

  test('example.txt', () => {
    const input = getInput(__dirname, "./example.txt");
    expect(part2(input)).toBe(29);
  });


  test('input.txt', () => {
    const input = getInput(__dirname);
    expect(part2(input)).toBe(446);
  });

});
