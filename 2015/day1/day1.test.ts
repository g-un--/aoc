import { part1, part2 } from './day1'
import fs from 'fs';
import path from 'path';

describe("2015/day1/part1", () => {

  test('((', () => {
    expect(part1("((")).toBe("2");
  });

  test('(())', () => {
    expect(part1("(())")).toBe("0");
  });

  test('input.txt', () => {
    const input = fs.readFileSync(path.resolve(__dirname, "./input.txt"), "utf-8");
    expect(part1(input)).toBe("138");
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
    const input = fs.readFileSync(path.resolve(__dirname, "./input.txt"), "utf-8");
    expect(part2(input)).toBe("1771");
  });

});
