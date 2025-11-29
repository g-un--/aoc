import { part1, part2 } from './day1';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2015/day1/part1", () => {

  test('((', () => {
    expect(part1("((")).toBe("2");
  });

  test('(())', () => {
    expect(part1("(())")).toBe("0");
  });

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part1(input!)).toBe("138");
  });

});

describe("2015/day1/part2", () => {

  test(')', () => {
    expect(part2(")")).toBe("1");
  });

  test('()())', () => {
    expect(part2("()())")).toBe("5");
  });

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part2(input!)).toBe("1771");
  });

});
