import { part1, part2 } from './day3';
import { getInput } from '../utils'
import { describe, test, expect } from 'bun:test'

describe("2015/day3/part1", () => {

  test('>', () => {
    expect(part1(">")).toBe("2");
  });

  test('^>v<', () => {
    expect(part1("^>v<")).toBe("4");
  });

  test('^v^v^v^v^v', () => {
    expect(part1("^v^v^v^v^v")).toBe("2");
  });

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part1(input)).toBe("2081");
  });

});

describe("2015/day3/part2", () => {

  test('^v', () => {
    expect(part2("^v")).toBe("3");
  });

  test('^>v<', () => {
    expect(part2("^>v<")).toBe("3");
  });

  test('^v^v^v^v^v', () => {
    expect(part2("^v^v^v^v^v")).toBe("11");
  });

  test('input.txt', () => {
    const input = getInput(__dirname)[0];
    expect(part2(input)).toBe("2341");
  });

});
